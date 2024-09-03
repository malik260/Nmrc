using Microsoft.AspNetCore.Mvc;
using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Enums;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;
using Mortgage.Ecosystem.Web.Filter;

namespace Mortgage.Ecosystem.Web.Controllers.Systems
{
    public class TitleController : BaseController
    {
        private readonly ITitleService _iTitleService;
        private readonly IAuditTrailService _iAuditTrailService;

        public TitleController(IUnitOfWork iUnitOfWork, ITitleService iTitleService, IAuditTrailService iAuditTrailService) : base(iUnitOfWork)
        {
            _iTitleService = iTitleService;
            _iAuditTrailService = iAuditTrailService;
        }

        #region View function
        [AuthorizeFilter("title:view")]
        public IActionResult TitleIndex()
        {
            return View();
        }

        [AuthorizeFilter("title:view")]
        public IActionResult TitleForm()
        {
            return View();
        }
        #endregion

        #region Get data
        [HttpGet]
        //[AuthorizeFilter("title:search,user:search")]
        public async Task<IActionResult> GetListJson(TitleListParam param)
        {
            TData<List<TitleEntity>> obj = await _iTitleService.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("title:search,user:search")]
        public async Task<IActionResult> GetPageListJson(TitleListParam param, Pagination pagination)
        {
            TData<List<TitleEntity>> obj = await _iTitleService.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("title:view")]
        public async Task<IActionResult> GetFormJson(int id)
        {
            TData<TitleEntity> obj = await _iTitleService.GetEntity(id);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("title:view")]
        public async Task<IActionResult> GetTitleName(TitleListParam param)
        {
            TData<string> obj = new TData<string>();
            var list = await _iTitleService.GetList(param);
            if (list.Tag == 1)
            {
                obj.Data = string.Join(",", list.Data.Select(p => p.Name));
                obj.Tag = 1;
            }

            var auditInstance = new AuditTrailEntity();
            auditInstance.Action = SystemOperationCode.GetTitleName.ToString();
            auditInstance.ActionRoute = SystemOperationCode.Title.ToString();

            var audit = await _iAuditTrailService.SaveForm(auditInstance);
            return Json(obj);
        }
        #endregion

        #region Submit data
        [HttpPost]
        [AuthorizeFilter("title:add,title:edit")]
        public async Task<IActionResult> SaveFormJson(TitleEntity entity)
        {
            TData<string> obj = await _iTitleService.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("title:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await _iTitleService.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}