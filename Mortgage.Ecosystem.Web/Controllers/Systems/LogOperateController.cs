using Microsoft.AspNetCore.Mvc;
using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;
using Mortgage.Ecosystem.Web.Filter;

namespace Mortgage.Ecosystem.Web.Controllers.Systems
{
    public class LogOperateController : BaseController
    {
        private readonly ILogOperateService _iLogOperateService;

        public LogOperateController(IUnitOfWork iUnitOfWork, ILogOperateService iLogOperateService) : base(iUnitOfWork)
        {
            _iLogOperateService = iLogOperateService;
        }

        #region View function
        [AuthorizeFilter("logoperate:view")]
        public IActionResult LogOperateIndex()
        {
            return View();
        }

        [AuthorizeFilter("logoperate:view")]
        public IActionResult LogOperateDetail()
        {
            return View();
        }
        #endregion

        #region get data
        [HttpGet]
        [AuthorizeFilter("logoperate:search")]
        public async Task<IActionResult> GetListJson(LogOperateListParam param)
        {
            TData<List<LogOperateEntity>> obj = await _iLogOperateService.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("logoperate:search")]
        public async Task<IActionResult> GetPageListJson(LogOperateListParam param, Pagination pagination)
        {
            TData<List<LogOperateEntity>> obj = await _iLogOperateService.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("logoperate:view")]
        public async Task<IActionResult> GetFormJson(int id)
        {
            TData<LogOperateEntity> obj = await _iLogOperateService.GetEntity(id);
            return Json(obj);
        }
        #endregion

        #region Submit data
        [HttpPost]
        [AuthorizeFilter("logoperate:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await _iLogOperateService.DeleteForm(ids);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("logoperate:delete")]
        public async Task<IActionResult> RemoveAllFormJson()
        {
            TData obj = await _iLogOperateService.RemoveAllForm();
            return Json(obj);
        }
        #endregion
    }
}