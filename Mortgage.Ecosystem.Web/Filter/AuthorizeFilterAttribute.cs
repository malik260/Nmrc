using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Mortgage.Ecosystem.BusinessLogic.Layer.Services;
using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
using Mortgage.Ecosystem.DataAccess.Layer.Helpers.Web;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Operator;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;

namespace Mortgage.Ecosystem.Web.Filter
{
    // Verify the login filter
    public class AuthorizeFilterAttribute : ActionFilterAttribute
    {
        public AuthorizeFilterAttribute()
        {

        }

        public AuthorizeFilterAttribute(string authorize)
        {
            Authorize = authorize;
        }

        // Permission string, e.g. user:view
        public string Authorize { get; set; }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            bool hasPermission = false;

            OperatorInfo user = await Operator.Instance.Current();
            if (user == null || user.Employee == 0)
            {
                // Prevent users from choosing to remember me, the page has been refreshed on the home page
                if (CookieHelper.Get("RememberMe").ToInt() == 1)
                {
                    Operator.Instance.RemoveCurrent();
                }

                #region Not logged in
                if (context.HttpContext.Request.IsAjaxRequest())
                {
                    TData obj = new TData();
                    obj.Message = "Sorry, no login or login timed out";
                    context.Result = new JsonResult(obj);
                    return;
                }
                else
                {
                    context.Result = new RedirectResult("~/Home/Login");
                    return;
                }
                #endregion
            }
            else
            {
                // system user has all permissions
                if (user.IsSystem == 1)
                {
                    hasPermission = true;
                }
                else
                {
                    // permission judgment
                    if (!string.IsNullOrEmpty(Authorize))
                    {
                        string[] authorizeList = Authorize.Split(',');
                        TData<List<MenuAuthorizeInfo>> objMenuAuthorize = await new MenuAuthorizeService().GetAuthorizeList(user);
                        List<MenuAuthorizeInfo> authorizeInfoList = objMenuAuthorize.Data.Where(p => authorizeList.Contains(p.Authorize)).ToList();
                        if (authorizeInfoList.Any())
                        {
                            hasPermission = true;

                            #region Add and modify judgments
                            if (context.RouteData.Values["Action"].ToString() == "SaveFormJson")
                            {
                                var id = context.HttpContext.Request.Form["Id"];
                                if (id.ToLong() > 0)
                                {
                                    if (!authorizeInfoList.Where(p => p.Authorize.Contains("edit")).Any())
                                    {
                                        hasPermission = false;
                                    }
                                }
                                else
                                {
                                    if (!authorizeInfoList.Where(p => p.Authorize.Contains("add")).Any())
                                    {
                                        hasPermission = false;
                                    }
                                }
                            }
                            #endregion
                        }
                        if (!hasPermission)
                        {
                            if (context.HttpContext.Request.IsAjaxRequest())
                            {
                                TData obj = new TData();
                                obj.Message = "Sorry, no permission";
                                context.Result = new JsonResult(obj);
                            }
                            else
                            {
                                context.Result = new RedirectResult("~/Home/NoPermission");
                            }
                        }
                    }
                    else
                    {
                        hasPermission = true;
                    }
                }
                if (hasPermission)
                {
                    var resultContext = await next();
                }
            }
        }
    }
}