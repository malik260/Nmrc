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
    public class AccreditationFeeController : BaseController
    {
        private readonly IAccreditationFeeService _iAccreditationFeeService;

        public AccreditationFeeController(IUnitOfWork iUnitOfWork, IAccreditationFeeService iAccreditationFeeService) : base(iUnitOfWork)
        {
            _iAccreditationFeeService = iAccreditationFeeService;
        }

        #region View function
        [AuthorizeFilter("accreditationfee:view")]
        public IActionResult AccreditationFeeIndex()
        {
            return View();
        }

        public IActionResult accreditationFeeForm()
        {
            return View();
        }

       
        #endregion

        #region Get data
        [HttpGet]
        [AuthorizeFilter("accreditationfee:search,user:search")]
        public async Task<IActionResult> GetListJson(AccreditationFeeListParam param)
        {
            TData<List<AccreditationFeeEntity>> obj = await _iAccreditationFeeService.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("accreditationfee:search,user:search")]
        public async Task<IActionResult> GetaccreditationFeePageListJson(AccreditationFeeListParam param, Pagination pagination)
        {
            TData<List<AccreditationFeeEntity>> obj = await _iAccreditationFeeService.GetPageList(param, pagination);
            return Json(obj);
        }




        [HttpGet]
        [AuthorizeFilter("accreditationfee:search,user:search")]
        public async Task<IActionResult> GetAccreditationFeeTreeListJson(AccreditationFeeListParam param)
        {
            TData<List<ZtreeInfo>> obj = await _iAccreditationFeeService.GetZtreeAccreditationFeeList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("accreditationfee:view")]
        public async Task<IActionResult> GetUserTreeListJson(AccreditationFeeListParam param)
        {
            TData<List<ZtreeInfo>> obj = await _iAccreditationFeeService.GetZtreeUserList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("accreditationfee:view")]
        public async Task<IActionResult> GetFormJson(int id)
        {
            TData<AccreditationFeeEntity> obj = await _iAccreditationFeeService.GetEntity(id);
            return Json(obj);
        }

        [HttpGet]
        public async Task<IActionResult> GetMaxSortJson()
        {
            TData<int> obj = await _iAccreditationFeeService.GetMaxSort();
            return Json(obj);
        }
        #endregion

        #region Submit dataA


        [HttpPost]
        [AuthorizeFilter("accreditationfee:add,accreditationfee:edit")]
        public async Task<IActionResult> SaveFormJson(AccreditationFeeEntity entity)
        {
            TData<string> obj = await _iAccreditationFeeService.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("accreditationfee:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await _iAccreditationFeeService.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}