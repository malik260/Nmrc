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
    public class ApprovalLogController : BaseController
    {
        private readonly IApprovalLogService _iApprovalLogService;
        private readonly IAuditTrailService _iAuditTrailService;
        public ApprovalLogController(IUnitOfWork iUnitOfWork, IApprovalLogService iApprovalLogService) : base(iUnitOfWork)
        {
            _iApprovalLogService = iApprovalLogService;
        }

        #region View function
        [AuthorizeFilter("approvallog:view")]
        public IActionResult ApprovalLogIndex()
        {
            return View();
        }

        [AuthorizeFilter("approvallog:view")]
        public IActionResult ApprovalLogForm()
        {
            return View();
        }
        #endregion

        #region Get data
        [HttpGet]
        [AuthorizeFilter("approvallog:search,user:search")]
        public async Task<IActionResult> GetListJson(ApprovalLogListParam param)
        {
            TData<List<ApprovalLogEntity>> obj = await _iApprovalLogService.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("approvallog:search,user:search")]
        public async Task<IActionResult> GetPageListJson(ApprovalLogListParam param, Pagination pagination)
        {
            TData<List<ApprovalLogEntity>> obj = await _iApprovalLogService.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("approvallog:view")]
        public async Task<IActionResult> GetFormJson(long id)
        {
            TData<ApprovalLogEntity> obj = await _iApprovalLogService.GetEntity(id);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("approvallog:view")]
        public async Task<IActionResult> GetApprovalLogName(ApprovalLogListParam param)
        {
            TData<string> obj = new TData<string>();
            var list = await _iApprovalLogService.GetList(param);
            if (list.Tag == 1)
            {
                obj.Data = string.Join(",", list.Data.Select(p => p.MenuId));
                obj.Tag = 1;
            }

            var auditInstance = new AuditTrailEntity();
            auditInstance.Action = SystemOperationCode.GetApprovalLogName.ToString();
            auditInstance.ActionRoute = SystemOperationCode.AppprovalLog.ToString();

            var audit = await _iAuditTrailService.SaveForm(auditInstance);
            return Json(obj);
        }
        #endregion

        #region Submit data
        [HttpPost]
        [AuthorizeFilter("approvallog:add,approvallog:edit")]
        public async Task<IActionResult> SaveFormJson(ApprovalLogEntity entity)
        {
            TData<string> obj = await _iApprovalLogService.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("approvallog:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await _iApprovalLogService.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}