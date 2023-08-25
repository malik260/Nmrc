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
    public class RefundController : BaseController
    {
        private readonly IRefundService _iRefundService;

        public RefundController(IUnitOfWork iUnitOfWork, IRefundService iRefundService) : base(iUnitOfWork)
        {
            _iRefundService = iRefundService;
        }

        #region View function

        [AuthorizeFilter("refund:view")]
        public IActionResult RefundIndex()
        {
            return View();
        }

        public IActionResult RefundForm()
        {
            return View();
        }

        #endregion View function

        #region Get data

        [HttpGet]
        [AuthorizeFilter("refund:search,user:search")]
        public async Task<IActionResult> GetListJson(RefundListParam param)
        {
            TData<List<RefundEntity>> obj = await _iRefundService.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("eticket:search,user:search")]
        public async Task<IActionResult> GetRefundPageListJson(RefundListParam param, Pagination pagination)
        {
            TData<List<RefundEntity>> obj = await _iRefundService.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("refund:search,user:search")]
        public async Task<IActionResult> GetCompanyTreeListJson(RefundListParam param)
        {
            TData<List<ZtreeInfo>> obj = await _iRefundService.GetZtreeRefundList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("refund:view")]
        public async Task<IActionResult> GetUserTreeListJson(RefundListParam param)
        {
            TData<List<ZtreeInfo>> obj = await _iRefundService.GetZtreeUserList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("refund:view")]
        public async Task<IActionResult> GetFormJson(int id)
        {
            TData<RefundEntity> obj = await _iRefundService.GetEntity(id)
;
            return Json(obj);
        }

        [HttpGet]
        //[AuthorizeFilter("refund:view")]
        public async Task<IActionResult> ViewCustomerInformation()
        {
            TData<CustomerDetailsViewModel> obj = await _iRefundService.GetCustomerDetails();
            return Json(obj);
        }

        [HttpGet]
        public async Task<IActionResult> GetMaxSortJson()
        {
            TData<int> obj = await _iRefundService.GetMaxSort();
            return Json(obj);
        }

        #endregion Get data

        #region Submit data

        [HttpPost]
        [AuthorizeFilter("refund:add,company:edit")]
        public async Task<IActionResult> SaveFormJson(RefundEntity entity)
        {
            TData<string> obj = await _iRefundService.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("refund:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await _iRefundService.DeleteForm(ids);
            return Json(obj);
        }

        #endregion Submit data
    }
}