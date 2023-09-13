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
    public class CreditTypeController : BaseController
    {
        private readonly ICreditTypeService _iCreditTypeService;

        public CreditTypeController(IUnitOfWork iUnitOfWork, ICreditTypeService iCreditTypeService) : base(iUnitOfWork)
        {
            _iCreditTypeService = iCreditTypeService;
        }

        #region View function
        [AuthorizeFilter("credittype:view")]
        public IActionResult CreditTypeIndex()
        {
            return View();
        }

        public IActionResult CreditTypeForm()
        {
            return View();
        }
        #endregion

        #region Get data
        [HttpGet]
        [AuthorizeFilter("credittype:search,user:search")]
        public async Task<IActionResult> GetListJson(CreditTypeListParam param)
        {
            TData<List<CreditTypeEntity>> obj = await _iCreditTypeService.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("credittype:search,user:search")]
        public async Task<IActionResult> GetCreditTypePageListJson(CreditTypeListParam param, Pagination pagination)
        {
            TData<List<CreditTypeEntity>> obj = await _iCreditTypeService.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("credittype:search,user:search")]
        public async Task<IActionResult> GetCreditTypeTreeListJson(CreditTypeListParam param)
        {
            TData<List<ZtreeInfo>> obj = await _iCreditTypeService.GetZtreeCreditTypeList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("credittype:search,user:search")]
        public async Task<IActionResult> GetPageListJson(CreditTypeListParam param, Pagination pagination)
        {
            TData<List<CreditTypeEntity>> obj = await _iCreditTypeService.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("credittype:view")]
        public async Task<IActionResult> GetUserTreeListJson(CreditTypeListParam param)
        {
            TData<List<ZtreeInfo>> obj = await _iCreditTypeService.GetZtreeUserList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("credittype:view")]
        public async Task<IActionResult> GetFormJson(string code)
        {
            TData<CreditTypeEntity> obj = await _iCreditTypeService.GetEntity(code);
            return Json(obj);
        }

        //[HttpGet]
        //public async Task<IActionResult> GetMaxSortJson()
        //{
        //    TData<int> obj = await _iCreditTypeService.GetMaxSort();
        //    return Json(obj);
        //}
        #endregion

        #region Submit data
        [HttpPost]
        [AuthorizeFilter("credittype:add,company:edit")]
        public async Task<IActionResult> SaveFormJson(CreditTypeEntity entity)
        {
            TData<string> obj = await _iCreditTypeService.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("credittype:add,employee:edit")]
        public async Task<IActionResult> UpdateFormJson(CreditTypeEntity entity)
        {
            TData<string> obj = await _iCreditTypeService.UpdateForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("credittype:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await _iCreditTypeService.DeleteForm(ids);
            return Json(obj);
        }


        #endregion
    }
}
