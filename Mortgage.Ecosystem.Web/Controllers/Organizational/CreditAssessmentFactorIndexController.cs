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
    public class CreditAssessmentFactorIndexController : BaseController
    {
        private readonly ICreditAssessmentFactorIndexService _iCreditAssessmentFactorIndexService;

        public CreditAssessmentFactorIndexController(IUnitOfWork iUnitOfWork, ICreditAssessmentFactorIndexService iCreditAssessmentFactorIndexService) : base(iUnitOfWork)
        {
            _iCreditAssessmentFactorIndexService = iCreditAssessmentFactorIndexService;
        }

        #region View function
        [AuthorizeFilter("creditassessmentfactorindex:view")]
        public IActionResult CreditAssessmentFactorIndexIndex()
        {
            return View();
        }

        public IActionResult CreditAssessmentFactorIndexForm()
        {
            return View();
        }
        #endregion

        #region Get data
        [HttpGet]
        [AuthorizeFilter("creditassessmentfactorindex:search,user:search")]
        public async Task<IActionResult> GetListJson(int riskFactorId)
        {
            List<CreditAssessmentFactorIndexEntity> obj = await _iCreditAssessmentFactorIndexService.GetList(riskFactorId);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("creditassessmentfactorindex:view")]
        public async Task<IActionResult> GetFormJson(int id)
        {
            TData<CreditAssessmentFactorIndexEntity> obj = await _iCreditAssessmentFactorIndexService.GetEntity(id)
;
            return Json(obj);
        }
        #endregion

        #region Submit data
        [HttpPost]
        [AuthorizeFilter("creditassessmentfactorindex:add,company:edit")]
        public async Task<IActionResult> SaveFormJson(CreditAssessmentFactorIndexEntity entity)
        {
            TData<string> obj = await _iCreditAssessmentFactorIndexService.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("creditassessmentfactorindex:add,employee:edit")]
        public async Task<IActionResult> UpdateFormJson(CreditAssessmentFactorIndexEntity entity)
        {
            TData<string> obj = await _iCreditAssessmentFactorIndexService.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("creditassessmentfactorindex:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await _iCreditAssessmentFactorIndexService.DeleteForm(ids);
            return Json(obj);
        }


        #endregion
    }
}