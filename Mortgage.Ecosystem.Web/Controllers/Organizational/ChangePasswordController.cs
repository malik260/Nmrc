using Microsoft.AspNetCore.Mvc;
using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.Web.Filter;

namespace Mortgage.Ecosystem.Web.Controllers.Organizational
{
    public class ChangePasswordController : BaseController
    {
        private readonly IChangePasswordService _iChangePasswordService;

        public ChangePasswordController(IUnitOfWork iUnitOfWork, IChangePasswordService iChangePasswordService) : base(iUnitOfWork)
        {
            _iChangePasswordService = iChangePasswordService;
        }

        #region View function
        [AuthorizeFilter("changePassword:view")]
        public IActionResult ChangePasswordIndex()
        {
            return View();
        }

        public IActionResult ChangePasswordForm()
        {
            return View();
        }
        #endregion

        #region Get data
        [HttpGet]
        [AuthorizeFilter("changePassword:search,user:search")]
        public async Task<IActionResult> GetListJson(ChangePasswordListParam param)
        {
            TData<List<ChangePasswordEntity>> obj = await _iChangePasswordService.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("ChangePassword:search,user:search")]
        public async Task<IActionResult> GetCompanyTreeListJson(ChangePasswordListParam param)
        {
            TData<List<ZtreeInfo>> obj = await _iChangePasswordService.GetZtreeChangePasswordList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("changePassword:view")]
        public async Task<IActionResult> GetUserTreeListJson(ChangePasswordListParam param)
        {
            TData<List<ZtreeInfo>> obj = await _iChangePasswordService.GetZtreeUserList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("changePassword:view")]
        public async Task<IActionResult> GetFormJson(int id)
        {
            TData<ChangePasswordEntity> obj = await _iChangePasswordService.GetEntity(id);
            return Json(obj);
        }

        [HttpGet]
        public async Task<IActionResult> GetMaxSortJson()
        {
            TData<int> obj = await _iChangePasswordService.GetMaxSort();
            return Json(obj);
        }
        #endregion

        #region Submit data
        [HttpPost]
        [AuthorizeFilter("changePassword:add,company:edit")]
        public async Task<IActionResult> SaveFormJson(ChangePasswordEntity entity)
        {
            TData<string> obj = await _iChangePasswordService.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("changePassword:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await _iChangePasswordService.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}