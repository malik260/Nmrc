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
    public class ChangeEmployerController : BaseController
    {
        private readonly IChangeEmployerService _iChangeEmployerService;
        private readonly IAuditTrailService _iAuditTrailService;

        public ChangeEmployerController(IUnitOfWork iUnitOfWork, IChangeEmployerService iChangeEmployerService, IAuditTrailService AuditTrailService) : base(iUnitOfWork)
        {
            _iChangeEmployerService = iChangeEmployerService;
            _iAuditTrailService = AuditTrailService;
        }

        #region View function
        // [AuthorizeFilter("changeEmployer:view")]
        public IActionResult ChangeEmployerIndex()
        {
            return View();
        }

        public IActionResult ChangeEmployerForm()
        {
            return View();
        }
        #endregion

        #region Get data
        [HttpGet]
        //[AuthorizeFilter("changeEmployer:search,user:search")]
        public async Task<IActionResult> GetListJson(ChangeEmployerListParam param)
        {
            TData<List<ChangeEmployerEntity>> obj = await _iChangeEmployerService.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        //[AuthorizeFilter("changeEmployer:search,user:search")]
        public async Task<IActionResult> GetChangeEmployerPageListJson(ChangeEmployerListParam param, Pagination pagination)
        {
            TData<List<ChangeEmployerEntity>> obj = await _iChangeEmployerService.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        //[AuthorizeFilter("ChangeEmployer:search,user:search")]
        public async Task<IActionResult> GetCompanyTreeListJson(ChangeEmployerListParam param)
        {
            TData<List<ZtreeInfo>> obj = await _iChangeEmployerService.GetZtreeChangeEmployerList(param);
            return Json(obj);
        }

        [HttpGet]
        // [AuthorizeFilter("changeEmployer:view")]
        public async Task<IActionResult> GetUserTreeListJson(ChangeEmployerListParam param)
        {
            TData<List<ZtreeInfo>> obj = await _iChangeEmployerService.GetZtreeUserList(param);
            return Json(obj);
        }

        [HttpGet]
        // [AuthorizeFilter("changeEmployer:view")]
        public async Task<IActionResult> GetFormJson(int id)
        {
            TData<ChangeEmployerEntity> obj = await _iChangeEmployerService.GetEntity(id);
            return Json(obj);
        }

        [HttpGet]
        public async Task<IActionResult> GetMaxSortJson()
        {
            TData<int> obj = await _iChangeEmployerService.GetMaxSort();
            return Json(obj);
        }

        [HttpGet]
        //[AuthorizeFilter("refund:view")]
        public async Task<IActionResult> ViewCompanyName()
        {
            var auditInstance = new AuditTrailEntity();
            auditInstance.Action = SystemOperationCode.ViewCompanyName.ToString();
            auditInstance.ActionRoute = SystemOperationCode.ChangeEmployer.ToString();

            var audit = await _iAuditTrailService.SaveForm(auditInstance);
            TData<CustomerDetailsViewModel> obj = await _iChangeEmployerService.GetCompanyName();
            return Json(obj);
        }
        #endregion

        #region Submit data
        [HttpPost]
        // [AuthorizeFilter("changeEmployer:add,company:edit")]
        public async Task<IActionResult> SaveFormJson(ChangeEmployerEntity entity)
        {
            TData<string> obj = await _iChangeEmployerService.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        // [AuthorizeFilter("changeEmployer:add,employee:edit")]
        public async Task<IActionResult> UpdateFormJson(ChangeEmployerEntity entity)
        {
            TData<string> obj = await _iChangeEmployerService.UpdateForm(entity);
            return Json(obj);
        }

        [HttpPost]
        //  [AuthorizeFilter("changeEmployer:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await _iChangeEmployerService.DeleteForm(ids);
            return Json(obj);
        }


        #endregion
    }
}
