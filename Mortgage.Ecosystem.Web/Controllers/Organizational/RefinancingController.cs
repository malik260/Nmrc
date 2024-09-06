using Microsoft.AspNetCore.Mvc;
using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.BusinessLogic.Layer.Services;
using Mortgage.Ecosystem.DataAccess.Layer.Enums;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;
using Mortgage.Ecosystem.Web.Filter;

namespace Mortgage.Ecosystem.Web.Controllers.Systems
{
    public class RefinancingController : BaseController
    {
        private readonly IRefinancingService _iRefinancingService;
        private readonly IAuditTrailService _iAuditTrailService;

        public RefinancingController(IUnitOfWork iUnitOfWork, IRefinancingService iRefinancingService, IAuditTrailService iAuditTrailService) : base(iUnitOfWork)
        {
            _iRefinancingService = iRefinancingService;
            _iAuditTrailService = iAuditTrailService;
        }

        #region View function
        [AuthorizeFilter("refinancing:view")]
        public IActionResult RefinancingIndex()
        {
            return View();
        }

        [AuthorizeFilter("refinancing:view")]
        public IActionResult RefinancingForm()
        {
            return View();
        }
        #endregion

        #region Get data
        [HttpGet]
        //[AuthorizeFilter("gender:search,user:search")]
        public async Task<IActionResult> GetListJson(RefinancingListParam param)
        {
            TData<List<RefinancingEntity>> obj = await _iRefinancingService.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("refinancing:search,user:search")]
        public async Task<IActionResult> GetPageListJson(RefinancingListParam param, Pagination pagination)
        {
            TData<List<RefinancingEntity>> obj = await _iRefinancingService.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("refinancing:view")]
        public async Task<IActionResult> GetFormJson()
        {
            TData<RefinancingEntity> obj = await _iRefinancingService.GetEntity();
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("refinancing:view")]
        public async Task<IActionResult> GetRefinanceName(RefinancingListParam param)
        {
            TData<string> obj = new TData<string>();
            var list = await _iRefinancingService.GetList(param);
            if (list.Tag == 1)
            {
                obj.Data = string.Join(",", list.Data.Select(p => p.LenderID));
                obj.Tag = 1;
            }

            var auditInstance = new AuditTrailEntity();
            auditInstance.Action = SystemOperationCode.GetRefinanceName.ToString();
            auditInstance.ActionRoute = SystemOperationCode.Refinancing.ToString();

            var audit = await _iAuditTrailService.SaveForm(auditInstance);
            return Json(obj);
        }
        #endregion

        #region Submit data
        [HttpPost]
        [AuthorizeFilter("refinancing:add,refinancing:edit")]
        public async Task<IActionResult> SaveFormJson(RefinancingEntity entity)
        {
            TData<string> obj = await _iRefinancingService.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("refinancing:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await _iRefinancingService.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}