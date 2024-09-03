using Microsoft.AspNetCore.Mvc;
using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Enums;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;
using Mortgage.Ecosystem.Web.Filter;

namespace Mortgage.Ecosystem.Web.Controllers.Systems
{
    public class DesignationController : BaseController
    {
        private readonly IDesignationService _iDesignationService;
        private readonly IAuditTrailService _iAuditTrailService;

        public DesignationController(IUnitOfWork iUnitOfWork, IDesignationService iDesignationService) : base(iUnitOfWork)
        {
            _iDesignationService = iDesignationService;
        }

        #region View function
        [AuthorizeFilter("designation:view")]
        public IActionResult DesignationIndex()
        {
            return View();
        }

        public IActionResult DesignationForm()
        {
            return View();
        }
        #endregion

        #region Get data
        [HttpGet]
        [AuthorizeFilter("designation:search,user:view")]
        public async Task<IActionResult> GetListJson(DesignationListParam param)
        {
            TData<List<DesignationEntity>> obj = await _iDesignationService.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("designation:search,user:view")]
        public async Task<IActionResult> GetPageListJson(DesignationListParam param, Pagination pagination)
        {
            TData<List<DesignationEntity>> obj = await _iDesignationService.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("designation:view")]
        public async Task<IActionResult> GetFormJson(long id)
        {
            TData<DesignationEntity> obj = await _iDesignationService.GetEntity(id);
            return Json(obj);
        }

        [HttpGet]
        public async Task<IActionResult> GetMaxSortJson()
        {
            TData<int> obj = await _iDesignationService.GetMaxSort();
            return Json(obj);
        }

        [HttpGet]
        public async Task<IActionResult> GetDesignationName(DesignationListParam param)
        {
            TData<string> obj = new TData<string>();
            var list = await _iDesignationService.GetList(param);
            if (list.Tag == 1)
            {
                obj.Data = string.Join(",", list.Data.Select(p => p.Name));
                obj.Tag = 1;
            }

            var auditInstance = new AuditTrailEntity();
            auditInstance.Action = SystemOperationCode.GetDesignationName.ToString();
            auditInstance.ActionRoute = SystemOperationCode.Designation.ToString();

            var audit = await _iAuditTrailService.SaveForm(auditInstance);
            return Json(obj);
        }
        #endregion

        #region Submit data
        [HttpPost]
        [AuthorizeFilter("designation:add,designation:edit")]
        public async Task<IActionResult> SaveFormJson(DesignationEntity entity)
        {
            TData<string> obj = await _iDesignationService.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("designation:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await _iDesignationService.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}