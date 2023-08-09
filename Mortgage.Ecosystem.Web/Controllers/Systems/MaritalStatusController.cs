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
    public class MaritalStatusController : BaseController
    {
        private readonly IMaritalStatusService _iMaritalStatusService;

        public MaritalStatusController(IUnitOfWork iUnitOfWork, IMaritalStatusService iMaritalStatusService) : base(iUnitOfWork)
        {
            _iMaritalStatusService = iMaritalStatusService;
        }

        #region View function
        [AuthorizeFilter("maritalstatus:view")]
        public IActionResult MaritalStatusIndex()
        {
            return View();
        }

        [AuthorizeFilter("maritalstatus:view")]
        public IActionResult MaritalStatusForm()
        {
            return View();
        }
        #endregion

        #region Get data
        [HttpGet]
        [AuthorizeFilter("maritalstatus:search,user:search")]
        public async Task<IActionResult> GetListJson(MaritalStatusListParam param)
        {
            TData<List<MaritalStatusEntity>> obj = await _iMaritalStatusService.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("maritalstatus:search,user:search")]
        public async Task<IActionResult> GetPageListJson(MaritalStatusListParam param, Pagination pagination)
        {
            TData<List<MaritalStatusEntity>> obj = await _iMaritalStatusService.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("maritalstatus:view")]
        public async Task<IActionResult> GetFormJson(long id)
        {
            TData<MaritalStatusEntity> obj = await _iMaritalStatusService.GetEntity(id);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("maritalstatus:view")]
        public async Task<IActionResult> GetMaritalStatusName(MaritalStatusListParam param)
        {
            TData<string> obj = new TData<string>();
            var list = await _iMaritalStatusService.GetList(param);
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
        [AuthorizeFilter("maritalstatus:add,maritalstatus:edit")]
        public async Task<IActionResult> SaveFormJson(MaritalStatusEntity entity)
        {
            TData<string> obj = await _iMaritalStatusService.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("maritalstatus:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await _iMaritalStatusService.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}