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
    public class NationalityController : BaseController
    {
        private readonly INationalityService _iNationalityService;

        public NationalityController(IUnitOfWork iUnitOfWork, INationalityService iNationalityService) : base(iUnitOfWork)
        {
            _iNationalityService = iNationalityService;
        }

        #region View function
        [AuthorizeFilter("nationality:view")]
        public IActionResult NationalityIndex()
        {
            return View();
        }

        [AuthorizeFilter("nationality:view")]
        public IActionResult NationalityForm()
        {
            return View();
        }
        #endregion

        #region Get data
        [HttpGet]
        [AuthorizeFilter("nationality:search,user:search")]
        public async Task<IActionResult> GetListJson(NationalityListParam param)
        {
            TData<List<NationalityEntity>> obj = await _iNationalityService.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("nationality:search,user:search")]
        public async Task<IActionResult> GetPageListJson(NationalityListParam param, Pagination pagination)
        {
            TData<List<NationalityEntity>> obj = await _iNationalityService.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("nationality:view")]
        public async Task<IActionResult> GetFormJson(long id)
        {
            TData<NationalityEntity> obj = await _iNationalityService.GetEntity(id);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("nationality:view")]
        public async Task<IActionResult> GetNationalityName(NationalityListParam param)
        {
            TData<string> obj = new TData<string>();
            var list = await _iNationalityService.GetList(param);
            if (list.Tag == 1)
            {
                obj.Data = string.Join(",", list.Data.Select(p => p.FullName));
                obj.Tag = 1;
            }
            return Json(obj);
        }
        #endregion

        #region Submit data
        [HttpPost]
        [AuthorizeFilter("nationality:add,nationality:edit")]
        public async Task<IActionResult> SaveFormJson(NationalityEntity entity)
        {
            TData<string> obj = await _iNationalityService.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("nationality:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await _iNationalityService.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}