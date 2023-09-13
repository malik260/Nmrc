using Microsoft.AspNetCore.Mvc;
using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.BusinessLogic.Layer.Services;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;
using Mortgage.Ecosystem.Web.Filter;

namespace Mortgage.Ecosystem.Web.Controllers.Organizational
{
    public class UnlockAdminUserController : BaseController
    {
        private readonly IUnlockAdminUserService _iUnlockAdminUserService;

        public UnlockAdminUserController(IUnitOfWork iUnitOfWork, IUnlockAdminUserService iUnlockAdminUserService) : base(iUnitOfWork)
        {
            _iUnlockAdminUserService = iUnlockAdminUserService;
        }

        #region View function
        [AuthorizeFilter("unlocknhfportal:view")]
        public IActionResult UnlockAdminUserIndex()
        {
            return View();
        }

        public IActionResult UnlockAdminUserForm()
        {
            return View();
        }
        #endregion

        #region Get data
        [HttpGet]
        [AuthorizeFilter("unlocknhfportal:search,user:search")]
        public async Task<IActionResult> GetListJson(UnlockAdminUserListParam param)
        {
            TData<List<UnlockAdminUserEntity>> obj = await _iUnlockAdminUserService.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("unlocknhfportal:search,user:search")]
        public async Task<IActionResult> GetUnlockAdminUserPageListJson(UnlockAdminUserListParam param, Pagination pagination)
        {
            TData<List<UnlockAdminUserEntity>> obj = await _iUnlockAdminUserService.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("unlocknhfportal:search,user:search")]
        public async Task<IActionResult> GetCompanyTreeListJson(UnlockAdminUserListParam param)
        {
            TData<List<ZtreeInfo>> obj = await _iUnlockAdminUserService.GetZtreeUnlockAdminUserList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("unlocknhfportal:view")]
        public async Task<IActionResult> GetUserTreeListJson(UnlockAdminUserListParam param)
        {
            TData<List<ZtreeInfo>> obj = await _iUnlockAdminUserService.GetZtreeUserList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("unlocknhfportal:view")]
        public async Task<IActionResult> GetFormJson(int id)
        {
            TData<UnlockAdminUserEntity> obj = await _iUnlockAdminUserService.GetEntity(id);
            return Json(obj);
        }

        [HttpGet]
        public async Task<IActionResult> GetMaxSortJson()
        {
            TData<int> obj = await _iUnlockAdminUserService.GetMaxSort();
            return Json(obj);
        }
        #endregion

        #region Submit data
        [HttpPost]
        [AuthorizeFilter("unlocknhfportal:add,company:edit")]
        public async Task<IActionResult> SaveFormJson(UnlockAdminUserEntity entity)
        {
            TData<string> obj = await _iUnlockAdminUserService.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("unlocknhfportal:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await _iUnlockAdminUserService.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}