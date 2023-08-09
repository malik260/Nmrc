using Microsoft.AspNetCore.Mvc;
using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;
using Mortgage.Ecosystem.Web.Filter;

namespace Mortgage.Ecosystem.Web.Controllers.Organizational
{
    public class DiasporaUserController : BaseController
    {
        private readonly IDiasporaUserService _iDiasporaUserService;

        public DiasporaUserController(IUnitOfWork iUnitOfWork, IDiasporaUserService iDiasporaUserService) : base(iUnitOfWork)
        {
            _iDiasporaUserService = iDiasporaUserService;
        }

        #region View function
        [AuthorizeFilter("diasporaUser:view")]
        public IActionResult DiasporaUserIndex()
        {
            return View();
        }

        public IActionResult DiasporaUserForm()
        {
            return View();
        }
        #endregion

        #region Get data
        [HttpGet]
        [AuthorizeFilter("diasporaUser:search,user:search")]
        public async Task<IActionResult> GetListJson(DiasporaUserListParam param)
        {
            TData<List<DiasporaUserEntity>> obj = await _iDiasporaUserService.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("diasporaUser:search,user:search")]
        public async Task<IActionResult> GetDiasporaUserPageListJson(DiasporaUserListParam param, Pagination pagination)
        {
            TData<List<DiasporaUserEntity>> obj = await _iDiasporaUserService.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("DiasporaUser:search,user:search")]
        public async Task<IActionResult> GetCompanyTreeListJson(DiasporaUserListParam param)
        {
            TData<List<ZtreeInfo>> obj = await _iDiasporaUserService.GetZtreeDiasporaUserList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("diasporaUser:view")]
        public async Task<IActionResult> GetUserTreeListJson(DiasporaUserListParam param)
        {
            TData<List<ZtreeInfo>> obj = await _iDiasporaUserService.GetZtreeUserList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("diasporaUser:view")]
        public async Task<IActionResult> GetFormJson(int id)
        {
            TData<DiasporaUserEntity> obj = await _iDiasporaUserService.GetEntity(id);
            return Json(obj);
        }

        [HttpGet]
        public async Task<IActionResult> GetMaxSortJson()
        {
            TData<int> obj = await _iDiasporaUserService.GetMaxSort();
            return Json(obj);
        }
        #endregion

        #region Submit data
        [HttpPost]
        [AuthorizeFilter("diasporaUser:add,company:edit")]
        public async Task<IActionResult> SaveFormJson(DiasporaUserEntity entity)
        {
            TData<string> obj = await _iDiasporaUserService.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("diasporaUser:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await _iDiasporaUserService.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}