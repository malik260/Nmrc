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
    [ExceptionFilter]
    public class CreditScoreController : BaseController
    {
        private readonly ICreditScoreService _iCreditScoreService;
        private readonly IAuditTrailService _iAuditTrailService;

        public CreditScoreController(IUnitOfWork iUnitOfWork, ICreditScoreService iCreditScoreService, IAuditTrailService iAuditTrailService) : base(iUnitOfWork)
        {
            _iCreditScoreService = iCreditScoreService;
            _iAuditTrailService = iAuditTrailService;   
        }

        #region View function
        [AuthorizeFilter("creditscore:view")]
        public IActionResult CreditScoreIndex()
        {
            return View();
        }

        public IActionResult CreditScoreForm()
        {
            return View();
        }

       
        #endregion

        #region Get data
        [HttpGet]
        [AuthorizeFilter("creditscore:search,user:search")]
        public async Task<IActionResult> GetListJson(CreditScoreListParam param)
        {
            TData<List<CreditScoreEntity>> obj = await _iCreditScoreService.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("creditscore:search,user:search")]
        public async Task<IActionResult> GetCreditScorePageListJson(CreditScoreListParam param, Pagination pagination)
        {
            var auditInstance = new AuditTrailEntity();
            auditInstance.Action = SystemOperationCode.GetCreditScorePageListJson.ToString();
            auditInstance.ActionRoute = SystemOperationCode.CreditScore.ToString();

            var audit = await _iAuditTrailService.SaveForm(auditInstance);
            TData<List<CreditScoreEntity>> obj = await _iCreditScoreService.GetPageList(param, pagination);
            return Json(obj);
        }


        //[HttpGet]
        //[AuthorizeFilter("propertysubscription:search,user:search")]
        //public async Task<IActionResult> GetEtransactPageListJson(PropertySubscriptionListParam param, Pagination pagination)
        //{
        //    TData<List<PropertySubscriptionEntity>> obj = await _iPropertySubscriptionService.GetPageList(param, pagination);
        //    return Json(obj);
        //}


        [HttpGet]
        [AuthorizeFilter("creditscore:search,user:search")]
        public async Task<IActionResult> GetCreditScoreTreeListJson(CreditScoreListParam param)
        {
            TData<List<ZtreeInfo>> obj = await _iCreditScoreService.GetZtreeCreditScoreList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("creditscore:view")]
        public async Task<IActionResult> GetUserTreeListJson(CreditScoreListParam param)
        {
            TData<List<ZtreeInfo>> obj = await _iCreditScoreService.GetZtreeUserList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("creditscore:view")]
        public async Task<IActionResult> GetFormJson(int id)
        {
            TData<CreditScoreEntity> obj = await _iCreditScoreService.GetEntity(id);
            return Json(obj);
        }

        [HttpGet]
        public async Task<IActionResult> GetMaxSortJson()
        {
            TData<int> obj = await _iCreditScoreService.GetMaxSort();
            return Json(obj);
        }
        #endregion

        #region Submit data
        [HttpPost]
        [AuthorizeFilter("creditscore:add,creditscore:edit")]
        public async Task<IActionResult> SaveFormJson(CreditScoreEntity entity)
        {
            TData<string> obj = await _iCreditScoreService.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("creditscore:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await _iCreditScoreService.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}