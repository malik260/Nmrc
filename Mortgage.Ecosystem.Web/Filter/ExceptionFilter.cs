using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Mortgage.Ecosystem.DataAccess.Layer.Helpers;
using Mortgage.Ecosystem.DataAccess.Layer.Models;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Operator;
using System.Net;

namespace Mortgage.Ecosystem.Web.Filter
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        private string Username;

        public override void OnException(ExceptionContext context)
        {
            var statusCode = HttpStatusCode.InternalServerError;

            if (context.Exception is EntityNotFoundException)
            {
                statusCode = HttpStatusCode.NotFound;
            }

            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.StatusCode = (int)statusCode;
            context.Result = new JsonResult(new
            {
                error = new[] { context.Exception.Message },
                stackTrace = context.Exception.StackTrace
            });

            var Users =  Operator.Instance.Current().Result;
            Username = Users.UserName;
            // Log the exception asynchronously
            Task.Run(() => LogUnhandledException(context)).ConfigureAwait(false);
        }

        private async Task LogUnhandledException(ExceptionContext context)
        {
            var User = await Operator.Instance.Current();

            var endpoint = context.ActionDescriptor as Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor;

            var ex = context.Exception;
            var endPoint = endpoint.ControllerName + ", " + endpoint.ActionName;
            var userName = Username;
            var errorMessage = ex.Message;
            if (ex.InnerException != null) errorMessage += ", INNER_EXCEPTION: " + ex.InnerException.Message;

            try
            {
               
                    var dbContext = new ApplicationDbContext();

                    var log = new ErrorLogEntity
                    {
                        Level = "Error",
                        Username = userName,
                        Callsite = endPoint.ToString(),
                        OriginatingProcess = ex.Source,
                        StackTrace = ex.StackTrace,
                        Message = errorMessage,
                        Type = ex.GetType().Name,
                        ErrorCode = ((int)HttpStatusCode.InternalServerError).ToString(),
                        AdditionalInfo = errorMessage + " " + ex.StackTrace,
                        InnerException = ex.ToString(),
                        LoggedOnDate = DateTime.Now,
                        IpAddress = NetHelper.GetLocalIPAddress(),
                        Device = Environment.MachineName
                    };

                    dbContext.ErrorLogEntity.Add(log);
                    dbContext.SaveChanges();
                
            }
            catch (Exception loggingException)
            {
                // Handle logging exception here, e.g., write to a file, etc.
            }

        }
    }
}
