using Microsoft.AspNetCore.Mvc;
using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;
using Mortgage.Ecosystem.Web.Filter;

namespace Mortgage.Ecosystem.Web.Controllers.Systems
{
    public class ApprovalSetupController : BaseController
    {
        private readonly IApprovalSetupService _iApprovalSetupService;

        public ApprovalSetupController(IUnitOfWork iUnitOfWork, IApprovalSetupService iApprovalSetupService) : base(iUnitOfWork)
        {
            _iApprovalSetupService = iApprovalSetupService;
        }

        #region View function
        [AuthorizeFilter("approvalsetup:view")]
        public IActionResult ApprovalSetupIndex()
        {
            return View();
        }

        [AuthorizeFilter("approvalsetup:view")]
        public IActionResult ApprovalSetupForm()
        {
            return View();
        }
        #endregion

        #region Get data
        [HttpGet]
        [AuthorizeFilter("approvalsetup:search,user:search")]
        public async Task<IActionResult> GetListJson(ApprovalSetupListParam param)
        {
            TData<List<ApprovalSetupEntity>> obj = await _iApprovalSetupService.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("approvalsetup:search,user:search")]
        public async Task<IActionResult> GetPageListJson(ApprovalSetupListParam param, Pagination pagination)
        {
            TData<List<ApprovalSetupEntity>> obj = await _iApprovalSetupService.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("approvalsetup:view")]
        public async Task<IActionResult> GetFormJson(long id)
        {
            TData<ApprovalSetupEntity> obj = await _iApprovalSetupService.GetEntity(id);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("approvalsetup:view")]
        public async Task<IActionResult> GetApprovalSetupName(ApprovalSetupListParam param)
        {
            TData<string> obj = new TData<string>();
            var list = await _iApprovalSetupService.GetList(param);
            if (list.Tag == 1)
            {
                obj.Data = string.Join(",", list.Data.Select(p => p.MenuId));
                obj.Tag = 1;
            }
            return Json(obj);
        }
        #endregion

        #region Submit data
        [HttpPost]
        [AuthorizeFilter("approvalsetup:add,approvalsetup:edit")]
        public async Task<IActionResult> SaveFormJson(ApprovalSetupEntity entity)
        {
            TData<string> obj = await _iApprovalSetupService.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("approvalsetup:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await _iApprovalSetupService.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}