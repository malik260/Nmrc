using Microsoft.AspNetCore.Mvc;
using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.BusinessLogic.Layer.Services;
using Mortgage.Ecosystem.DataAccess.Layer.Enums;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;
using Mortgage.Ecosystem.Web.Filter;

namespace Mortgage.Ecosystem.Web.Controllers.Organizational
{
    public class ChecklistController : BaseController
    {
        private readonly IChecklistService _iChecklistService;
        private readonly IAuditTrailService _iAuditTrailService;

        public ChecklistController(IUnitOfWork iUnitOfWork, IChecklistService iChecklistService, IAuditTrailService iAuditTrailService) : base(iUnitOfWork)
        {
            _iChecklistService = iChecklistService;
            _iAuditTrailService = iAuditTrailService;
        }

        #region View function
        [AuthorizeFilter("checklist:view")]
        public IActionResult ChecklistIndex()
        {
            return View();
        }

        public IActionResult ChecklistForm()
        {
            return View();
        }
        #endregion

        #region Get data
        [HttpGet]
        [AuthorizeFilter("checklist:search,user:search")]
        public async Task<IActionResult> GetListJson(ChecklistListParam param)
        {
            TData<List<ChecklistEntity>> obj = await _iChecklistService.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("checklist:search,user:search")]
        public async Task<IActionResult> GetChecklistPageListJson(ChecklistListParam param, Pagination pagination)
        {
            TData<List<ChecklistEntity>> obj = await _iChecklistService.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("checklist:search,user:search")]
        public async Task<IActionResult> GetChecklistTreeListJson(ChecklistListParam param)
        {
            var auditInstance = new AuditTrailEntity();
            auditInstance.Action = SystemOperationCode.GetChecklistTreeListJson.ToString();
            auditInstance.ActionRoute = SystemOperationCode.Checklist.ToString();

            var audit = await _iAuditTrailService.SaveForm(auditInstance);
            TData<List<ZtreeInfo>> obj = await _iChecklistService.GetZtreeChecklistList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("checklist:search,user:search")]
        public async Task<IActionResult> GetPageListJson(ChecklistListParam param, Pagination pagination)
        {
            TData<List<ChecklistEntity>> obj = await _iChecklistService.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("checklist:view")]
        public async Task<IActionResult> GetUserTreeListJson(ChecklistListParam param)
        {

            TData<List<ZtreeInfo>> obj = await _iChecklistService.GetZtreeUserList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("checklist:view")]
        public async Task<IActionResult> GetFormJson(int id)
        {
            TData<ChecklistEntity> obj = await _iChecklistService.GetEntity(id);
            return Json(obj);
        }

        //[HttpGet]
        //public async Task<IActionResult> GetMaxSortJson()
        //{
        //    TData<int> obj = await _iChecklistService.GetMaxSort();
        //    return Json(obj);
        //}
        #endregion

        #region Submit data
        [HttpPost]
        [AuthorizeFilter("checklist:add,company:edit")]
        public async Task<IActionResult> SaveFormJson(ChecklistEntity entity)
        {
            TData<string> obj = await _iChecklistService.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("checklist:add,employee:edit")]
        public async Task<IActionResult> UpdateFormJson(ChecklistEntity entity)
        {
            TData<string> obj = await _iChecklistService.UpdateForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("checklist:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await _iChecklistService.DeleteForm(ids);
            return Json(obj);
        }


        #endregion
    }
}
