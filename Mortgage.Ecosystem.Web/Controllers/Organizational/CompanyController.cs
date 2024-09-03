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
    public class CompanyController : BaseController
    {
        private readonly ICompanyService _iCompanyService;
        private readonly IAuditTrailService _iAuditTrailService;
        public CompanyController(IUnitOfWork iUnitOfWork, ICompanyService iCompanyService, IAuditTrailService iAuditTrailService) : base(iUnitOfWork)
        {
            _iCompanyService = iCompanyService;
            _iAuditTrailService = iAuditTrailService;
        }

        #region View function
        //[AuthorizeFilter("company:view")]
        public IActionResult CompanyIndex()
        {
            return View();
        }

        public IActionResult CompanyForm()
        {
            return View();
        }

        [AuthorizeFilter("nhfdiaspora:view")]
        public IActionResult NhfDiasporaIndex()
        {
            return View();
        }

        public IActionResult NhfDiasporaForm()
        {
            return View();
        }

        [AuthorizeFilter("nhfregcompany:view")]
        public IActionResult NHFRegCompanyIndex()
        {
            return View();
        }

        public IActionResult NHFRegCompanyForm()
        {
            return View();
        }

        #endregion

        #region Get data
        [HttpGet]
        [AuthorizeFilter("company:search,user:search")]
        public async Task<IActionResult> GetListJson(CompanyListParam param)
        {
            TData<List<CompanyEntity>> obj = await _iCompanyService.GetList(param);
            return Json(obj);
        }


        public async Task<IActionResult> GetCurrentCompany(CompanyListParam param)
        {
            try
            {
                TData<List<CompanyEntity>> obj = await _iCompanyService.GetCurrentCompany(param);
                var auditInstance = new AuditTrailEntity();
                auditInstance.Action = SystemOperationCode.GetCurrentCompany.ToString();
                auditInstance.ActionRoute = SystemOperationCode.Company.ToString();

                var audit = await _iAuditTrailService.SaveForm(auditInstance);
                return Json(obj);
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        [HttpGet]
        [AuthorizeFilter("company:search,user:search")]
        public async Task<IActionResult> GetPageListJson(CompanyListParam param, Pagination pagination)
        {
            TData<List<CompanyEntity>> obj = await _iCompanyService.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("company:search,user:search")]
        public async Task<IActionResult> GetApprovalPageListJson(CompanyListParam param, Pagination pagination)
        {
            try
            {
                TData<List<CompanyEntity>> obj = await _iCompanyService.GetApprovalPageList(param, pagination);
                var auditInstance = new AuditTrailEntity();
                auditInstance.Action = SystemOperationCode.GetApprovalPageListJson.ToString();
                auditInstance.ActionRoute = SystemOperationCode.Company.ToString();

                var audit = await _iAuditTrailService.SaveForm(auditInstance);
                return Json(obj);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpGet]
        //[AuthorizeFilter("company:search,user:search")]
        public async Task<IActionResult> GetCompanyTreeListJson(CompanyListParam param)
        {
            TData<List<ZtreeInfo>> obj = await _iCompanyService.GetZtreeCompanyList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("company:view")]
        public async Task<IActionResult> GetUserTreeListJson(CompanyListParam param)
        {
            TData<List<ZtreeInfo>> obj = await _iCompanyService.GetZtreeUserList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("company:view")]
        public async Task<IActionResult> GetFormJson(int id)
        {
            TData<CompanyEntity> obj = await _iCompanyService.GetEntity(id);
            return Json(obj);
        }
        #endregion

        #region Submit data
        [HttpPost]
        [AuthorizeFilter("company:add,company:edit")]
        public async Task<IActionResult> SaveFormJson(CompanyEntity entity)
        {
            try
            {
                TData<string> obj = await _iCompanyService.SaveForm(entity);
                var auditInstance = new AuditTrailEntity();
                auditInstance.Action = SystemOperationCode.RegisterCompany.ToString();
                auditInstance.ActionRoute = SystemOperationCode.Company.ToString();

                var audit = await _iAuditTrailService.SaveForm(auditInstance);
                return Json(obj);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpPost]
        //[AuthorizeFilter("company:add,company:edit")]
        public async Task<IActionResult> SaveFormsJson(CompanyEntity entity)
        {
            try
            {
                TData<string> obj = await _iCompanyService.SaveForms(entity);
                var auditInstance = new AuditTrailEntity();
                auditInstance.Action = SystemOperationCode.RegisterCompany.ToString();
                auditInstance.ActionRoute = SystemOperationCode.Company.ToString();
                auditInstance.UserName = entity.Id.ToString();
                var audit = await _iAuditTrailService.SaveForm(auditInstance);
                return Json(obj);

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpPost]
        [AuthorizeFilter("company:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await _iCompanyService.DeleteForm(ids);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("company:add,company:edit")]
        public async Task<IActionResult> ApproveFormJson(CompanyEntity entity)
        {
            try
            {
                TData obj = await _iCompanyService.ApproveForm(entity);
                var auditInstance = new AuditTrailEntity();
                auditInstance.Action = SystemOperationCode.ApproveFormJson.ToString();
                auditInstance.ActionRoute = SystemOperationCode.Company.ToString();

                var audit = await _iAuditTrailService.SaveForm(auditInstance);

                return Json(obj);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpPost]
        //[AuthorizeFilter("company:add,company:edit")]
        public async Task<IActionResult> RejectFormJson(CompanyEntity entity, string Remark)
        {
            try
            {
                TData obj = await _iCompanyService.RejectForm(entity, Remark);
                var auditInstance = new AuditTrailEntity();
                auditInstance.Action = SystemOperationCode.RejectFormJson.ToString();
                auditInstance.ActionRoute = SystemOperationCode.Company.ToString();

                var audit = await _iAuditTrailService.SaveForm(auditInstance);
                return Json(obj);
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public async Task<IActionResult> GetCompanyInfo()
        {
            TData<CustomerDetailsViewModel> obj = await _iCompanyService.GetCompanyInfo();
            var auditInstance = new AuditTrailEntity();
            auditInstance.Action = SystemOperationCode.GetCompanyInfo.ToString();
            auditInstance.ActionRoute = SystemOperationCode.Company.ToString();

            var audit = await _iAuditTrailService.SaveForm(auditInstance);
            return Json(obj);
        }
        #endregion
    }
}