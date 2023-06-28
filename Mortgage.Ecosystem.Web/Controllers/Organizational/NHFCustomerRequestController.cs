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
    public class NHFCustomerRequestController : BaseController
    {
        private readonly INHFCustomerRequestService _iNHFCustomerRequestService;

        public NHFCustomerRequestController(IUnitOfWork iUnitOfWork, INHFCustomerRequestService iNHFCustomerRequestService) : base(iUnitOfWork)
        {
            _iNHFCustomerRequestService = iNHFCustomerRequestService;
        }

        #region View function
        [AuthorizeFilter("nhfcustomerrequest:view")]
        public IActionResult NHFCustomerRequestIndex()
        {
            return View();
        }

        public IActionResult NHFCustomerRequestForm()
        {
            return View();
        }


        #endregion

        #region Get data
        [HttpGet]
        [AuthorizeFilter("nhfcustomerrequest:search,user:search")]
        public async Task<IActionResult> GetListJson(NHFCustomerRequestListParam param)
        {
            TData<List<NHFCustomerRequestEntity>> obj = await _iNHFCustomerRequestService.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("nhfcustomerrequest:search,user:search")]
        public async Task<IActionResult> GetContributionHistoryPageListJson(NHFCustomerRequestListParam param, Pagination pagination)
        {
            TData<List<NHFCustomerRequestEntity>> obj = await _iNHFCustomerRequestService.GetPageList(param, pagination);
            return Json(obj);
        }





        [HttpGet]
        [AuthorizeFilter("nhfcustomerrequest:search,user:search")]
        public async Task<IActionResult> GetNHFCustomerRequestTreeListJson(NHFCustomerRequestListParam param)
        {
            TData<List<ZtreeInfo>> obj = await _iNHFCustomerRequestService.GetZtreeNHFCustomerRequestList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("nhfcustomerrequest:view")]
        public async Task<IActionResult> GetUserTreeListJson(NHFCustomerRequestListParam param)
        {
            TData<List<ZtreeInfo>> obj = await _iNHFCustomerRequestService.GetZtreeUserList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("nhfcustomerrequest:view")]
        public async Task<IActionResult> GetFormJson(int id)
        {
            TData<NHFCustomerRequestEntity> obj = await _iNHFCustomerRequestService.GetEntity(id);
            return Json(obj);
        }

        [HttpGet]
        public async Task<IActionResult> GetMaxSortJson()
        {
            TData<int> obj = await _iNHFCustomerRequestService.GetMaxSort();
            return Json(obj);
        }
        #endregion

        #region Submit data
        [HttpPost]
        [AuthorizeFilter("nhfcustomerrequest:add,nhfcustomerrequest:edit")]
        public async Task<IActionResult> SaveFormJson(NHFCustomerRequestEntity entity)
        {
            TData<string> obj = await _iNHFCustomerRequestService.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("nhfcustomerrequest:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await _iNHFCustomerRequestService.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}
