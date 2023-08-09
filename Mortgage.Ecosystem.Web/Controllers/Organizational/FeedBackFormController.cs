using Microsoft.AspNetCore.Mvc;
using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.BusinessLogic.Layer.Services;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.Web.Filter;

namespace Mortgage.Ecosystem.Web.Controllers.Organizational
{
    public class FeedBackFormController : BaseController
    {
        private readonly IFeedBackFormService _iFeedBackFormService;

        public FeedBackFormController(IUnitOfWork iUnitOfWork, IFeedBackFormService iFeedBackFormService) : base(iUnitOfWork)
        {
            _iFeedBackFormService = iFeedBackFormService;
        }

        #region View function
        [AuthorizeFilter("feedbackform:view")]
        public IActionResult FeedBackFormIndex()
        {
            return View();
        }

        public IActionResult FeedBackFormForm()
        {
            return View();
        }
        #endregion

        #region Get data
        [HttpGet]
        [AuthorizeFilter("feedbackform:search,user:search")]
        public async Task<IActionResult> GetListJson(FeedBackFormListParam param)
        {
            TData<List<FeedBackFormEntity>> obj = await _iFeedBackFormService.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("feedbackform:search,user:search")]
        public async Task<IActionResult> GetFeedBackFormTreeListJson(FeedBackFormListParam param)
        {
            TData<List<ZtreeInfo>> obj = await _iFeedBackFormService.GetZtreeFeedBackFormList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("feedbackform:view")]
        public async Task<IActionResult> GetUserTreeListJson(FeedBackFormListParam param)
        {
            TData<List<ZtreeInfo>> obj = await _iFeedBackFormService.GetZtreeUserList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("feedbackform:view")]
        public async Task<IActionResult> GetFormJson(int id)
        {
            TData<FeedBackFormEntity> obj = await _iFeedBackFormService.GetEntity(id);
            return Json(obj);
        }

        [HttpGet]
        public async Task<IActionResult> GetMaxSortJson()
        {
            TData<int> obj = await _iFeedBackFormService.GetMaxSort();
            return Json(obj);
        }
        #endregion

        #region Submit data
        [HttpPost]
        [AuthorizeFilter("feedbackform:add,company:edit")]
        public async Task<IActionResult> SaveFormJson(FeedBackFormEntity entity)
        {
            TData<string> obj = await _iFeedBackFormService.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("feedbackform:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await _iFeedBackFormService.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}