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
    public class LogLoginController : BaseController
    {
        private readonly ILogLoginService _iLogLoginService;

        public LogLoginController(IUnitOfWork iUnitOfWork, ILogLoginService iLogLoginService) : base(iUnitOfWork)
        {
            _iLogLoginService = iLogLoginService;
        }

        #region View function
        [AuthorizeFilter("loglogin:view")]
        public IActionResult LogLoginIndex()
        {
            return View();
        }
        #endregion

        #region Get data
        [HttpGet]
        [AuthorizeFilter("loglogin:search")]
        public async Task<IActionResult> GetListJson(LogLoginListParam param)
        {
            TData<List<LogLoginEntity>> obj = await _iLogLoginService.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("loglogin:search")]
        public async Task<IActionResult> GetPageListJson(LogLoginListParam param, Pagination pagination)
        {
            TData<List<LogLoginEntity>> obj = await _iLogLoginService.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("loglogin:view")]
        public async Task<IActionResult> GetFormJson(long id)
        {
            TData<LogLoginEntity> obj = await _iLogLoginService.GetEntity(id);
            return Json(obj);
        }
        #endregion

        #region Submit data
        [HttpPost]
        [AuthorizeFilter("loglogin:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await _iLogLoginService.DeleteForm(ids);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("loglogin:delete")]
        public async Task<IActionResult> RemoveAllFormJson()
        {
            TData obj = await _iLogLoginService.RemoveAllForm();
            return Json(obj);
        }
        #endregion
    }
}