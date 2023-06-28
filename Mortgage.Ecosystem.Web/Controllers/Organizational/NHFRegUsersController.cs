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
    public class NHFRegUsersController : BaseController
    {
        private readonly INHFRegUsersService _iNHFRegUsersService;

        public NHFRegUsersController(IUnitOfWork iUnitOfWork, INHFRegUsersService iNHFRegUsersService) : base(iUnitOfWork)
        {
            _iNHFRegUsersService = iNHFRegUsersService;
        }

        #region View function
        [AuthorizeFilter("nhfregusers:view")]
        public IActionResult NHFRegUsersIndex()
        {
            return View();
        }

        public IActionResult NHFRegUsersForm()
        {
            return View();
        }
        #endregion

        #region Get data
        [HttpGet]
        [AuthorizeFilter("nhfregusers:search,user:search")]
        public async Task<IActionResult> GetListJson(NHFRegUsersListParam param)
        {
            TData<List<NHFRegUsersEntity>> obj = await _iNHFRegUsersService.GetList(param);
            return Json(obj);
        }


        [HttpGet]
        [AuthorizeFilter("nhfregusers:search,user:search")]
        public async Task<IActionResult> GetEmployeePageListJson(NHFRegUsersListParam param, Pagination pagination)
        {
            TData<List<NHFRegUsersEntity>> obj = await _iNHFRegUsersService.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("nhfregusers:search,user:search")]
        public async Task<IActionResult> GetEmployerPageListJson(NHFRegUsersListParam param, Pagination pagination)
        {
            TData<List<NHFRegUsersEntity>> obj = await _iNHFRegUsersService.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("nhfregusers:search,user:search")]
        public async Task<IActionResult> GetnhfregusersTreeListJson(NHFRegUsersListParam param)
        {
            TData<List<ZtreeInfo>> obj = await _iNHFRegUsersService.GetZtreeNHFRegUsersList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("nhfregusers:view")]
        public async Task<IActionResult> GetUserTreeListJson(NHFRegUsersListParam param)
        {
            TData<List<ZtreeInfo>> obj = await _iNHFRegUsersService.GetZtreeUserList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("nhfregusers:view")]
        public async Task<IActionResult> GetFormJson(int id)
        {
            TData<NHFRegUsersEntity> obj = await _iNHFRegUsersService.GetEntity(id);
            return Json(obj);
        }

        [HttpGet]
        public async Task<IActionResult> GetMaxSortJson()
        {
            TData<int> obj = await _iNHFRegUsersService.GetMaxSort();
            return Json(obj);
        }
        #endregion

        #region Submit data
        [HttpPost]
        [AuthorizeFilter("nhfregusers:add,nhfregusers:edit")]
        public async Task<IActionResult> SaveFormJson(NHFRegUsersEntity entity)
        {
            TData<string> obj = await _iNHFRegUsersService.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("nhfregusers:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await _iNHFRegUsersService.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}