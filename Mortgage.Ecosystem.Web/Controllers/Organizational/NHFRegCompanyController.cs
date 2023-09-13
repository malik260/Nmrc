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
    public class NHFRegCompanyController : BaseController
    {
        private readonly INHFRegCompanyService _iNHFRegCompanyService;

        public NHFRegCompanyController(IUnitOfWork iUnitOfWork, INHFRegCompanyService iNHFRegCompanyService) : base(iUnitOfWork)
        {
            _iNHFRegCompanyService = iNHFRegCompanyService;
        }

        #region View function
        [AuthorizeFilter("nhfregCompany:view")]
        public IActionResult NHFRegCompanyIndex()
        {
            return View();
        }

        public IActionResult NHFRegCompanyForm()
        {
            return View();
        }
        #endregion

        #region Get data
        [HttpGet]
        [AuthorizeFilter("nhfregcompany:search,user:search")]
        public async Task<IActionResult> GetListJson(NHFRegCompanyListParam param)
        {
            TData<List<NHFRegCompanyEntity>> obj = await _iNHFRegCompanyService.GetList(param);
            return Json(obj);
        }


        [HttpGet]
        [AuthorizeFilter("nhfregcompany:search,user:search")]
        public async Task<IActionResult> GetEmployeePageListJson(NHFRegCompanyListParam param, Pagination pagination)
        {
            TData<List<NHFRegCompanyEntity>> obj = await _iNHFRegCompanyService.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("nhfregusers:search,user:search")]
        public async Task<IActionResult> GetEmployerPageListJson(NHFRegCompanyListParam param, Pagination pagination)
        {
            TData<List<NHFRegCompanyEntity>> obj = await _iNHFRegCompanyService.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("nhfregusers:search,user:search")]
        public async Task<IActionResult> GetnhfregusersTreeListJson(NHFRegCompanyListParam param)
        {
            TData<List<ZtreeInfo>> obj = await _iNHFRegCompanyService.GetZtreeNHFRegCompanyList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("nhfregusers:view")]
        public async Task<IActionResult> GetUserTreeListJson(NHFRegCompanyListParam param)
        {
            TData<List<ZtreeInfo>> obj = await _iNHFRegCompanyService.GetZtreeUserList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("nhfregusers:view")]
        public async Task<IActionResult> GetFormJson(int id)
        {
            TData<NHFRegCompanyEntity> obj = await _iNHFRegCompanyService.GetEntity(id);
            return Json(obj);
        }

        [HttpGet]
        public async Task<IActionResult> GetMaxSortJson()
        {
            TData<int> obj = await _iNHFRegCompanyService.GetMaxSort();
            return Json(obj);
        }
        #endregion

        #region Submit data
        [HttpPost]
        [AuthorizeFilter("nhfregusers:add,nhfregusers:edit")]
        public async Task<IActionResult> SaveFormJson(NHFRegCompanyEntity entity)
        {
            TData<string> obj = await _iNHFRegCompanyService.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("nhfregusers:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await _iNHFRegCompanyService.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}