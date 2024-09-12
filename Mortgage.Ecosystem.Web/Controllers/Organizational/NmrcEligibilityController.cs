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
    public class NmrcEligibilityController : BaseController
    {
        private readonly INmrcEligibilityService _iNmrcEligibilityService;
        private readonly IAuditTrailService _iAuditTrailService;
        public NmrcEligibilityController(IUnitOfWork iUnitOfWork, INmrcEligibilityService iNmrcEligibilityService, IAuditTrailService iAuditTrailService) : base(iUnitOfWork)
        {
            _iNmrcEligibilityService = iNmrcEligibilityService;
            _iAuditTrailService = iAuditTrailService;
        }

        #region View function
        [AuthorizeFilter("nmrceligibility:view")]
        public IActionResult NmrcEligibilityIndex()
        {
            return View();
        }

        public IActionResult NmrcEligibilityForm()
        {
            return View();
        }

        public IActionResult NmrcEligibilityEditForm()
        {
            return View();
        }
        #endregion

        #region Get data
        [HttpGet]
        [AuthorizeFilter("nmrceligibility:search,user:search")]
        public async Task<IActionResult> GetListJson(NmrcEligibilityListParam param)
        {
            TData<List<NmrcEligibilityEntity>> obj = await _iNmrcEligibilityService.GetList(param);
            return Json(obj);
        }

        public async Task<IActionResult> GetPmbEligibilityCriteria(NmrcEligibilityListParam param)
        {
            TData<List<NmrcEligibilityEntity>> obj = await _iNmrcEligibilityService.GetPmbList(param);
            return Json(obj);
        }

         public async Task<IActionResult> GetObligorligibilityCriteria(NmrcEligibilityListParam param)
        {
            TData<List<NmrcEligibilityEntity>> obj = await _iNmrcEligibilityService.GetObligorList(param);
            return Json(obj);
        }


        public async Task<IActionResult> GetCategory(string categoryId)
        {
            var auditInstance = new AuditTrailEntity();
            auditInstance.Action = SystemOperationCode.GetNmrcEligibilityCriteria.ToString();
            auditInstance.ActionRoute = SystemOperationCode.NmrcEligibility.ToString();

            var audit = await _iAuditTrailService.SaveForm(auditInstance);
            TData<List<NmrcEligibilityEntity>> obj = await _iNmrcEligibilityService.GetCategory(categoryId);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("nmrceligibility:search,user:search")]
        public async Task<IActionResult> GetNmrcEligibilityCriteriaPageListJson(NmrcEligibilityListParam param, Pagination pagination)
        {
            TData<List<NmrcEligibilityEntity>> obj = await _iNmrcEligibilityService.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("nmrceligibility:view")]
        public async Task<IActionResult> GetFormJson(int id)
        {
            TData<NmrcEligibilityEntity> obj = await _iNmrcEligibilityService.GetEntity(id)
;
            return Json(obj);
        }

        public async Task<IActionResult> GetFormJsonn(int id)
        {
            TData<NmrcEligibilityEntity> obj = await _iNmrcEligibilityService.GetEntities(id)
;
            return Json(obj);
        }
        #endregion

        #region Submit data
        [HttpPost]
        //[AuthorizeFilter("nmrceligibility:add,company:edit")]
        public async Task<IActionResult> SaveFormJson(NmrcEligibilityEntity entity)
        {
            TData<string> obj = await _iNmrcEligibilityService.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        // [AuthorizeFilter("nmrceligibility:add,employee:edit")]
        public async Task<IActionResult> UpdateFormJson(NmrcEligibilityEntity entity)
        {
            TData<string> obj = await _iNmrcEligibilityService.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("nmrceligibility:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await _iNmrcEligibilityService.DeleteForm(ids);
            return Json(obj);
        }


        #endregion
    }
}