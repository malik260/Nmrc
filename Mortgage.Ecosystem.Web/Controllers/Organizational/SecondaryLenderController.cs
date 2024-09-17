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
    public class SecondaryLenderController : BaseController
    {
        private readonly ISecondaryLenderService _iSecondaryLenderService;
        private readonly IAuditTrailService _iAuditTrailService;
        public SecondaryLenderController(IUnitOfWork iUnitOfWork, ISecondaryLenderService iSecondaryLenderService, IAuditTrailService iAuditTrailService) : base(iUnitOfWork)
        {
            _iSecondaryLenderService = iSecondaryLenderService;
            _iAuditTrailService = iAuditTrailService;
        }

        #region View function
        [AuthorizeFilter("secondarylenderapproval:view")]
        public IActionResult SecondaryLenderApprovalIndex()
        {
            return View();
        }

        public IActionResult SecondaryLenderApprovalForm()
        {
            return View();
        }

        
        [AuthorizeFilter("secondarylender:view")]
        public IActionResult SecondaryLenderIndex()
        {
            return View();
        }

        public IActionResult SecondaryLenderForm()
        {
            return View();
        }

         public IActionResult SecondaryLenderEmployeeForm()
        {
            return View();
        }


        [AuthorizeFilter("secondarylenderEmployee:view")]
        public IActionResult SecondaryLenderEmployeeIndex()
        {
            return View();
        }



        #endregion

        #region Get data
        [HttpGet]
        public async Task<IActionResult> GetListJson(SecondaryLenderListParam param)
        {
            TData<List<SecondaryLenderEntity>> obj = await _iSecondaryLenderService.GetList(param);
            return Json(obj);
        }

        [HttpGet]
       //[AuthorizeFilter("pmb:search,user:search")]
        public async Task<IActionResult> GetPageListJson(SecondaryLenderListParam param, Pagination pagination)
        {
            TData<List<SecondaryLenderEntity>> obj = await _iSecondaryLenderService.GetPageList(param, pagination);
            return Json(obj);
        }


        [HttpGet]
        //[AuthorizeFilter("pmb:search,user:search")]
        public async Task<IActionResult> GetSecondaryLenderEmployee(EmployeeListParam param)
        {
            TData<List<EmployeeEntity>> obj = await _iSecondaryLenderService.GetSecondaryLenderEmployee(param);
            var auditInstance = new AuditTrailEntity();
            auditInstance.Action = SystemOperationCode.GetPmbEmployee.ToString();
            auditInstance.ActionRoute = SystemOperationCode.PMB.ToString();

            var audit = await _iAuditTrailService.SaveForm(auditInstance);

            return Json(obj);
        }

        [HttpGet]
        //[AuthorizeFilter("company:search,user:search")]
        public async Task<IActionResult> GetApprovalPageListJson(SecondaryLenderListParam param, Pagination pagination)
        {
            TData<List<SecondaryLenderEntity>> obj = await _iSecondaryLenderService.GetApprovalPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("pmb:search,user:search")]
        public async Task<IActionResult> GetPmbTreeListJson(SecondaryLenderListParam param)
        {
            TData<List<ZtreeInfo>> obj = await _iSecondaryLenderService.GetZtreeSecondaryLenderList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("pmb:view")]
        public async Task<IActionResult> GetUserTreeListJson(SecondaryLenderListParam param)
        {
            TData<List<ZtreeInfo>> obj = await _iSecondaryLenderService.GetZtreeUserList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("pmb:view")]
        public async Task<IActionResult> GetFormJson(int id)
        {
            TData<SecondaryLenderEntity> obj = await _iSecondaryLenderService.GetEntity(id);
            return Json(obj);
        }
        #endregion

        #region Submit data
        

        [HttpPost]
       [AuthorizeFilter("secondarylender:add,secondarylender:edit")]
        public async Task<IActionResult> SaveFormsJson(SecondaryLenderEntity entity)
        {
            try
            {
                TData<string> obj = await _iSecondaryLenderService.SaveForms(entity);
                return Json(obj);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        [HttpPost]
        [AuthorizeFilter("secondarylender:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await _iSecondaryLenderService.DeleteForm(ids);
            return Json(obj);
        }

        [HttpPost]
        //[AuthorizeFilter("pmb:add,pmb:edit")]
        public async Task<IActionResult> ApproveFormJson(SecondaryLenderEntity entity)
        {
            try
            {
                TData obj = await _iSecondaryLenderService.ApproveForm(entity);
                return Json(obj);
            }
            catch (Exception e)
            {

                throw;
            }
        }


        [HttpPost]
        [AuthorizeFilter("pmb:add,pmb:edit")]
        public async Task<IActionResult> DisApproveFormJson(SecondaryLenderEntity entity, string Remark)
        {
            try
            {
                TData obj = await _iSecondaryLenderService.DisApproveForm(entity, Remark);
                var auditInstance = new AuditTrailEntity();
                auditInstance.Action = SystemOperationCode.DisApproveFormJson.ToString();
                auditInstance.ActionRoute = SystemOperationCode.PMB.ToString();

                var audit = await _iAuditTrailService.SaveForm(auditInstance);
                return Json(obj);
            }
            catch (Exception e)
            {

                throw;
            }
        }




        [HttpPost]
        //[AuthorizeFilter("employee:add,employee:edit")]
        public async Task<IActionResult> SaveNewEmployee(EmployeeEntity entity)
        {
            try
            {
                TData<string> obj = await _iSecondaryLenderService.SaveNewEmployee(entity);
                var auditInstance = new AuditTrailEntity();
                auditInstance.Action = SystemOperationCode.SaveNewEmployee.ToString();
                auditInstance.ActionRoute = SystemOperationCode.SecondaryLender.ToString();

                var audit = await _iAuditTrailService.SaveForm(auditInstance);
                return Json(obj);
            }
            catch (Exception e)
            {

                throw;
            }
        }
        #endregion
    }
}