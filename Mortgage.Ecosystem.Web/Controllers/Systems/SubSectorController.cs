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
    public class SubSectorController : BaseController
    {
        private readonly ISubSectorService _iSubSectorService;

        public SubSectorController(IUnitOfWork iUnitOfWork, ISubSectorService iSubSectorService) : base(iUnitOfWork)
        {
            _iSubSectorService = iSubSectorService;
        }

        #region View function
        [AuthorizeFilter("accounttype:view")]
        public IActionResult SubSectorIndex()
        {
            return View();
        }

        [AuthorizeFilter("accounttype:view")]
        public IActionResult SubSectorForm()
        {
            return View();
        }
        #endregion

        #region Get data
        [HttpGet]
        [AuthorizeFilter("accounttype:search,user:search")]
        public async Task<IActionResult> GetListJson(SubSectorListParam param)
        {
            TData<List<SubSectorEntity>> obj = await _iSubSectorService.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("accounttype:search,user:search")]
        public async Task<IActionResult> GetPageListJson(SubSectorListParam param, Pagination pagination)
        {
            TData<List<SubSectorEntity>> obj = await _iSubSectorService.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("accounttype:view")]
        public async Task<IActionResult> GetFormJson(long id)
        {
            TData<SubSectorEntity> obj = await _iSubSectorService.GetEntity(id);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("accounttype:view")]
        public async Task<IActionResult> GetSubSectorName(SubSectorListParam param)
        {
            TData<string> obj = new TData<string>();
            var list = await _iSubSectorService.GetList(param);
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
        [AuthorizeFilter("accounttype:add,accounttype:edit")]
        public async Task<IActionResult> SaveFormJson(SubSectorEntity entity)
        {
            TData<string> obj = await _iSubSectorService.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("accounttype:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await _iSubSectorService.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}