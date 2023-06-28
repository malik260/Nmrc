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
    public class UnlockNhfPortalController : BaseController
    {
        private readonly IUnlockNhfPortalService _iUnlockNhfPortalService;

        public UnlockNhfPortalController(IUnitOfWork iUnitOfWork, IUnlockNhfPortalService iUnlockNhfPortalService) : base(iUnitOfWork)
        {
            _iUnlockNhfPortalService = iUnlockNhfPortalService;
        }

        #region View function
        [AuthorizeFilter("unlocknhfportal:view")]
        public IActionResult UnlockNhfPortalIndex()
        {
            return View();
        }

        public IActionResult UnlockNhfPortalForm()
        {
            return View();
        }
        #endregion

        #region Get data
        [HttpGet]
        [AuthorizeFilter("unlocknhfportal:search,user:search")]
        public async Task<IActionResult> GetListJson(UnlockNhfPortalListParam param)
        {
            TData<List<UnlockNhfPortalEntity>> obj = await _iUnlockNhfPortalService.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("unlocknhfportal:search,user:search")]
        public async Task<IActionResult> GetUnlockNhfPortalPageListJson(UnlockNhfPortalListParam param, Pagination pagination)
        {
            TData<List<UnlockNhfPortalEntity>> obj = await _iUnlockNhfPortalService.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("unlocknhfportal:search,user:search")]
        public async Task<IActionResult> GetCompanyTreeListJson(UnlockNhfPortalListParam param)
        {
            TData<List<ZtreeInfo>> obj = await _iUnlockNhfPortalService.GetZtreeUnlockNhfPortalList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("unlocknhfportal:view")]
        public async Task<IActionResult> GetUserTreeListJson(UnlockNhfPortalListParam param)
        {
            TData<List<ZtreeInfo>> obj = await _iUnlockNhfPortalService.GetZtreeUserList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("unlocknhfportal:view")]
        public async Task<IActionResult> GetFormJson(int id)
        {
            TData<UnlockNhfPortalEntity> obj = await _iUnlockNhfPortalService.GetEntity(id);
            return Json(obj);
        }

        [HttpGet]
        public async Task<IActionResult> GetMaxSortJson()
        {
            TData<int> obj = await _iUnlockNhfPortalService.GetMaxSort();
            return Json(obj);
        }
        #endregion

        #region Submit data
        [HttpPost]
        [AuthorizeFilter("unlocknhfportal:add,company:edit")]
        public async Task<IActionResult> SaveFormJson(UnlockNhfPortalEntity entity)
        {
            TData<string> obj = await _iUnlockNhfPortalService.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("unlocknhfportal:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await _iUnlockNhfPortalService.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}