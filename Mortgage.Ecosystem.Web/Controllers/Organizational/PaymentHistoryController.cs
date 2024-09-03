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
    public class PaymentHistoryController : BaseController
    {
        private readonly IPaymentHistoryService _iPaymentHistoryService;
        private readonly IAuditTrailService _iAuditTrailService;
        public PaymentHistoryController(IUnitOfWork iUnitOfWork, IPaymentHistoryService iPaymentHistoryService, IAuditTrailService iAuditTrailService) : base(iUnitOfWork)
        {
            _iPaymentHistoryService = iPaymentHistoryService;
            _iAuditTrailService = iAuditTrailService;
        }

        #region View function
        [AuthorizeFilter("paymenthistory:view")]
        public IActionResult PaymentHistoryIndex()
        {
            return View();
        }

        public IActionResult PaymentHistoryForm()
        {
            return View();
        }

       
        #endregion

        #region Get data
        [HttpGet]
        [AuthorizeFilter("paymenthistory:search,user:search")]
        public async Task<IActionResult> GetListJson(PaymentHistoryListParam param)
        {
            TData<List<PaymentHistoryEntity>> obj = await _iPaymentHistoryService.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("paymenthistory:search,user:search")]
        public async Task<IActionResult> GetRemitaPageListJson(PaymentHistoryListParam param, Pagination pagination)
        {
            TData<List<PaymentHistoryEntity>> obj = await _iPaymentHistoryService.GetPageList(param, pagination);
            var auditInstance = new AuditTrailEntity();
            auditInstance.Action = SystemOperationCode.GetRemitaPageListJson.ToString();
            auditInstance.ActionRoute = SystemOperationCode.PaymentHistory.ToString();

            var audit = await _iAuditTrailService.SaveForm(auditInstance);
            return Json(obj);
        }


        [HttpGet]
        [AuthorizeFilter("paymenthistory:search,user:search")]
        public async Task<IActionResult> GetEtransactPageListJson(PaymentHistoryListParam param, Pagination pagination)
        {
            TData<List<PaymentHistoryEntity>> obj = await _iPaymentHistoryService.GetPageList(param, pagination);
            var auditInstance = new AuditTrailEntity();
            auditInstance.Action = SystemOperationCode.GetEtransactPageListJson.ToString();
            auditInstance.ActionRoute = SystemOperationCode.PaymentHistory.ToString();

            var audit = await _iAuditTrailService.SaveForm(auditInstance);
            return Json(obj);
        }


        [HttpGet]
        [AuthorizeFilter("paymenthistory:search,user:search")]
        public async Task<IActionResult> GetPaymentHistoryTreeListJson(PaymentHistoryListParam param)
        {
            TData<List<ZtreeInfo>> obj = await _iPaymentHistoryService.GetZtreePaymentHistoryList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("paymenthistory:view")]
        public async Task<IActionResult> GetUserTreeListJson(PaymentHistoryListParam param)
        {
            TData<List<ZtreeInfo>> obj = await _iPaymentHistoryService.GetZtreeUserList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("paymenthistory:view")]
        public async Task<IActionResult> GetFormJson(int id)
        {
            TData<PaymentHistoryEntity> obj = await _iPaymentHistoryService.GetEntity(id);
            return Json(obj);
        }

        [HttpGet]
        public async Task<IActionResult> GetMaxSortJson()
        {
            TData<int> obj = await _iPaymentHistoryService.GetMaxSort();
            return Json(obj);
        }
        #endregion

        #region Submit data
        [HttpPost]
        [AuthorizeFilter("paymenthistory:add,paymenthistory:edit")]
        public async Task<IActionResult> SaveFormJson(PaymentHistoryEntity entity)
        {
            TData<string> obj = await _iPaymentHistoryService.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("paymenthistory:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await _iPaymentHistoryService.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}