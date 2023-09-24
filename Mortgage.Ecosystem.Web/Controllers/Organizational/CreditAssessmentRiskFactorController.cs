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
    public class CreditAssessmentRiskFactorController : BaseController
    {
        private readonly ICreditAssessmentRiskFactorService _iCreditAssessmentRiskFactorService;

        public CreditAssessmentRiskFactorController(IUnitOfWork iUnitOfWork, ICreditAssessmentRiskFactorService iCreditAssessmentRiskFactorService) : base(iUnitOfWork)
        {
            _iCreditAssessmentRiskFactorService = iCreditAssessmentRiskFactorService;
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

        [HttpGet]
        [AuthorizeFilter("creditassessmentriskfactor:view")]
        public async Task<IActionResult> GetFormJson(int id)
        {
            TData<CreditAssessmentRiskFactorEntity> obj = await _iCreditAssessmentRiskFactorService.GetEntity(id)
;
            return Json(obj);
        }

        //[HttpGet]
        //public async Task<IActionResult> GetMaxSortJson()
        //{
        //    TData<int> obj = await _iCreditAssessmentRiskFactorService.GetMaxSort();
        //    return Json(obj);
        //}
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