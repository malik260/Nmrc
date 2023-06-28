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
    public class ApproveEmployerAggregatorController : BaseController
    {
        private readonly IApproveEmployerAggregatorService _iApproveEmployerAggregatorService;

        public ApproveEmployerAggregatorController(IUnitOfWork iUnitOfWork, IApproveEmployerAggregatorService iApproveEmployerAggregatorService) : base(iUnitOfWork)
        {
            _iApproveEmployerAggregatorService = iApproveEmployerAggregatorService;
        }

        #region View function
        [AuthorizeFilter("approveemployeraggregator:view")]
        public IActionResult ApproveEmployerAggregatorIndex()
        {
            return View();
        }

        public IActionResult ApproveEmployerAggregatorForm()
        {
            return View();
        }


        #endregion

        #region Get data
        [HttpGet]
        [AuthorizeFilter("approveemployeraggregator:search,user:search")]
        public async Task<IActionResult> GetListJson(ApproveEmployerAggregatorListParam param)
        {
            TData<List<ApproveEmployerAggregatorEntity>> obj = await _iApproveEmployerAggregatorService.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("approveemployeraggregator:search,user:search")]
        public async Task<IActionResult> GetApproveEmployerAggregatorPageListJson(ApproveEmployerAggregatorListParam param, Pagination pagination)
        {
            TData<List<ApproveEmployerAggregatorEntity>> obj = await _iApproveEmployerAggregatorService.GetPageList(param, pagination);
            return Json(obj);
        }


        //[HttpGet]
        //[AuthorizeFilter("loaninitiation:search,user:search")]
        //public async Task<IActionResult> GetEtransactPageListJson(PaymentHistoryListParam param, Pagination pagination)
        //{
        //    TData<List<PaymentHistoryEntity>> obj = await _iPaymentHistoryService.GetPageList(param, pagination);
        //    return Json(obj);
        //}


        [HttpGet]
        [AuthorizeFilter("approveemployeraggregator:search,user:search")]
        public async Task<IActionResult> GetApproveEmployerAggregatorTreeListJson(ApproveEmployerAggregatorListParam param)
        {
            TData<List<ZtreeInfo>> obj = await _iApproveEmployerAggregatorService.GetZtreeApproveEmployerAggregatorList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("approveemployeraggregator:view")]
        public async Task<IActionResult> GetUserTreeListJson(ApproveEmployerAggregatorListParam param)
        {
            TData<List<ZtreeInfo>> obj = await _iApproveEmployerAggregatorService.GetZtreeUserList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("approveemployeraggregator:view")]
        public async Task<IActionResult> GetFormJson(int id)
        {
            TData<ApproveEmployerAggregatorEntity> obj = await _iApproveEmployerAggregatorService.GetEntity(id);
            return Json(obj);
        }

        [HttpGet]
        public async Task<IActionResult> GetMaxSortJson()
        {
            TData<int> obj = await _iApproveEmployerAggregatorService.GetMaxSort();
            return Json(obj);
        }
        #endregion

        #region Submit data
        [HttpPost]
        [AuthorizeFilter("approveemployeraggregator:add,approveemployeraggregator:edit")]
        public async Task<IActionResult> SaveFormJson(ApproveEmployerAggregatorEntity entity)
        {
            TData<string> obj = await _iApproveEmployerAggregatorService.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("approveemployeraggregator:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await _iApproveEmployerAggregatorService.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}