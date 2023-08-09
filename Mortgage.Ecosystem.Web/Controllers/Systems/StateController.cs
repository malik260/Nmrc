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
    public class StateController : BaseController
    {
        private readonly IStateService _iStateService;

        public StateController(IUnitOfWork iUnitOfWork, IStateService iStateService) : base(iUnitOfWork)
        {
            _iStateService = iStateService;
        }

        #region View function
        [AuthorizeFilter("state:view")]
        public IActionResult StateIndex()
        {
            return View();
        }

        [AuthorizeFilter("state:view")]
        public IActionResult StateForm()
        {
            return View();
        }
        #endregion

        #region Get data
        [HttpGet]
        [AuthorizeFilter("state:search,user:search")]
        public async Task<IActionResult> GetListJson(StateListParam param)
        {
            TData<List<StateEntity>> obj = await _iStateService.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("state:search,user:search")]
        public async Task<IActionResult> GetPageListJson(StateListParam param, Pagination pagination)
        {
            TData<List<StateEntity>> obj = await _iStateService.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("state:view")]
        public async Task<IActionResult> GetFormJson(long id)
        {
            TData<StateEntity> obj = await _iStateService.GetEntity(id);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("state:view")]
        public async Task<IActionResult> GetStateName(StateListParam param)
        {
            TData<string> obj = new TData<string>();
            var list = await _iStateService.GetList(param);
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
        [AuthorizeFilter("state:add,state:edit")]
        public async Task<IActionResult> SaveFormJson(StateEntity entity)
        {
            TData<string> obj = await _iStateService.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("state:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await _iStateService.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}