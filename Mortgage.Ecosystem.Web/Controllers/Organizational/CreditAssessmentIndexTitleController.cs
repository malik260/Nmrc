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
    public class CreditAssessmentIndexTitleController : BaseController
    {
        private readonly ICreditAssessmentIndexTitleService _iCreditAssessmentIndexTitleService;

        public CreditAssessmentIndexTitleController(IUnitOfWork iUnitOfWork, ICreditAssessmentIndexTitleService iCreditAssessmentIndexTitleService) : base(iUnitOfWork)
        {
            _iCreditAssessmentIndexTitleService = iCreditAssessmentIndexTitleService;
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
        #endregion

        #region Get data
        [HttpGet]
        [AuthorizeFilter("creditassessmentindextitle:search,user:search")]
        public async Task<IActionResult> GetListJson(int factorIndexId)
        {
            TData<List<CreditAssessmentIndexTitleEntity>> obj = await _iCreditAssessmentIndexTitleService.GetList(factorIndexId);
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