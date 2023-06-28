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
    public class CompanyClassController : BaseController
    {
        private readonly ICompanyClassService _iCompanyClassService;

        public CompanyClassController(IUnitOfWork iUnitOfWork, ICompanyClassService iCompanyClassService) : base(iUnitOfWork)
        {
            _iCompanyClassService = iCompanyClassService;
        }

        #region View function
        [AuthorizeFilter("companyclass:view")]
        public IActionResult CompanyClassIndex()
        {
            return View();
        }

        [AuthorizeFilter("companyclass:view")]
        public IActionResult CompanyClassForm()
        {
            return View();
        }
        #endregion

        #region Get data
        [HttpGet]
        [AuthorizeFilter("companyclass:search,user:search")]
        public async Task<IActionResult> GetListJson(CompanyClassListParam param)
        {
            TData<List<CompanyClassEntity>> obj = await _iCompanyClassService.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("companyclass:search,user:search")]
        public async Task<IActionResult> GetPageListJson(CompanyClassListParam param, Pagination pagination)
        {
            TData<List<CompanyClassEntity>> obj = await _iCompanyClassService.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("companyclass:view")]
        public async Task<IActionResult> GetFormJson(long id)
        {
            TData<CompanyClassEntity> obj = await _iCompanyClassService.GetEntity(id);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("companyclass:view")]
        public async Task<IActionResult> GetCompanyClassName(CompanyClassListParam param)
        {
            TData<string> obj = new TData<string>();
            var list = await _iCompanyClassService.GetList(param);
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
        [AuthorizeFilter("companyclass:add,companyclass:edit")]
        public async Task<IActionResult> SaveFormJson(CompanyClassEntity entity)
        {
            TData<string> obj = await _iCompanyClassService.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("companyclass:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await _iCompanyClassService.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}