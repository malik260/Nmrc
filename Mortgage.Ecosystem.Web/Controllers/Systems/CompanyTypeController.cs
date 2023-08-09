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
    public class CompanyTypeController : BaseController
    {
        private readonly ICompanyTypeService _iCompanyTypeService;

        public CompanyTypeController(IUnitOfWork iUnitOfWork, ICompanyTypeService iCompanyTypeService) : base(iUnitOfWork)
        {
            _iCompanyTypeService = iCompanyTypeService;
        }

        #region View function
        [AuthorizeFilter("companytype:view")]
        public IActionResult CompanyTypeIndex()
        {
            return View();
        }

        [AuthorizeFilter("companytype:view")]
        public IActionResult CompanyTypeForm()
        {
            return View();
        }
        #endregion

        #region Get data
        [HttpGet]
        [AuthorizeFilter("companytype:search,user:search")]
        public async Task<IActionResult> GetListJson(CompanyTypeListParam param)
        {
            TData<List<CompanyTypeEntity>> obj = await _iCompanyTypeService.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("companytype:search,user:search")]
        public async Task<IActionResult> GetPageListJson(CompanyTypeListParam param, Pagination pagination)
        {
            TData<List<CompanyTypeEntity>> obj = await _iCompanyTypeService.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("companytype:view")]
        public async Task<IActionResult> GetFormJson(long id)
        {
            TData<CompanyTypeEntity> obj = await _iCompanyTypeService.GetEntity(id);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("companytype:view")]
        public async Task<IActionResult> GetCompanyTypeName(CompanyTypeListParam param)
        {
            TData<string> obj = new TData<string>();
            var list = await _iCompanyTypeService.GetList(param);
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
        [AuthorizeFilter("companytype:add,companytype:edit")]
        public async Task<IActionResult> SaveFormJson(CompanyTypeEntity entity)
        {
            TData<string> obj = await _iCompanyTypeService.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("companytype:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await _iCompanyTypeService.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}