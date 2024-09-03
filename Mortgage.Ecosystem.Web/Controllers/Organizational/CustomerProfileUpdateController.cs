using Microsoft.AspNetCore.Mvc;
using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.BusinessLogic.Layer.Services;
using Mortgage.Ecosystem.DataAccess.Layer.Enums;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Operator;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;
using Mortgage.Ecosystem.Web.Filter;

namespace Mortgage.Ecosystem.Web.Controllers.Organizational
{
    public class CustomerProfileUpdateController : BaseController
    {
        private readonly ICustomerProfileUpdateService _iCustomerProfileUpdateService;
        private readonly IAuditTrailService _iAuditTrailService;
        public CustomerProfileUpdateController(IUnitOfWork iUnitOfWork, ICustomerProfileUpdateService iCustomerProfileUpdateService, IAuditTrailService iAuditTrailService) : base(iUnitOfWork)
        {
            _iCustomerProfileUpdateService = iCustomerProfileUpdateService;
            _iAuditTrailService = iAuditTrailService;
        }

        #region View function
        //[AuthorizeFilter("customerprofileupdate:view")]
        public IActionResult CustomerProfileUpdateIndex()
        {
            return View();
        }

        public IActionResult CustomerProfileUpdateForm()
        {
            return View();
        }

        [AuthorizeFilter("customerupdateapproval:view")]
        public IActionResult CustomerUpdateApprovalIndex()
        {
            return View();
        }
        #endregion

        #region Get data
        [HttpGet]
        [AuthorizeFilter("customerprofileupdate:search,user:search")]
        public async Task<IActionResult> GetListJson(CustomerProfileUpdateListParam param)
        {
            TData<List<CustomerProfileUpdateEntity>> obj = await _iCustomerProfileUpdateService.GetList(param);
            return Json(obj);
        }


        [HttpGet]
        [AuthorizeFilter("customerprofileupdate:search,user:search")]
        public async Task<IActionResult> GetCustomerProfileUpdatePageListJson(CustomerProfileUpdateListParam param, Pagination pagination)
        {
            TData<List<CustomerProfileUpdateEntity>> obj = await _iCustomerProfileUpdateService.GetPageList(param, pagination);
            var auditInstance = new AuditTrailEntity();
            auditInstance.Action = SystemOperationCode.GetCustomerProfileUpdatePageListJson.ToString();
            auditInstance.ActionRoute = SystemOperationCode.ChangeEmployer.ToString();

            var audit = await _iAuditTrailService.SaveForm(auditInstance);
            return Json(obj);
        }

        [HttpGet]
        //[AuthorizeFilter("employee:search,user:search")]
        public async Task<IActionResult> GetApprovalPageListJson(CustomerProfileUpdateListParam param, Pagination pagination)
        {
            TData<List<CustomerProfileUpdateEntity>> obj = await _iCustomerProfileUpdateService.GetApprovalPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("customerprofileupdate:search,user:search")]
        public async Task<IActionResult> GetCustomerProfileUpdateTreeListJson(CustomerProfileUpdateListParam param)
        {
            TData<List<ZtreeInfo>> obj = await _iCustomerProfileUpdateService.GetZtreeCustomerProfileUpdateList(param);
            var auditInstance = new AuditTrailEntity();
            auditInstance.Action = SystemOperationCode.GetCustomerProfileAwaitingUpdatePageListJson.ToString();
            auditInstance.ActionRoute = SystemOperationCode.ChangeEmployer.ToString();

            var audit = await _iAuditTrailService.SaveForm(auditInstance);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("customerprofileupdate:view")]
        public async Task<IActionResult> GetUserTreeListJson(CustomerProfileUpdateListParam param)
        {
            TData<List<ZtreeInfo>> obj = await _iCustomerProfileUpdateService.GetZtreeUserList(param);
            return Json(obj);
        }

        [HttpGet]
        //AuthorizeFilter("customerprofileupdate:view")]
        public async Task<IActionResult> GetFormJson(int id)
        {
            TData<CustomerProfileUpdateEntity> obj = await _iCustomerProfileUpdateService.GetEntity(id);
            return Json(obj);
        }

        [HttpGet]
        //AuthorizeFilter("customerprofileupdate:view")]
        public async Task<IActionResult> GetFormsJson(long id)
        {
            TData<CustomerProfileUpdateEntity> obj = await _iCustomerProfileUpdateService.GetEntity(id);
            return Json(obj);
        }

        [HttpGet]
        public async Task<IActionResult> GetMaxSortJson()
        {
            TData<int> obj = await _iCustomerProfileUpdateService.GetMaxSort();
            return Json(obj);
        }

        [HttpGet]
        //[AuthorizeFilter("refund:view")]
        public async Task<IActionResult> ViewCustomerInformation()
        {

            TData<CustomerDetailsViewModel> obj = await _iCustomerProfileUpdateService.GetCustomerDetails();
            return Json(obj);
        }

        #endregion

        #region Submit data
        [HttpPost]
        //[AuthorizeFilter("customerprofileupdate:add,customerprofileupdate:edit")]
        public async Task<IActionResult> SaveFormJson(CustomerProfileUpdateEntity entity)
        {
            TData<string> obj = await _iCustomerProfileUpdateService.SaveForm(entity);
            var auditInstance = new AuditTrailEntity();
            auditInstance.Action = SystemOperationCode.UpdateCustomer.ToString();
            auditInstance.ActionRoute = SystemOperationCode.ChangeEmployer.ToString();

            var audit = await _iAuditTrailService.SaveForm(auditInstance);
            return Json(obj);
        }


        //[HttpPost]
        //[AuthorizeFilter("customerprofileupdate:add,employee:edit")]
        //public async Task<IActionResult> SavedFormJson(CustomerProfileUpdateEntity entity)
        //{
        //    TData<string> obj = await _iCustomerProfileUpdateService.UpdateCustomerProfile(entity);
        //    return Json(obj);
        //}

        [HttpPost]
        // [AuthorizeFilter("customerprofileupdate:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await _iCustomerProfileUpdateService.DeleteForm(ids);
            return Json(obj);
        }

        [HttpPost]
        public async Task<IActionResult> ApproveFormJson(CustomerProfileUpdateEntity entity)
        {
            TData obj = await _iCustomerProfileUpdateService.ApproveForm(entity);
            return Json(obj);
        }


        [HttpPost]
        public async Task<IActionResult> RejectFormJson(CustomerProfileUpdateEntity entity, string Remark)
        {
            TData obj = await _iCustomerProfileUpdateService.RejectForm(entity, Remark);
            return Json(obj);
        }
        #endregion
    }
}