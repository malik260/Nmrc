using Microsoft.AspNetCore.Mvc;
using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.BusinessLogic.Layer.Services;
using Mortgage.Ecosystem.DataAccess.Layer.Enums;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;
using Mortgage.Ecosystem.Web.Filter;

namespace Mortgage.Ecosystem.Web.Controllers.Systems
{
    public class DisbursementController : BaseController
    {
        private readonly IDisbursementService _iDisbursementService;
        private readonly IAuditTrailService _iAuditTrailService;

        public DisbursementController(IUnitOfWork iUnitOfWork, IDisbursementService iDisbursementService, IAuditTrailService iAuditTrailService) : base(iUnitOfWork)
        {
            _iDisbursementService = iDisbursementService;
            _iAuditTrailService = iAuditTrailService;
        }

        #region View function
        [AuthorizeFilter("disbursement:view")]
        public IActionResult DisbursementIndex()
        {
            return View();
        }

        [AuthorizeFilter("disbursement:view")]
        public IActionResult DisbursementForm()
        {
            return View();
        }
        #endregion

        #region Get data
        [HttpGet]
        //[AuthorizeFilter("disbursement:search,user:search")]
        public async Task<IActionResult> GetListJson(DisbursementListParam param)
        {
            TData<List<DisbursementEntity>> obj = await _iDisbursementService.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("loanreview:search,user:search")]
        public async Task<IActionResult> GetPageListJson(DisbursementListParam param, Pagination pagination)
        {
            TData<List<DisbursementEntity>> obj = await _iDisbursementService.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("loanreview:view")]
        public async Task<IActionResult> GetFormJson()
        {
            TData<DisbursementEntity> obj = await _iDisbursementService.GetEntity();
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("loanreview:view")]
        public async Task<IActionResult> GetLenderName(DisbursementListParam param)
        {
            TData<string> obj = new TData<string>();
            var list = await _iDisbursementService.GetList(param);
            if (list.Tag == 1)
            {
                obj.Data = string.Join(",", list.Data.Select(p => p.LenderID));
                obj.Tag = 1;
            }

            var auditInstance = new AuditTrailEntity();
            auditInstance.Action = SystemOperationCode.GetDisbursementName.ToString();
            auditInstance.ActionRoute = SystemOperationCode.Disbursement.ToString();

            var audit = await _iAuditTrailService.SaveForm(auditInstance);
            return Json(obj);
        }
        #endregion

        #region Submit data
        [HttpPost]
        [AuthorizeFilter("loanreview:add,refinancing:edit")]
        public async Task<IActionResult> SaveFormJson(DisbursementEntity entity)
        {
            TData<string> obj = await _iDisbursementService.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("loanreview:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await _iDisbursementService.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}