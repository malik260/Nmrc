﻿using Microsoft.AspNetCore.Mvc;
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
    public class FeedBackFormController : BaseController
    {
        private readonly IFeedBackFormService _iFeedBackFormService;
        private readonly IAuditTrailService _iAuditTrailService;
        public FeedBackFormController(IUnitOfWork iUnitOfWork, IFeedBackFormService iFeedBackFormService, IAuditTrailService iAuditTrailService) : base(iUnitOfWork)
        {
            _iFeedBackFormService = iFeedBackFormService;
            _iAuditTrailService = iAuditTrailService;
        }

        #region View function
        [AuthorizeFilter("feedbackform:view")]
        public IActionResult FeedBackFormIndex()
        {
            return View();
        }

        public IActionResult FeedBackFormForm()
        {
            return View();
        }

        public IActionResult AdminFeedBackFormIndex()
        {
            return View();
        }

        [AuthorizeFilter("adminfeedbackform:view")]

        public IActionResult FeedBackFormDetails()
        {
            return View();
        }
        #endregion

        #region Get data
        [HttpGet]
        //   [AuthorizeFilter("feedbackform:search,user:search")]
        public async Task<IActionResult> GetListJson(FeedBackFormListParam param)
        {
            TData<List<FeedBackFormEntity>> obj = await _iFeedBackFormService.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        //  [AuthorizeFilter("feedbackform:search,user:search")]
        public async Task<IActionResult> GetFeedBackFormTreeListJson(FeedBackFormListParam param)
        {
            TData<List<ZtreeInfo>> obj = await _iFeedBackFormService.GetZtreeFeedBackFormList(param);
            return Json(obj);
        }


        [HttpGet]
        //  [AuthorizeFilter("feedbackform:search,user:search")]
        public async Task<IActionResult> GetFeedBackFormPageListJson(FeedBackFormListParam param, Pagination pagination)
        {
            TData<List<FeedBackFormEntity>> obj = await _iFeedBackFormService.GetPageList(param, pagination);

            var auditInstance = new AuditTrailEntity();
            auditInstance.Action = SystemOperationCode.GetFeedBackFormPageListJson.ToString();
            auditInstance.ActionRoute = SystemOperationCode.FeedBack.ToString();

            var audit = await _iAuditTrailService.SaveForm(auditInstance);
            return Json(obj);
        }

        [HttpGet]
        //  [AuthorizeFilter("feedbackform:search,user:search")]
        public async Task<IActionResult> GetEmployeeFeedBackFormPageListJson(FeedBackFormListParam param, Pagination pagination)
        {
            TData<List<FeedBackFormEntity>> obj = await _iFeedBackFormService.GetEmployeePageList(param, pagination);

            var auditInstance = new AuditTrailEntity();
            auditInstance.Action = SystemOperationCode.GetFeedBackFormPageListJson.ToString();
            auditInstance.ActionRoute = SystemOperationCode.FeedBack.ToString();

            var audit = await _iAuditTrailService.SaveForm(auditInstance);
            return Json(obj);
        }

        [HttpGet]
        //   [AuthorizeFilter("feedbackform:view")]
        public async Task<IActionResult> GetUserTreeListJson(FeedBackFormListParam param)
        {
            TData<List<ZtreeInfo>> obj = await _iFeedBackFormService.GetZtreeUserList(param);
            return Json(obj);
        }

        [HttpGet]
        //   [AuthorizeFilter("feedbackform:view")]
        public async Task<IActionResult> GetFormJson(long id)
        {
            TData<FeedBackFormEntity> obj = await _iFeedBackFormService.GetEntity(id);
            return Json(obj);
        }

        [HttpGet]
        public async Task<IActionResult> GetMaxSortJson()
        {
            TData<int> obj = await _iFeedBackFormService.GetMaxSort();
            return Json(obj);
        }

        public async Task<IActionResult> GetEmployeeDetails()
        {
            try
            {
                var auditInstance = new AuditTrailEntity();
                auditInstance.Action = SystemOperationCode.GetEmployeeDetails.ToString();
                //   auditInstance.ActionRoute = SystemOperationCode.Contribution.ToString();

                var audit = await _iAuditTrailService.SaveForm(auditInstance);
                TData obj = await _iFeedBackFormService.GetCustomerDetails();
                return Json(obj);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        #endregion

        #region Submit data
        [HttpPost]
        //  [AuthorizeFilter("feedbackform:add,company:edit")]
        public async Task<IActionResult> SaveFormJson(FeedBackFormEntity entity)
        {
            TData<string> obj = await _iFeedBackFormService.SaveForm(entity);
            return Json(obj);
        }

        public async Task<IActionResult> SaveFormDetails(FeedBackFormEntity entity)
        {
            TData obj = await _iFeedBackFormService.SaveForms(entity);
            return Json(obj);
        }

        [HttpPost]
        //   [AuthorizeFilter("feedbackform:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await _iFeedBackFormService.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}