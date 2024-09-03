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
    public class CreditAssessmentRiskFactorController : BaseController
    {
        private readonly ICreditAssessmentRiskFactorService _iCreditAssessmentRiskFactorService;
        private readonly IAuditTrailService _iAuditTrailService;
        public CreditAssessmentRiskFactorController(IUnitOfWork iUnitOfWork, ICreditAssessmentRiskFactorService iCreditAssessmentRiskFactorService, IAuditTrailService iAuditTrailService) : base(iUnitOfWork)
        {
            _iCreditAssessmentRiskFactorService = iCreditAssessmentRiskFactorService;
            _iAuditTrailService = iAuditTrailService;
        }

        #region View function
        [AuthorizeFilter("creditassessmentriskfactor:view")]
        public IActionResult CreditAssessmentRiskFactorIndex()
        {
            return View();
        }

        public IActionResult CreditAssessmentRiskFactorForm()
        {
            return View();
        }

        public IActionResult CreditAssessmentRiskFactorEditForm()
        {
            return View();
        }
        #endregion

        #region Get data
        [HttpGet]
        [AuthorizeFilter("creditassessmentriskfactor:search,user:search")]
        //public async Task<IActionResult> GetListJson(string productcode)
        //{
        //    List<CreditAssessmentRiskFactorEntity> obj = await _iCreditAssessmentRiskFactorService.GetList(productcode);
        //    return Json(obj);
        //}
        public async Task<IActionResult> GetListJson(string productcode)
        {
            List<CreditAssessmentRiskFactorEntity> obj = await _iCreditAssessmentRiskFactorService.GetList(productcode);
            return Json(obj);
        }
        public async Task<IActionResult> GetRisks(string productcode)
        {
            var auditInstance = new AuditTrailEntity();
            auditInstance.Action = SystemOperationCode.GetRisks.ToString();
            auditInstance.ActionRoute = SystemOperationCode.CreditAssessmentFactor.ToString();

            var audit = await _iAuditTrailService.SaveForm(auditInstance);
            TData<List<CreditAssessmentRiskFactorEntity>> obj = await _iCreditAssessmentRiskFactorService.Getrisks(productcode);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("creditassessmentriskfactor:view")]
        public async Task<IActionResult> GetFormJson(long id)
        {
            TData<CreditAssessmentRiskFactorEntity> obj = await _iCreditAssessmentRiskFactorService.GetEntity(id)
;
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("creditassessmentriskfactor:view")]
        public async Task<IActionResult> GetFormJsonn(int id)
        {
            TData<CreditAssessmentRiskFactorEntity> obj = await _iCreditAssessmentRiskFactorService.GetEntities(id)
;
            return Json(obj);
        }

        public async Task<IActionResult> GetCreditTypePageListJson(CreditAssessmentRiskFactorListParam param, Pagination pagination)
        {
            TData<List<CreditAssessmentRiskFactorEntity>> obj = await _iCreditAssessmentRiskFactorService.GetPageList(param, pagination);
            return Json(obj);
        }
        #endregion

        #region Submit data
        [HttpPost]
        [AuthorizeFilter("creditassessmentriskfactor:add,company:edit")]
        public async Task<IActionResult> SaveFormJson(CreditAssessmentRiskFactorEntity entity)
        {
            TData<string> obj = await _iCreditAssessmentRiskFactorService.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("creditassessmentriskfactor:add,employee:edit")]
        public async Task<IActionResult> UpdateFormJson(CreditAssessmentRiskFactorEntity entity)
        {
            TData<string> obj = await _iCreditAssessmentRiskFactorService.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("creditassessmentriskfactor:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await _iCreditAssessmentRiskFactorService.DeleteForm(ids);
            return Json(obj);
        }


        #endregion
    }
}