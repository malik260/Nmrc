using Microsoft.AspNetCore.Mvc;
using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;
using Mortgage.Ecosystem.Web.Filter;

namespace Mortgage.Ecosystem.Web.Controllers.Organizational
{
    public class CompanyController : BaseController
    {
        private readonly ICompanyService _iCompanyService;

        public CompanyController(IUnitOfWork iUnitOfWork, ICompanyService iCompanyService) : base(iUnitOfWork)
        {
            _iCompanyService = iCompanyService;
        }

        #region View function
        [AuthorizeFilter("company:view")]
        public IActionResult CompanyIndex()
        {
            return View();
        }

        public IActionResult CompanyForm()
        {
            return View();
        }

        [AuthorizeFilter("nhfdiaspora:view")]
        public IActionResult NhfDiasporaIndex()
        {
            return View();
        }

        public IActionResult NhfDiasporaForm()
        {
            return View();
        }

        [AuthorizeFilter("nhfemployee:view")]
        public IActionResult NhfEmployeeIndex()
        {
            return View();
        }

        public IActionResult NhfEmployeeForm()
        {
            return View();
        }
        #endregion

        #region Get data
        [HttpGet]
        [AuthorizeFilter("company:search,user:search")]
        public async Task<IActionResult> GetListJson(CompanyListParam param)
        {
            TData<List<CompanyEntity>> obj = await _iCompanyService.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("company:search,user:search")]
        public async Task<IActionResult> GetPageListJson(CompanyListParam param, Pagination pagination)
        {
            TData<List<CompanyEntity>> obj = await _iCompanyService.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("company:search,user:search")]
        public async Task<IActionResult> GetCompanyTreeListJson(CompanyListParam param)
        {
            TData<List<ZtreeInfo>> obj = await _iCompanyService.GetZtreeCompanyList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("company:view")]
        public async Task<IActionResult> GetUserTreeListJson(CompanyListParam param)
        {
            TData<List<ZtreeInfo>> obj = await _iCompanyService.GetZtreeUserList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("company:view")]
        public async Task<IActionResult> GetFormJson(int id)
        {
            TData<CompanyEntity> obj = await _iCompanyService.GetEntity(id);
            return Json(obj);
        }
        #endregion

        #region Submit data
        [HttpPost]
        [AuthorizeFilter("company:add,company:edit")]
        public async Task<IActionResult> SaveFormJson(CompanyEntity entity)
        {
            TData<string> obj = await _iCompanyService.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("company:add,company:edit")]
        public async Task<IActionResult> SaveFormsJson(CompanyEntity entity)
        {
            TData<string> obj = await _iCompanyService.SaveForms(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("company:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await _iCompanyService.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}