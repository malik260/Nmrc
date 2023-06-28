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
    public class AlertTypeController : BaseController
    {
        private readonly IAlertTypeService _iAlertTypeService;

        public AlertTypeController(IUnitOfWork iUnitOfWork, IAlertTypeService iAlertTypeService) : base(iUnitOfWork)
        {
            _iAlertTypeService = iAlertTypeService;
        }

        #region View function
        [AuthorizeFilter("Alerttype:view")]
        public IActionResult AlertTypeIndex()
        {
            return View();
        }

        [AuthorizeFilter("Alerttype:view")]
        public IActionResult AlertTypeForm()
        {
            return View();
        }
        #endregion

        #region Get data
        [HttpGet]
        [AuthorizeFilter("Alerttype:search,user:search")]
        public async Task<IActionResult> GetListJson(AlertTypeListParam param)
        {
            TData<List<AlertTypeEntity>> obj = await _iAlertTypeService.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("Alerttype:search,user:search")]
        public async Task<IActionResult> GetPageListJson(AlertTypeListParam param, Pagination pagination)
        {
            TData<List<AlertTypeEntity>> obj = await _iAlertTypeService.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("Alerttype:view")]
        public async Task<IActionResult> GetFormJson(long id)
        {
            TData<AlertTypeEntity> obj = await _iAlertTypeService.GetEntity(id);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("Alerttype:view")]
        public async Task<IActionResult> GetAlertTypeName(AlertTypeListParam param)
        {
            TData<string> obj = new TData<string>();
            var list = await _iAlertTypeService.GetList(param);
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
        [AuthorizeFilter("Alerttype:add,Alerttype:edit")]
        public async Task<IActionResult> SaveFormJson(AlertTypeEntity entity)
        {
            TData<string> obj = await _iAlertTypeService.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("Alerttype:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await _iAlertTypeService.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}