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
    public class PropertySubscriptionController : BaseController
    {
        private readonly IPropertySubscriptionService _iPropertySubscriptionService;

        public PropertySubscriptionController(IUnitOfWork iUnitOfWork, IPropertySubscriptionService iPropertySubscriptionService) : base(iUnitOfWork)
        {
            _iPropertySubscriptionService = iPropertySubscriptionService;
        }

        #region View function
        [AuthorizeFilter("propertysubscription:view")]
        public IActionResult PropertySubscriptionIndex()
        {
            return View();
        }

        public IActionResult PropertySubscriptionForm()
        {
            return View();
        }

       
        #endregion

        #region Get data
        [HttpGet]
        [AuthorizeFilter("propertysubscription:search,user:search")]
        public async Task<IActionResult> GetListJson(PropertySubscriptionListParam param)
        {
            TData<List<PropertySubscriptionEntity>> obj = await _iPropertySubscriptionService.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("propertysubscription:search,user:search")]
        public async Task<IActionResult> GetPropertySubscriptionPageListJson(PropertySubscriptionListParam param, Pagination pagination)
        {
            TData<List<PropertySubscriptionEntity>> obj = await _iPropertySubscriptionService.GetPageList(param, pagination);
            return Json(obj);
        }


        //[HttpGet]
        //[AuthorizeFilter("propertysubscription:search,user:search")]
        //public async Task<IActionResult> GetEtransactPageListJson(PropertySubscriptionListParam param, Pagination pagination)
        //{
        //    TData<List<PropertySubscriptionEntity>> obj = await _iPropertySubscriptionService.GetPageList(param, pagination);
        //    return Json(obj);
        //}


        [HttpGet]
        [AuthorizeFilter("propertysubscription:search,user:search")]
        public async Task<IActionResult> GetPropertySubscriptionTreeListJson(PropertySubscriptionListParam param)
        {
            TData<List<ZtreeInfo>> obj = await _iPropertySubscriptionService.GetZtreePropertySubscriptionList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("propertysubscription:view")]
        public async Task<IActionResult> GetUserTreeListJson(PropertySubscriptionListParam param)
        {
            TData<List<ZtreeInfo>> obj = await _iPropertySubscriptionService.GetZtreeUserList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("propertysubscription:view")]
        public async Task<IActionResult> GetFormJson(int id)
        {
            TData<PropertySubscriptionEntity> obj = await _iPropertySubscriptionService.GetEntity(id);
            return Json(obj);
        }

        [HttpGet]
        public async Task<IActionResult> GetMaxSortJson()
        {
            TData<int> obj = await _iPropertySubscriptionService.GetMaxSort();
            return Json(obj);
        }
        #endregion

        #region Submit data
        [HttpPost]
        [AuthorizeFilter("propertysubscription:add,propertysubscription:edit")]
        public async Task<IActionResult> SaveFormJson(PropertySubscriptionEntity entity)
        {
            TData<string> obj = await _iPropertySubscriptionService.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("propertysubscription:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await _iPropertySubscriptionService.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}