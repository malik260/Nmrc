using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Mortgage.Ecosystem.DataAccess.Layer;
using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
using Mortgage.Ecosystem.DataAccess.Layer.Helpers;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using System.Web;

namespace Mortgage.Ecosystem.Web.Filter
{
    // Global exception filter
    public class GlobalExceptionFilter : IExceptionFilter, IAsyncExceptionFilter
    {
        // Global exception catch
        // <param name="context"></param>
        // <returns></returns>
        public Task OnExceptionAsync(ExceptionContext context)
        {
            if (GlobalConstant.IsProduction)
            {
                OnException(context);
            }
            return Task.CompletedTask;
        }

        // Global exception catch
        // <param name="context"></param>
        public void OnException(ExceptionContext context)
        {
            LogHelper.Error(context.Exception);

            var errorMessage = context.Exception.GetOriginalException().Message;
            if (context.HttpContext.Request.IsAjaxRequest())
            {
                var obj = new TData();
                obj.Message = errorMessage ?? "Sorry, system error, please contact the administrator!";
                context.Result = new JsonResult(obj);
                context.ExceptionHandled = true;
            }
            else
            {
                context.Result = new RedirectResult($"~/Home/Error?message={HttpUtility.UrlEncode(errorMessage)}");
                context.ExceptionHandled = true;
            }
        }
    }
}