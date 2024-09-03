using Microsoft.AspNetCore.Mvc;
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
    public class AccountTypeController : BaseController
    {
        private readonly IAccountTypeService _iAccountTypeService;
        private readonly IAuditTrailService _iAuditTrailService;
        public AccountTypeController(IUnitOfWork iUnitOfWork, IAccountTypeService iAccountTypeService, IAuditTrailService iAuditTrailService) : base(iUnitOfWork)
        {
            _iAccountTypeService = iAccountTypeService;
            _iAuditTrailService = iAuditTrailService;
        }

        #region View function
        [AuthorizeFilter("accounttype:view")]
        public IActionResult AccountTypeIndex()
        {
            return View();
        }

        [AuthorizeFilter("accounttype:view")]
        public IActionResult AccountTypeForm()
        {
            return View();
        }
        #endregion

        #region Get data
        [HttpGet]
        //[AuthorizeFilter("accounttype:search,user:search")]
        public async Task<IActionResult> GetListJson(AccountTypeListParam param)
        {
            TData<List<AccountTypeEntity>> obj = await _iAccountTypeService.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("accounttype:search,user:search")]
        public async Task<IActionResult> GetPageListJson(AccountTypeListParam param, Pagination pagination)
        {
            TData<List<AccountTypeEntity>> obj = await _iAccountTypeService.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("accounttype:view")]
        public async Task<IActionResult> GetFormJson(long id)
        {
            TData<AccountTypeEntity> obj = await _iAccountTypeService.GetEntity(id);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("accounttype:view")]
        public async Task<IActionResult> GetAccountTypeName(AccountTypeListParam param)
        {
            TData<string> obj = new TData<string>();
            var list = await _iAccountTypeService.GetList(param);
            if (list.Tag == 1)
            {
                obj.Data = string.Join(",", list.Data.Select(p => p.Name));
                obj.Tag = 1;
            }
            var auditInstance = new AuditTrailEntity();
            auditInstance.Action = SystemOperationCode.GetAccountTypeName.ToString();
            auditInstance.ActionRoute = SystemOperationCode.AccountType.ToString();

            var audit = await _iAuditTrailService.SaveForm(auditInstance);
            return Json(obj);
        }
        #endregion

        #region Submit data
        [HttpPost]
        [AuthorizeFilter("accounttype:add,accounttype:edit")]
        public async Task<IActionResult> SaveFormJson(AccountTypeEntity entity)
        {
            TData<string> obj = await _iAccountTypeService.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("accounttype:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await _iAccountTypeService.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}