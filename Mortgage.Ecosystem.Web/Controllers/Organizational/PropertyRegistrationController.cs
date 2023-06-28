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
    public class PropertyRegistrationController : BaseController
    {
        private readonly IPropertyRegistrationService _iPropertyRegistrationService;

        public PropertyRegistrationController(IUnitOfWork iUnitOfWork, IPropertyRegistrationService iPropertyRegistrationService) : base(iUnitOfWork)
        {
            _iPropertyRegistrationService = iPropertyRegistrationService;
        }

        #region View function
        [AuthorizeFilter("propertyregistration:view")]
        public IActionResult PropertyRegistrationIndex()
        {
            return View();
        }

        public IActionResult PropertyRegistrationForm()
        {
            return View();
        }

       
        #endregion

        #region Get data
        [HttpGet]
        [AuthorizeFilter("propertyregistration:search,user:search")]
        public async Task<IActionResult> GetListJson(PropertyRegistrationListParam param)
        {
            TData<List<PropertyRegistrationEntity>> obj = await _iPropertyRegistrationService.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("propertyregistration:search,user:search")]
        public async Task<IActionResult> GetPropertyRegistrationPageListJson(PropertyRegistrationListParam param, Pagination pagination)
        {
            TData<List<PropertyRegistrationEntity>> obj = await _iPropertyRegistrationService.GetPageList(param, pagination);
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
        [AuthorizeFilter("propertyregistration:search,user:search")]
        public async Task<IActionResult> GetPropertyRegistrationTreeListJson(PropertyRegistrationListParam param)
        {
            TData<List<ZtreeInfo>> obj = await _iPropertyRegistrationService.GetZtreePropertyRegistrationList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("propertyregistration:view")]
        public async Task<IActionResult> GetUserTreeListJson(PropertyRegistrationListParam param)
        {
            TData<List<ZtreeInfo>> obj = await _iPropertyRegistrationService.GetZtreeUserList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("propertyregistration:view")]
        public async Task<IActionResult> GetFormJson(int id)
        {
            TData<PropertyRegistrationEntity> obj = await _iPropertyRegistrationService.GetEntity(id);
            return Json(obj);
        }

        [HttpGet]
        public async Task<IActionResult> GetMaxSortJson()
        {
            TData<int> obj = await _iPropertyRegistrationService.GetMaxSort();
            return Json(obj);
        }
        #endregion

        #region Submit data
        [HttpPost]
        [AuthorizeFilter("propertyregistration:add,propertyregistration:edit")]
        public async Task<IActionResult> SaveFormJson(PropertyRegistrationEntity entity)
        {
            TData<string> obj = await _iPropertyRegistrationService.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("propertyregistration:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await _iPropertyRegistrationService.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}