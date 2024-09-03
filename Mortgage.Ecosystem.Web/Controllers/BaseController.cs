using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
using Mortgage.Ecosystem.DataAccess.Layer.Enums;
using Mortgage.Ecosystem.DataAccess.Layer.Helpers;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Operator;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;

namespace Mortgage.Ecosystem.Web.Controllers
{
    // Basic controller, used to record access logs
    public class BaseController : Controller
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public BaseController(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        // Before the action is executed
        // <param name="context">context</param>
        // <param name="next">The action continues</param>
        // <returns></returns>
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var sw = new Stopwatch();
            sw.Start();

            var action = context.RouteData.Values["Action"].ToStr();
            var user = await Operator.Instance.Current();

            var resultContext = await next();

            sw.Stop();
            var ip = NetHelper.Ip;
            //var publicIp = NetHelper.GetPublicIPAddress();
            var operateEntity = new LogOperateEntity();
            var controllerName = context.RouteData.Values["controller"] + "/";
            var currentUrl = "/" + controllerName + action;
            var currentMenuUrl = controllerName + action;

            if (user != null)
            {
                var currentMenu = await new DataRepository().GetMenuId(currentMenuUrl);
                if (currentMenu > 0)
                {
                    user.CurrentMenu = currentMenu;
                }

                var approvalEmployeeItems = await new DataRepository().GetEmployeeApprovalItems();
                user.ApprovalEmployeeItems = approvalEmployeeItems.ToList();
                user.ApprovalItemCount = user.ApprovalEmployeeItems.Count;

                var approvalEmployerItems = await new DataRepository().GetCompanyApprovalItems();
                user.ApprovalEmployerItems = approvalEmployerItems.ToList();
                user.ApprovalItemCount += user.ApprovalEmployerItems.Count;

                ViewBag.OperatorInfo = user;
            }

            var notLogAction = new string[] { "GetServerJson", "Error" };
            if (!notLogAction.Select(p => p.ToUpper()).Contains(action.ToUpper()))
            {
                #region Get request parameters

                switch (context.HttpContext.Request.Method.ToUpper())
                {
                    case "GET":
                        operateEntity.ExecuteParam = context.HttpContext.Request.QueryString.Value.ToStr();
                        break;

                    case "POST":
                        if (context.ActionArguments?.Count > 0)
                        {
                            operateEntity.ExecuteUrl += context.HttpContext.Request.QueryString.Value.ToStr();
                            operateEntity.ExecuteParam = TextHelper.GetSubString(JsonConvert.SerializeObject(context.ActionArguments), 4000);
                        }
                        else
                        {
                            operateEntity.ExecuteParam = context.HttpContext.Request.QueryString.Value.ToStr();
                        }
                        break;
                }

                #endregion Get request parameters

                #region exception acquisition

                var sbException = new StringBuilder();
                if (resultContext.Exception != null)
                {
                    var exception = resultContext.Exception;
                    sbException.AppendLine(exception.Message);
                    while (exception.InnerException != null)
                    {
                        sbException.AppendLine(exception.InnerException.Message);
                        exception = exception.InnerException;
                    }
                    sbException.AppendLine(resultContext.Exception.StackTrace);
                    operateEntity.LogStatus = OperateStatusEnum.Fail.ToInt();
                }
                else
                {
                    operateEntity.LogStatus = OperateStatusEnum.Success.ToInt();
                }

                #endregion exception acquisition

                #region log entity

                if (user != null)
                {
                    operateEntity.Company = user.Company > 0 ? user.Company : 0;
                    operateEntity.BaseCreatorId = user.Employee;
                }

                operateEntity.ExecuteTime = sw.ElapsedMilliseconds.ToInt();
                operateEntity.IpAddress = ip;
                operateEntity.ExecuteUrl = currentUrl.Replace("//", "/");
                operateEntity.ExecuteResult = TextHelper.GetSubString(sbException.ToString(), 4000);

                #endregion log entity

                //store to database
                async void taskAction()
                {
                    // Time-consuming tasks are completed asynchronously
                    await _iUnitOfWork.LogOperates.SaveForm(operateEntity);
                }
                AsyncTaskHelper.StartTask(taskAction);
            }
        }

        // After the action is executed
        // <param name="context">context</param>
        // <param name="next">next action</param>
        // <returns></returns>
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
        }
    }
}