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
    public class NmrcCategoryController : BaseController
    {
        private readonly INmrcCategoryService _iNmrcCategoryService;
        private readonly IAuditTrailService _iAuditTrailService;

        public NmrcCategoryController(IUnitOfWork iUnitOfWork, INmrcCategoryService iNmrcCategoryService, IAuditTrailService iAuditTrailService) : base(iUnitOfWork)
        {
            _iNmrcCategoryService = iNmrcCategoryService;
            _iAuditTrailService = iAuditTrailService;
        }

        #region View function
        [AuthorizeFilter("nmrccategory:view")]
        public IActionResult NmrcCategoryIndex()
        {
            return View();
        }

        [AuthorizeFilter("nmrccategory:view")]
        public IActionResult NmrcCategoryForm()
        {
            return View();
        }
        #endregion

        #region Get data
        [HttpGet]
        //[AuthorizeFilter("gender:search,user:search")]
        public async Task<IActionResult> GetListJson(NmrcCategoryListParam param)
        {
            TData<List<NmrcCategoryEntity>> obj = await _iNmrcCategoryService.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("nmrccategory:search,user:search")]
        public async Task<IActionResult> GetPageListJson(NmrcCategoryListParam param, Pagination pagination)
        {
            TData<List<NmrcCategoryEntity>> obj = await _iNmrcCategoryService.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("nmrccategory:view")]
        public async Task<IActionResult> GetFormJson(int id)
        {
            TData<NmrcCategoryEntity> obj = await _iNmrcCategoryService.GetEntity(id);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("nmrccategory:view")]
        public async Task<IActionResult> GetGenderName(NmrcCategoryListParam param)
        {
            TData<string> obj = new TData<string>();
            var list = await _iNmrcCategoryService.GetList(param);
            if (list.Tag == 1)
            {
                obj.Data = string.Join(",", list.Data.Select(p => p.Name));
                obj.Tag = 1;
            }

            var auditInstance = new AuditTrailEntity();
            auditInstance.Action = SystemOperationCode.GetNmrcCategoryName.ToString();
            auditInstance.ActionRoute = SystemOperationCode.NmrcCategory.ToString();

            var audit = await _iAuditTrailService.SaveForm(auditInstance);
            return Json(obj);
        }
        #endregion

        #region Submit data
        [HttpPost]
        [AuthorizeFilter("nmrccategory:add,nmrccategory:edit")]
        public async Task<IActionResult> SaveFormJson(NmrcCategoryEntity entity)
        {
            TData<string> obj = await _iNmrcCategoryService.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("gender:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await _iNmrcCategoryService.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}