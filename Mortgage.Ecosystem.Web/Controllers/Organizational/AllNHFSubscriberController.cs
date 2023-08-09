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
    public class AllNHFSubscriberController : BaseController
    {
        private readonly IAllNHFSubscriberService _iAllNHFSubscriberService;
        

        public AllNHFSubscriberController(IUnitOfWork iUnitOfWork, IAllNHFSubscriberService iAllNHFSubscriberService) : base(iUnitOfWork)
        {
            _iAllNHFSubscriberService = iAllNHFSubscriberService;
        }

        #region View function
        [AuthorizeFilter("allnhfsubscriber:view")]
        public IActionResult AllNHFSubscriberIndex()
        {
            return View();
        }

        public IActionResult AllNHFSubcriberForm()
        {
            return View();
        }


        #endregion

        #region Get data
        [HttpGet]
        [AuthorizeFilter("allnhfsubscriber:search,user:search")]
        public async Task<IActionResult> GetListJson(AllNHFSubscriberListParam param)
        {
            TData<List<AllNHFSubscriberEntity>> obj = await _iAllNHFSubscriberService.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("allnhfsubscriber:search,user:search")]
        public async Task<IActionResult> GetEmployeePageListJson(AllNHFSubscriberListParam param, Pagination pagination)
        {
            TData<List<AllNHFSubscriberEntity>> obj = await _iAllNHFSubscriberService.GetPageList(param, pagination);
            return Json(obj);
        }


        [HttpGet]
        [AuthorizeFilter("allnhfsubscriber:search,user:search")]
        public async Task<IActionResult> GetEmployerPageListJson(AllNHFSubscriberListParam param, Pagination pagination)
        {
            TData<List<AllNHFSubscriberEntity>> obj = await _iAllNHFSubscriberService.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("allnhfsubscriber:search,user:search")]
        public async Task<IActionResult> GetDeveloperPageListJson(AllNHFSubscriberListParam param, Pagination pagination)
        {
            TData<List<AllNHFSubscriberEntity>> obj = await _iAllNHFSubscriberService.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("allnhfsubscriber:search,user:search")]
        public async Task<IActionResult> GetPmbPageListJson(AllNHFSubscriberListParam param, Pagination pagination)
        {
            TData<List<AllNHFSubscriberEntity>> obj = await _iAllNHFSubscriberService.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("allnhfsubscriber:search,user:search")]
        public async Task<IActionResult> GetCooperativePageListJson(AllNHFSubscriberListParam param, Pagination pagination)
        {
            TData<List<AllNHFSubscriberEntity>> obj = await _iAllNHFSubscriberService.GetPageList(param, pagination);
            return Json(obj);
        }


        [HttpGet]
        [AuthorizeFilter("allnhfsubscriber:search,user:search")]
        public async Task<IActionResult> GetNHFRegUsersTreeListJson(AllNHFSubscriberListParam param)
        {
            TData<List<ZtreeInfo>> obj = await _iAllNHFSubscriberService.GetZtreeAllNHFSubscriberList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("allnhfsubscriber:view")]
        public async Task<IActionResult> GetUserTreeListJson(AllNHFSubscriberListParam param)
        {
            TData<List<ZtreeInfo>> obj = await _iAllNHFSubscriberService.GetZtreeUserList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("allnhfsubscriber:view")]
        public async Task<IActionResult> GetFormJson(int id)
        {
            TData<AllNHFSubscriberEntity> obj = await _iAllNHFSubscriberService.GetEntity(id);
            return Json(obj);
        }

        [HttpGet]
        public async Task<IActionResult> GetMaxSortJson()
        {
            TData<int> obj = await _iAllNHFSubscriberService.GetMaxSort();
            return Json(obj);
        }
        #endregion

        #region Submit data
        [HttpPost]
        [AuthorizeFilter("allnhfsubscriber:add,allnhfSubscriber:edit")]
        public async Task<IActionResult> SaveFormJson(AllNHFSubscriberEntity entity)
        {
            TData<string> obj = await _iAllNHFSubscriberService.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("allnhfsubscriber:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await _iAllNHFSubscriberService.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}