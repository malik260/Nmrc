using Microsoft.AspNetCore.Mvc;
using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;
using Mortgage.Ecosystem.Web.Filter;

namespace Mortgage.Ecosystem.Web.Controllers.Systems
{
    public class BranchController : BaseController
    {
        private readonly IBranchService _iBranchService;

        public BranchController(IUnitOfWork iUnitOfWork, IBranchService iBranchService) : base(iUnitOfWork)
        {
            _iBranchService = iBranchService;
        }

        #region View function
        [AuthorizeFilter("branch:view")]
        public IActionResult BranchIndex()
        {
            return View();
        }

        [AuthorizeFilter("branch:view")]
        public IActionResult BranchForm()
        {
            return View();
        }
        #endregion

        #region Get data
        [HttpGet]
        [AuthorizeFilter("branch:search,user:search")]
        public async Task<IActionResult> GetListJson(BranchListParam param)
        {
            TData<List<BranchEntity>> obj = await _iBranchService.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("branch:search,user:search")]
        public async Task<IActionResult> GetPageListJson(BranchListParam param, Pagination pagination)
        {
            TData<List<BranchEntity>> obj = await _iBranchService.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("branch:view")]
        public async Task<IActionResult> GetFormJson(long id)
        {
            TData<BranchEntity> obj = await _iBranchService.GetEntity(id);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("branch:view")]
        public async Task<IActionResult> GetBranchName(BranchListParam param)
        {
            TData<string> obj = new TData<string>();
            var list = await _iBranchService.GetList(param);
            if (list.Tag == 1)
            {
                obj.Data = string.Join(",", list.Data.Select(p => p.Name));
                obj.Tag = 1;
            }
            return Json(obj);
        }
        #endregion

        #region Submit data
        [HttpPost]
        [AuthorizeFilter("branch:add,branch:edit")]
        public async Task<IActionResult> SaveFormJson(BranchEntity entity)
        {
            TData<string> obj = await _iBranchService.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("branch:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await _iBranchService.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}