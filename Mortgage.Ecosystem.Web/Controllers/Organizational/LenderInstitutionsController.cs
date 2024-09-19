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
    public class LenderInstitutionsController : BaseController
    {
        private readonly ILenderInstitutionsService _iPmbService;
        private readonly IAuditTrailService _iAuditTrailService;
        public LenderInstitutionsController(IUnitOfWork iUnitOfWork, ILenderInstitutionsService iPmbService, IAuditTrailService iAuditTrailService) : base(iUnitOfWork)
        {
            _iPmbService = iPmbService;
            _iAuditTrailService = iAuditTrailService;
        }

        #region View function
        [AuthorizeFilter("pmbapproval:view")]
        public IActionResult LenderInstitutionApprovalIndex()
        {
            return View();
        }

        public IActionResult PmbApprovalForm()
        {
            return View();
        }

        
        [AuthorizeFilter("lender:view")]
        public IActionResult LenderInstitutionIndex()
        {
            return View();
        }

        public IActionResult PmbForm()
        {
            return View();
        }

         public IActionResult LenderInstitutionEmployeeForm()
        {
            return View();
        }

        [AuthorizeFilter("lenderEmployee:view")]
        public IActionResult LenderInstitutionEmployeeIndex()
        {
            return View();
        }



        #endregion

        #region Get data
        [HttpGet]
        [AuthorizeFilter("pmb:search,user:search")]
        public async Task<IActionResult> GetListJson(PmbListParam param)
        {
            TData<List<LenderInstitutionsEntity>> obj = await _iPmbService.GetList(param);
            return Json(obj);
        }
        public async Task<IActionResult> GetNonNhfListJson(PmbListParam param)
        {
            TData<List<NonNhf>> obj = await _iPmbService.GetNonNhfList(param);
            return Json(obj);
        }

        //[HttpGet]
        //[AuthorizeFilter("company:search,user:search")]
        //public async Task<IActionResult> GetApproveCompanyListJson(CompanyListParam param)
        //{
        //    TData<List<CompanyEntity>> obj = await _iCompanyService.GetApproveCompanyList(param);
        //    return Json(obj);
        //}

        [HttpGet]
       //[AuthorizeFilter("pmb:search,user:search")]
        public async Task<IActionResult> GetPageListJson(PmbListParam param, Pagination pagination)
        {
            TData<List<LenderInstitutionsEntity>> obj = await _iPmbService.GetPageList(param, pagination);
            return Json(obj);
        }


        [HttpGet]
        //[AuthorizeFilter("pmb:search,user:search")]
        public async Task<IActionResult> GetPmbEmployee(EmployeeListParam param)
        {
            TData<List<EmployeeEntity>> obj = await _iPmbService.GetPmbEmployee(param);
            var auditInstance = new AuditTrailEntity();
            auditInstance.Action = SystemOperationCode.GetPmbEmployee.ToString();
            auditInstance.ActionRoute = SystemOperationCode.PMB.ToString();

            var audit = await _iAuditTrailService.SaveForm(auditInstance);

            return Json(obj);
        }

        [HttpGet]
        //[AuthorizeFilter("company:search,user:search")]
        public async Task<IActionResult> GetApprovalPageListJson(PmbListParam param, Pagination pagination)
        {
            TData<List<LenderInstitutionsEntity>> obj = await _iPmbService.GetApprovalPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("pmb:search,user:search")]
        public async Task<IActionResult> GetPmbTreeListJson(PmbListParam param)
        {
            TData<List<ZtreeInfo>> obj = await _iPmbService.GetZtreePmbList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("pmb:view")]
        public async Task<IActionResult> GetUserTreeListJson(PmbListParam param)
        {
            TData<List<ZtreeInfo>> obj = await _iPmbService.GetZtreeUserList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("pmb:view")]
        public async Task<IActionResult> GetFormJson(int id)
        {
            TData<LenderInstitutionsEntity> obj = await _iPmbService.GetEntity(id);
            return Json(obj);
        }
        #endregion

        #region Submit data
        

        [HttpPost]
       [AuthorizeFilter("pmb:add,pmb:edit")]
        public async Task<IActionResult> SaveFormsJson(LenderInstitutionsEntity entity)
        {
            try
            {
                TData<string> obj = await _iPmbService.SaveForms(entity);
                return Json(obj);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        [HttpPost]
        [AuthorizeFilter("pmb:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await _iPmbService.DeleteForm(ids);
            return Json(obj);
        }

        [HttpPost]
        //[AuthorizeFilter("pmb:add,pmb:edit")]
        public async Task<IActionResult> ApproveFormJson(LenderInstitutionsEntity entity)
        {
            try
            {
                TData obj = await _iPmbService.ApproveForm(entity);
                return Json(obj);
            }
            catch (Exception e)
            {

                throw;
            }
        }


        [HttpPost]
        [AuthorizeFilter("pmb:add,pmb:edit")]
        public async Task<IActionResult> DisApproveFormJson(LenderInstitutionsEntity entity, string Remark)
        {
            try
            {
                TData obj = await _iPmbService.DisApproveForm(entity, Remark);
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
                TData<string> obj = await _iPmbService.SaveNewEmployee(entity);
                var auditInstance = new AuditTrailEntity();
                auditInstance.Action = SystemOperationCode.SaveNewEmployee.ToString();
                auditInstance.ActionRoute = SystemOperationCode.PMB.ToString();

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