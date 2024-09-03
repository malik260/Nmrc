using Microsoft.AspNetCore.Mvc;
using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
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
    public class ChargeSetupController : BaseController
    {
        private readonly IChargeSetupService _iChargeSetupService;
        private readonly IAuditTrailService _iAuditTrailService;

        public ChargeSetupController(IUnitOfWork iUnitOfWork, IChargeSetupService iChargeSetupService, IAuditTrailService iAuditTrailService) : base(iUnitOfWork)
        {
            _iChargeSetupService = iChargeSetupService;
            _iAuditTrailService = iAuditTrailService;
        }

        #region View function
        [AuthorizeFilter("ChargeSetup:view")]
        public IActionResult ChargeSetupIndex()
        {
            return View();
        }

        public IActionResult ChargeSetupForm()
        {
            return View();
        }
        #endregion

        #region Get data
        [HttpGet]
        [AuthorizeFilter("ChargeSetup:search,user:search")]
        public async Task<IActionResult> GetListJson(ChargeSetupListParam param)
        {
            TData<List<ChargeSetupEntity>> obj = await _iChargeSetupService.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("ChargeSetup:search,user:search")]
        public async Task<IActionResult> GetChargeSetupPageListJson(ChargeSetupListParam param, Pagination pagination)
        {
            var auditInstance = new AuditTrailEntity();
            auditInstance.Action = SystemOperationCode.GetChargeSetupPageListJson.ToString();
            auditInstance.ActionRoute = SystemOperationCode.ChargeSetup.ToString();

            var audit = await _iAuditTrailService.SaveForm(auditInstance);
            TData<List<ChargeSetupEntity>> obj = await _iChargeSetupService.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("ChargeSetup:search,user:search")]
        public async Task<IActionResult> GetCompanyTreeListJson(ChargeSetupListParam param)
        {
            TData<List<ZtreeInfo>> obj = await _iChargeSetupService.GetZtreeChargeSetupList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("ChargeSetup:view")]
        public async Task<IActionResult> GetUserTreeListJson(ChargeSetupListParam param)
        {
            TData<List<ZtreeInfo>> obj = await _iChargeSetupService.GetZtreeUserList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("ChargeSetup:view")]
        public async Task<IActionResult> GetFormJson(int id)
        {
            TData<ChargeSetupEntity> obj = await _iChargeSetupService.GetEntity(id);
            return Json(obj);
        }

        [HttpGet]
        public async Task<IActionResult> GetMaxSortJson()
        {
            TData<int> obj = await _iChargeSetupService.GetMaxSort();
            return Json(obj);
        }
        #endregion

        #region Submit data
        [HttpPost]
        [AuthorizeFilter("ChargeSetup:add,company:edit")]
        public async Task<IActionResult> SaveFormJson(ChargeSetupEntity entity)
        {
            TData<string> obj = await _iChargeSetupService.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("ChargeSetup:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await _iChargeSetupService.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}