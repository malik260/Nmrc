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
    public class CreditAssessmentIndexController : BaseController
    {
        private readonly ICreditAssessmentIndexService _iCreditAssessmentIndexService;

        public CreditAssessmentIndexController(IUnitOfWork iUnitOfWork, ICreditAssessmentIndexService iCreditAssessmentIndexService) : base(iUnitOfWork)
        {
            _iCreditAssessmentIndexService = iCreditAssessmentIndexService;
        }

        #region View function
        [AuthorizeFilter("creditassessmentindex:view")]
        public IActionResult CreditAssessmentIndexIndex()
        {
            return View();
        }

        public IActionResult CreditAssessmentIndexForm()
        {
            return View();
        }
        #endregion

        #region Get data
        [HttpGet]
        [AuthorizeFilter("creditassessmentindex:search,user:search")]
        public async Task<IActionResult> GetListJson(int indexTitleId)
        {
            TData<List<CreditAssessmentIndexEntity>> obj = await _iCreditAssessmentIndexService.GetList(indexTitleId);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("creditassessmentindex:view")]
        public async Task<IActionResult> GetFormJson(int id)
        {
            TData<CreditAssessmentIndexEntity> obj = await _iCreditAssessmentIndexService.GetEntity(id)
;
            return Json(obj);
        }
        #endregion

        #region Submit data
        [HttpPost]
        [AuthorizeFilter("creditassessmentindex:add,company:edit")]
        public async Task<IActionResult> SaveFormJson(CreditAssessmentIndexEntity entity)
        {
            TData<string> obj = await _iCreditAssessmentIndexService.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("creditassessmentindex:add,employee:edit")]
        public async Task<IActionResult> UpdateFormJson(CreditAssessmentIndexEntity entity)
        {
            TData<string> obj = await _iCreditAssessmentIndexService.UpdateForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("creditassessmentindex:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await _iCreditAssessmentIndexService.DeleteForm(ids);
            return Json(obj);
        }


        #endregion
    }
}