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
    public class RiskAssessmentSetupController : BaseController
    {
        private readonly IRiskAssessmentSetupService _iRiskAssessmentSetupService;
        private readonly IAuditTrailService _iAuditTrailService;
        public RiskAssessmentSetupController(IUnitOfWork iUnitOfWork, IRiskAssessmentSetupService iRiskAssessmentSetupService, IAuditTrailService iAuditTrailService) : base(iUnitOfWork)
        {
            _iRiskAssessmentSetupService = iRiskAssessmentSetupService;
            _iAuditTrailService = iAuditTrailService;
        }

        #region View function
        [AuthorizeFilter("riskassessmentsetup:view")]
        public IActionResult RiskAssessmentSetupIndex()
        {
            return View();
        }

        public IActionResult AssessmentFactorsForm()
        {
            return View();
        }

        public IActionResult IndexHeaderForm()
        {
            return View();
        }


        #endregion

        #region Get data
        [HttpGet]
        [AuthorizeFilter("riskassessmentsetup:search,user:search")]
        public async Task<IActionResult> GetListJson(RiskAssessmentSetupListParam param)
        {
            TData<List<RiskAssessmentSetupEntity>> obj = await _iRiskAssessmentSetupService.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("riskassessmentsetup:search,user:search")]
        public async Task<IActionResult> GetAssessmentFactorsPageListJson(RiskAssessmentSetupListParam param, Pagination pagination)
        {
            try
            {
                TData<List<RiskAssessmentSetupEntity>> obj = await _iRiskAssessmentSetupService.GetPageList(param, pagination);
                var auditInstance = new AuditTrailEntity();
                auditInstance.Action = SystemOperationCode.GetAssessmentFactorsPageListJson.ToString();
                auditInstance.ActionRoute = SystemOperationCode.RiskAssessmentSetup.ToString();

                var audit = await _iAuditTrailService.SaveForm(auditInstance);

                return Json(obj);
            }
            catch (Exception e)
            {

                throw;
            }
        }


        [HttpGet]
        [AuthorizeFilter("riskassessmentsetup:search,user:search")]
        public async Task<IActionResult> GetIndexHeaderPageListJson(RiskAssessmentSetupListParam param, Pagination pagination)
        {
            TData<List<RiskAssessmentSetupEntity>> obj = await _iRiskAssessmentSetupService.GetPageList(param, pagination);
            return Json(obj);
        }


        [HttpGet]
        [AuthorizeFilter("riskassessmentsetup:search,user:search")]
        public async Task<IActionResult> GetPaymentHistoryTreeListJson(RiskAssessmentSetupListParam param)
        {
            TData<List<ZtreeInfo>> obj = await _iRiskAssessmentSetupService.GetZtreeRiskAssessmentSetupList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("riskassessmentsetup:view")]
        public async Task<IActionResult> GetUserTreeListJson(RiskAssessmentSetupListParam param)
        {
            TData<List<ZtreeInfo>> obj = await _iRiskAssessmentSetupService.GetZtreeUserList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("riskassessmentsetup:view")]
        public async Task<IActionResult> GetFormJson(int id)
        {
            TData<RiskAssessmentSetupEntity> obj = await _iRiskAssessmentSetupService.GetEntity(id);
            return Json(obj);
        }

        [HttpGet]
        public async Task<IActionResult> GetMaxSortJson()
        {
            TData<int> obj = await _iRiskAssessmentSetupService.GetMaxSort();
            return Json(obj);
        }
        #endregion

        #region Submit data
        [HttpPost]
        [AuthorizeFilter("riskassessmentsetup:add,riskassessmentsetup:edit")]
        public async Task<IActionResult> SaveFormJson(RiskAssessmentSetupEntity entity)
        {
            TData<string> obj = await _iRiskAssessmentSetupService.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("riskassessmentsetup:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await _iRiskAssessmentSetupService.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}