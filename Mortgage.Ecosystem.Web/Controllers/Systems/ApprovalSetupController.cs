﻿using Microsoft.AspNetCore.Mvc;
using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Enums;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;
using Mortgage.Ecosystem.Web.Filter;

namespace Mortgage.Ecosystem.Web.Controllers.Systems
{
    [ExceptionFilter]
    public class ApprovalSetupController : BaseController
    {
        private readonly IApprovalSetupService _iApprovalSetupService;
        private readonly IAuditTrailService _iAuditTrailService;
        public ApprovalSetupController(IUnitOfWork iUnitOfWork, IApprovalSetupService iApprovalSetupService, IAuditTrailService iAuditTrailService) : base(iUnitOfWork)
        {
            _iApprovalSetupService = iApprovalSetupService;
            _iAuditTrailService = iAuditTrailService;
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


        [AuthorizeFilter("pmboperationsetup:view")]
        public IActionResult PmbOperationSetupForm()
        {
            return View();
        }

        [AuthorizeFilter("pmboperationsetup:view")]
        public IActionResult PmbOperationSetupIndex()
        {
            return View();
        }

        [AuthorizeFilter("secondarylenderoperationsetup:view")]
        public IActionResult SecondaryLenderOperationSetupIndex()
        {
            return View();
        }

         public IActionResult SecondaryLenderOperationSetupForm()
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
            try
            {
                TData<string> obj = new TData<string>();
                var list = await _iApprovalSetupService.GetList(param);
                if (list.Tag == 1)
                {
                    obj.Data = string.Join(",", list.Data.Select(p => p.MenuId));
                    obj.Tag = 1;
                }
                var auditInstance = new AuditTrailEntity();
                auditInstance.Action = SystemOperationCode.GetApprovalSetupName.ToString();
                auditInstance.ActionRoute = SystemOperationCode.ApprovalSetup.ToString();

                var audit = await _iAuditTrailService.SaveForm(auditInstance);
                return Json(obj);
            }
            catch (Exception e)
            {

                throw;
            }
        }
        #endregion

        #region Submit data
        [HttpPost]
        //[AuthorizeFilter("approvalsetup:add,approvalsetup:edit")]
        public async Task<IActionResult> SaveFormJson(ApprovalSetupEntity entity)
        {
            TData<string> obj = await _iApprovalSetupService.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        //[AuthorizeFilter("approvalsetup:add,approvalsetup:edit")]
        public async Task<IActionResult> SaveFormJson2(ApprovalSetupEntity entity)
        {
            TData<string> obj = await _iApprovalSetupService.SaveForm2(entity);
            return Json(obj);
        }
          public async Task<IActionResult> SaveFormJson3(ApprovalSetupEntity entity)
        {
            TData<string> obj = await _iApprovalSetupService.SaveForm3(entity);
            return Json(obj);
        }

        [HttpPost]
        //[AuthorizeFilter("approvalsetup:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await _iApprovalSetupService.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}