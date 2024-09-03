using Microsoft.AspNetCore.Mvc;
using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.BusinessLogic.Layer.Services;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;
using Mortgage.Ecosystem.Web.Filter;

namespace Mortgage.Ecosystem.Web.Controllers.Organizational
{
    public class ErrorLogController : BaseController
    {
        private readonly IErrorLogService _ErrorLogService;

        public ErrorLogController(IUnitOfWork iUnitOfWork, IErrorLogService errorLogService) : base(iUnitOfWork)
        {
            _ErrorLogService = errorLogService;
           
        }

        [AuthorizeFilter("errorlog:view")]
        public IActionResult ErrorLogIndex()
        {
            return View();
        }
        public async Task<IActionResult> GetListJson(ErrorLogEntity param, Pagination pagination)
        {
            TData<List<ErrorLogEntity>> obj = await _ErrorLogService.GetPageList(param, pagination);
            return Json(obj);
        }


    }
}
