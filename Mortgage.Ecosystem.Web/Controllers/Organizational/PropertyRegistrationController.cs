using Microsoft.AspNetCore.Http;
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
using NPOI.SS.Formula.Atp;

namespace Mortgage.Ecosystem.Web.Controllers.Organizational
{
    [ExceptionFilter]
    public class PropertyRegistrationController : BaseController
    {
        private readonly IPropertyRegistrationService _iPropertyRegistrationService;
        private readonly IPropertyUploadService _iPropertyUploadService;
        private readonly IAuditTrailService _iAuditTrailService;

        public PropertyRegistrationController(IUnitOfWork iUnitOfWork, IPropertyRegistrationService iPropertyRegistrationService, IPropertyUploadService propertyUploadService, IAuditTrailService iAuditTrailService) : base(iUnitOfWork)
        {
            _iPropertyRegistrationService = iPropertyRegistrationService;
            _iPropertyUploadService = propertyUploadService;
            _iAuditTrailService = iAuditTrailService;
        }

        #region View function
        [AuthorizeFilter("propertyregistration:view")]
        public IActionResult PropertyRegistrationIndex()
        {
            return View();
        }

        public IActionResult PropertyImages()
        {
            return View();
        }

        public IActionResult PropertyRegistrationForm()
        {
            return View();
        }

        public IActionResult PropertyRegistrationEditForm()
        {
            return View();
        }

        #endregion

        #region Get data
        [HttpGet]
        [AuthorizeFilter("propertyregistration:search,user:search")]
        public async Task<IActionResult> GetListJson(PropertyRegistrationListParam param, Pagination pagination)
        {
            TData<List<PropertyRegistrationEntity>> obj = await _iPropertyRegistrationService.GetList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        //[AuthorizeFilter("propertyregistration:view")]
        public async Task<IActionResult> GetImagesJson(long id)
        {
            TData<List<PropertyUploadEntity>> obj = await _iPropertyUploadService.GetList(id);
            var auditInstance = new AuditTrailEntity();
            auditInstance.Action = SystemOperationCode.GetImagesJson.ToString();
            auditInstance.ActionRoute = SystemOperationCode.PropertyRegistration.ToString();

            var audit = await _iAuditTrailService.SaveForm(auditInstance);
            return Json(obj);
        }

        [HttpGet]
        //[AuthorizeFilter("propertyregistration:view")]
        public async Task<IActionResult> GetAllImages(PropertyUploadListParam param)
        {
            // PropertyUploadListParam param = new PropertyUploadListParam();  
            TData<List<PropertyUploadEntity>> obj = await _iPropertyUploadService.GetPropertyList(param);
            var auditInstance = new AuditTrailEntity();
            auditInstance.Action = SystemOperationCode.GetAllImages.ToString();
            auditInstance.ActionRoute = SystemOperationCode.PropertyRegistration.ToString();

            var audit = await _iAuditTrailService.SaveForm(auditInstance);
            return Json(obj);
        }
        public async Task<IActionResult> GetFormJsonn(long id)
        {

            TData<PropertyRegistrationEntity> obj = await _iPropertyRegistrationService.GetEntities(id)
;
            return Json(obj);
        }



        [HttpGet]
        [AuthorizeFilter("propertyregistration:search,user:search")]
        public async Task<IActionResult> GetPropertyRegistrationPageListJson(PropertyRegistrationListParam param, Pagination pagination)
        {
            try
            {
                TData<List<PropertyRegistrationEntity>> obj = await _iPropertyRegistrationService.GetPageList(param, pagination);
                var auditInstance = new AuditTrailEntity();
                auditInstance.Action = SystemOperationCode.GetPropertyRegistrationPageListJson.ToString();
                auditInstance.ActionRoute = SystemOperationCode.PropertyRegistration.ToString();

                var audit = await _iAuditTrailService.SaveForm(auditInstance);

                return Json(obj);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> RegisterProperty(PropertyRegistrationListParam param , Pagination pagination)
        {
            try
            {
                TData<List<PropertyRegistrationEntity>> obj = await _iPropertyRegistrationService.GetList(param, pagination);
                var auditInstance = new AuditTrailEntity();
                auditInstance.Action = SystemOperationCode.RegisterProperty.ToString();
                auditInstance.ActionRoute = SystemOperationCode.PropertyRegistration.ToString();

                var audit = await _iAuditTrailService.SaveForm(auditInstance);
                return Json(obj);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        //[HttpGet]
        //[AuthorizeFilter("propertysubscription:search,user:search")]
        //public async Task<IActionResult> GetEtransactPageListJson(PropertySubscriptionListParam param, Pagination pagination)
        //{
        //    TData<List<PropertySubscriptionEntity>> obj = await _iPropertySubscriptionService.GetPageList(param, pagination);
        //    return Json(obj);
        //}


        //[HttpGet]
        //[AuthorizeFilter("propertyregistration:search,user:search")]
        //public async Task<IActionResult> GetPropertyRegistrationTreeListJson(PropertyRegistrationListParam param)
        //{
        //    TData<List<ZtreeInfo>> obj = await _iPropertyRegistrationService.GetZtreePropertyRegistrationList(param);
        //    return Json(obj);
        //}

        //[HttpGet]
        //[AuthorizeFilter("propertyregistration:view")]
        //public async Task<IActionResult> GetUserTreeListJson(PropertyRegistrationListParam param)
        //{
        //    TData<List<ZtreeInfo>> obj = await _iPropertyRegistrationService.GetZtreeUserList(param);
        //    return Json(obj);
        //}

        [HttpGet]
        //[AuthorizeFilter("propertyregistration:view")]
        public async Task<IActionResult> GetFormJson(long id)
        {
            TData<List<PropertyRegistrationListParam>> obj = await _iPropertyRegistrationService.GetEntity(id);
            return Json(obj);
        }

        [HttpGet]
        public async Task<IActionResult> GetMaxSortJson()
        {
            TData<int> obj = await _iPropertyRegistrationService.GetMaxSort();
            return Json(obj);
        }

        [HttpGet]
        //[AuthorizeFilter("refund:view")]
        public async Task<IActionResult> ViewPmbCompanyName()
        {
            try
            {
                TData<CustomerDetailsViewModel> obj = await _iPropertyRegistrationService.GetPmbCompanyName();
                var auditInstance = new AuditTrailEntity();
                auditInstance.Action = SystemOperationCode.ViewPmbCompanyName.ToString();
                auditInstance.ActionRoute = SystemOperationCode.PropertyRegistration.ToString();

                var audit = await _iAuditTrailService.SaveForm(auditInstance);
                return Json(obj);

            }
            catch (Exception e)
            {

                throw;
            }        }
        #endregion

        #region Submit data
        [HttpPost]
        //[AuthorizeFilter("propertyregistration:add,propertyregistration:edit")]
        public async Task<IActionResult> SaveFormJson(PropertyRegistrationEntity entity)
        {

            try
            {
                TData<string> obj = await _iPropertyRegistrationService.SaveForm(entity);
                return Json(obj);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        [HttpPost]
        //[AuthorizeFilter("propertyregistration:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await _iPropertyRegistrationService.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}