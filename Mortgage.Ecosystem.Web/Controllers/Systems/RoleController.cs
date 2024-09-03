using Microsoft.AspNetCore.Mvc;
using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Enums;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;
using Mortgage.Ecosystem.Web.Filter;

namespace Mortgage.Ecosystem.Web.Controllers.Systems
{
    [ExceptionFilter]
    public class RoleController : BaseController
    {
        private readonly IRoleService _iRoleService;
        private readonly IAuditTrailService _iAuditTrailService;

        public RoleController(IUnitOfWork iUnitOfWork, IRoleService iRoleService, IAuditTrailService iAuditTrailService) : base(iUnitOfWork)
        {
            _iRoleService = iRoleService;
            _iAuditTrailService = iAuditTrailService;
        }

        #region View function
        [AuthorizeFilter("role:view")]
        public IActionResult RoleIndex()
        {
            return View();
        }

        [AuthorizeFilter("role:view")]
        public IActionResult RoleForm()
        {
            return View();
        }
        #endregion

        #region Get data
        [HttpGet]
        [AuthorizeFilter("role:search,user:search")]
        public async Task<IActionResult> GetListJson(RoleListParam param)
        {
            TData<List<RoleEntity>> obj = await _iRoleService.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("role:search,user:search")]
        public async Task<IActionResult> GetPageListJson(RoleListParam param, Pagination pagination)
        {
            TData<List<RoleEntity>> obj = await _iRoleService.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("role:search,user:search")]
        public async Task<IActionResult> GetRoleTreeListJson(RoleListParam param)
        {
            TData<List<ZtreeInfo>> obj = await _iRoleService.GetZtreeRoleList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("role:view")]
        public async Task<IActionResult> GetFormJson(long id)
        {
            TData<RoleEntity> obj = await _iRoleService.GetEntity(id);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("role:view")]
        public async Task<IActionResult> GetRoleName(RoleListParam param)
        {
            TData<string> obj = new TData<string>();
            var list = await _iRoleService.GetList(param);
            if (list.Tag == 1)
            {
                obj.Data = string.Join(",", list.Data.Select(p => p.RoleName));
                obj.Tag = 1;
            }

            var auditInstance = new AuditTrailEntity();
            auditInstance.Action = SystemOperationCode.GetRoleName.ToString();
            auditInstance.ActionRoute = SystemOperationCode.Role.ToString();

            var audit = await _iAuditTrailService.SaveForm(auditInstance);
            return Json(obj);
        }

        [HttpGet]
        public async Task<IActionResult> GetMaxSortJson()
        {
            TData<int> obj = await _iRoleService.GetMaxSort();
            return Json(obj);
        }
        #endregion

        #region Submit data
        [HttpPost]
        [AuthorizeFilter("role:add,role:edit")]
        public async Task<IActionResult> SaveFormJson(RoleEntity entity)
        {
            TData<string> obj = await _iRoleService.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("role:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await _iRoleService.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}