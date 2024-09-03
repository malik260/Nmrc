using Microsoft.AspNetCore.Mvc;
using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.BusinessLogic.Layer.Services;
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
    public class CreditAssessmentIndexTitleController : BaseController
    {
        private readonly ICreditAssessmentIndexTitleService _iCreditAssessmentIndexTitleService;
        private readonly IAuditTrailService _iAuditTrailService;
        public CreditAssessmentIndexTitleController(IUnitOfWork iUnitOfWork, ICreditAssessmentIndexTitleService iCreditAssessmentIndexTitleService, IAuditTrailService iAuditTrailService) : base(iUnitOfWork)
        {
            _iCreditAssessmentIndexTitleService = iCreditAssessmentIndexTitleService;
            _iAuditTrailService = iAuditTrailService;
        }

        #region View function
        [AuthorizeFilter("creditassessmentindextitle:view")]
        public IActionResult CreditAssessmentIndexTitleIndex()
        {
            return View();
        }

        public IActionResult CreditAssessmentIndexTitleForm()
        {
            return View();
        }

        public IActionResult CreditAssessmentIndexTitleEditForm()
        {
            return View();
        }
        #endregion

        #region Get data
        //[HttpGet]
        //[AuthorizeFilter("creditassessmentindextitle:search,user:search")]
        //public async Task<IActionResult> GetListJson(int factorIndexId)
        //{
        //    TData<List<CreditAssessmentIndexTitleEntity>> obj = await _iCreditAssessmentIndexTitleService.GetList(factorIndexId);
        //    return Json(obj);
        //}

        [HttpGet]
        [AuthorizeFilter("creditassessmentfactorindex:search,user:search")]
        public async Task<IActionResult> GetListJson(int factorIndexId)
        {
            List<CreditAssessmentIndexTitleEntity> obj = await _iCreditAssessmentIndexTitleService.GetList(factorIndexId);
            return Json(obj);
        }

        public async Task<IActionResult> GetIndexTitle(int FactorIndexId)
        {
            TData<List<CreditAssessmentIndexTitleEntity>> obj = await _iCreditAssessmentIndexTitleService.GetIndexTitle(FactorIndexId);
            return Json(obj);
        }


        public async Task<IActionResult> GetCreditTypePageListJson(CreditAssessmentIndexTitleListParam param, Pagination pagination)
        {
            TData<List<CreditAssessmentIndexTitleEntity>> obj = await _iCreditAssessmentIndexTitleService.GetPageList(param, pagination);
            return Json(obj);
        }


        [HttpGet]
        [AuthorizeFilter("creditassessmentindextitle:view")]
        public async Task<IActionResult> GetFormJson(int id)
        {
            TData<CreditAssessmentIndexTitleEntity> obj = await _iCreditAssessmentIndexTitleService.GetEntity(id)
;
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("creditassessmentindextitle:view")]
        public async Task<IActionResult> GetFormJsonn(int id)
        {
            TData<CreditAssessmentIndexTitleEntity> obj = await _iCreditAssessmentIndexTitleService.GetEntities(id)
;
            return Json(obj);
        }


        #endregion

        #region Submit data
        [HttpPost]
        [AuthorizeFilter("creditassessmentindextitle:add,company:edit")]
        public async Task<IActionResult> SaveFormJson(CreditAssessmentIndexTitleEntity entity)
        {
            TData<string> obj = await _iCreditAssessmentIndexTitleService.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("creditassessmentindextitle:add,employee:edit")]
        public async Task<IActionResult> UpdateFormJson(CreditAssessmentIndexTitleEntity entity)
        {
            TData<string> obj = await _iCreditAssessmentIndexTitleService.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("creditassessmentindextitle:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await _iCreditAssessmentIndexTitleService.DeleteForm(ids);
            return Json(obj);
        }


        #endregion
    }
}