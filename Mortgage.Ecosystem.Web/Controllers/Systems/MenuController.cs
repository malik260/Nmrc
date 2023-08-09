using Microsoft.AspNetCore.Mvc;
using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.Web.Filter;

namespace Mortgage.Ecosystem.Web.Controllers.Systems
{
    public class MenuController : BaseController
    {
        private readonly IMenuService _iMenuService;

        public MenuController(IUnitOfWork iUnitOfWork, IMenuService iMenuService) : base(iUnitOfWork)
        {
            _iMenuService = iMenuService;
        }

        #region view function

        [AuthorizeFilter("menu:view")]
        public IActionResult MenuIndex()
        {
            return View();
        }

        public IActionResult MenuForm()
        {
            return View();
        }

        public IActionResult MenuChoose()
        {
            return View();
        }

        public IActionResult MenuIcon()
        {
            return View();
        }

        #endregion view function

        #region Get data

        [HttpGet]
        [AuthorizeFilter("menu:search,role:search")]
        public async Task<IActionResult> GetListJson(MenuListParam param)
        {
            TData<List<MenuEntity>> obj = await _iMenuService.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("menu:search,role:search")]
        public async Task<IActionResult> GetMenuTreeListJson(MenuListParam param)
        {
            TData<List<ZtreeInfo>> obj = await _iMenuService.GetZtreeList(param);
            return Json(obj);
        }

        //[HttpPost]
        //[AuthorizeFilter("menu:view")]
        //public async Task<IActionResult> GetFormJson(long id)
        //{
        //    TData<MenuEntity> obj = await _iMenuService.GetEntity(id);
        //    ViewBag.ApprovalLevel = obj.Data.ApprovalLevel;
        //    return Json(obj);
        //}

        [HttpGet]
        [AuthorizeFilter("menu:view")]
        public async Task<IActionResult> GetFormJson(long id)
        {
            TData<MenuEntity> obj = await _iMenuService.GetEntity(id);
            ViewBag.ApprovalLevel = obj.Data.ApprovalLevel;
            return Json(obj);
        }

        [HttpGet]
        public async Task<IActionResult> GetMaxSortJson(long parent = 0)
        {
            TData<int> obj = await _iMenuService.GetMaxSort(parent);
            return Json(obj);
        }

        #endregion Get data

        #region Submit data

        [HttpPost]
        [AuthorizeFilter("menu:add,menu:edit")]
        public async Task<IActionResult> SaveFormJson(MenuEntity entity)
        {
            TData<string> obj = await _iMenuService.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("menu:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await _iMenuService.DeleteForm(ids);
            return Json(obj);
        }

        #endregion Submit data
    }
}