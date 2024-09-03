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
    public class DeveloperController : BaseController
    {
        private readonly IDeveloperService _iDeveloperService;
        private readonly IAuditTrailService _iAuditTrailService;
        public DeveloperController(IUnitOfWork iUnitOfWork, IDeveloperService ideveloperService, IAuditTrailService iAuditTrailService) : base(iUnitOfWork)
        {
            _iDeveloperService = ideveloperService;
            _iAuditTrailService = iAuditTrailService;
        }

        #region View function
        [AuthorizeFilter("developerapproval:view")]
        public IActionResult DeveloperApprovalIndex()
        {
            return View();
        }

        public IActionResult DeveloperApprovalForm()
        {
            return View();
        }


        [AuthorizeFilter("developer:view")]
        public IActionResult DeveloperIndex()
        {
            return View();
        }

        public IActionResult DeveloperForm()
        {
            return View();
        }


        #endregion

        #region Get data
        [HttpGet]
        [AuthorizeFilter("developer:search,user:search")]
        public async Task<IActionResult> GetListJson(DeveloperListParam param)
        {
            TData<List<DeveloperEntity>> obj = await _iDeveloperService.GetList(param);
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
        [AuthorizeFilter("developer:search,user:search")]
        public async Task<IActionResult> GetPageListJson(DeveloperListParam param, Pagination pagination)
        {
            TData<List<DeveloperEntity>> obj = await _iDeveloperService.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        //[AuthorizeFilter("company:search,user:search")]
        public async Task<IActionResult> GetApprovalPageListJson(DeveloperListParam param, Pagination pagination)
        {
            TData<List<DeveloperEntity>> obj = await _iDeveloperService.GetApprovalPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("developer:search,user:search")]
        public async Task<IActionResult> GetPmbTreeListJson(DeveloperListParam param)
        {
            TData<List<ZtreeInfo>> obj = await _iDeveloperService.GetZtreePmbList(param);
            var auditInstance = new AuditTrailEntity();
            auditInstance.Action = SystemOperationCode.GetPmbTreeListJson.ToString();
            auditInstance.ActionRoute = SystemOperationCode.Developer.ToString();

            var audit = await _iAuditTrailService.SaveForm(auditInstance);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("developer:view")]
        public async Task<IActionResult> GetUserTreeListJson(DeveloperListParam param)
        {
            TData<List<ZtreeInfo>> obj = await _iDeveloperService.GetZtreeUserList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("developer:view")]
        public async Task<IActionResult> GetFormJson(int id)
        {
            TData<DeveloperEntity> obj = await _iDeveloperService.GetEntity(id);
            return Json(obj);
        }
        #endregion

        #region Submit data


        [HttpPost]
        [AuthorizeFilter("developer:add,developer:edit")]
        public async Task<IActionResult> SaveFormsJson(DeveloperEntity entity)
        {
            TData<string> obj = await _iDeveloperService.SaveForms(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("developer:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await _iDeveloperService.DeleteForm(ids);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("developer:add,developer:edit")]
        public async Task<IActionResult> ApproveFormJson(DeveloperEntity entity)
        {
            TData obj = await _iDeveloperService.ApproveForm(entity);
            return Json(obj);
        }
        #endregion

    }
}
