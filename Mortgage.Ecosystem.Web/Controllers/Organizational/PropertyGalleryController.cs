using Microsoft.AspNetCore.Mvc;
using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.BusinessLogic.Layer.Services;
using Mortgage.Ecosystem.DataAccess.Layer.Enums;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;
using Mortgage.Ecosystem.Web.Filter;

namespace Mortgage.Ecosystem.Web.Controllers.Organizational
{
    public class PropertyGalleryController : BaseController
    {
        private readonly IPropertyGalleryService _iPropertyGalleryService;
        private readonly IAuditTrailService _iAuditTrailService;
        public PropertyGalleryController(IUnitOfWork iUnitOfWork, IPropertyGalleryService iPropertyGalleryService, IAuditTrailService iAuditTrailService) : base(iUnitOfWork)
        {
            _iPropertyGalleryService = iPropertyGalleryService;
            _iAuditTrailService = iAuditTrailService;
        }

        #region View function
        [AuthorizeFilter("propertygallery:view")]
        public IActionResult PropertyGalleryIndex()
        {
            return View();
        }


       
        #endregion

        #region Get data
        [HttpGet]
        [AuthorizeFilter("propertygallery:search,user:search")]
        public async Task<IActionResult> GetListJson(PropertyGalleryListParam param)
        {
            TData<List<PropertyGalleryEntity>> obj = await _iPropertyGalleryService.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("propertygallery:search,user:search")]
        public async Task<IActionResult> GetPropertyGalleryPageListJson(PropertyGalleryListParam param, Pagination pagination)
        {
            TData<List<PropertyGalleryEntity>> obj = await _iPropertyGalleryService.GetPageList(param, pagination);

            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("propertygallery:search,user:search")]
        public async Task<IActionResult> GetPropertyGalleryPageListJsonn(PropertyGalleryListParam param, Pagination pagination)
        {
            TData<List<PropertyGalleryEntity>> obj = await _iPropertyGalleryService.GetPageList(param, pagination);
            var auditInstance = new AuditTrailEntity();
            auditInstance.Action = SystemOperationCode.GetPropertyGalleryPageListJsonn.ToString();
            auditInstance.ActionRoute = SystemOperationCode.PropertyGallery.ToString();

            var audit = await _iAuditTrailService.SaveForm(auditInstance);

            var cardData = obj.Data.Select(entity => new
            {
                Title = entity.Title,
                
                // Add additional properties as needed for the card data
            });

            return Json(cardData);
        }


        //[HttpGet]
        //[AuthorizeFilter("propertysubscription:search,user:search")]
        //public async Task<IActionResult> GetEtransactPageListJson(PropertySubscriptionListParam param, Pagination pagination)
        //{
        //    TData<List<PropertySubscriptionEntity>> obj = await _iPropertySubscriptionService.GetPageList(param, pagination);
        //    return Json(obj);
        //}


        [HttpGet]
        [AuthorizeFilter("propertygallery:search,user:search")]
        public async Task<IActionResult> GetPropertyGalleryTreeListJson(PropertyGalleryListParam param)
        {
            TData<List<ZtreeInfo>> obj = await _iPropertyGalleryService.GetZtreePropertyGalleryList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("propertygallery:view")]
        public async Task<IActionResult> GetUserTreeListJson(PropertyGalleryListParam param)
        {
            TData<List<ZtreeInfo>> obj = await _iPropertyGalleryService.GetZtreeUserList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("propertygallery:view")]
        public async Task<IActionResult> GetFormJson(int id)
        {
            TData<PropertyGalleryEntity> obj = await _iPropertyGalleryService.GetEntity(id);
            return Json(obj);
        }

        [HttpGet]
        public async Task<IActionResult> GetMaxSortJson()
        {
            TData<int> obj =  await _iPropertyGalleryService.GetMaxSort();
            return Json(obj);
        }
        #endregion

        #region Submit data
        [HttpPost]
        [AuthorizeFilter("propertygallery:add,propertygallery:edit")]
        public async Task<IActionResult> SaveFormJson(PropertyGalleryEntity entity)
        {
            TData<string> obj = await _iPropertyGalleryService.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("propertygallery:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await _iPropertyGalleryService.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}