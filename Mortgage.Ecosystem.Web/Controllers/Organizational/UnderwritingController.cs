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
    public class UnderwritingController : BaseController
    {
        private readonly IUnderwritingService _iUnderwritingService;

        public UnderwritingController(IUnitOfWork iUnitOfWork, IUnderwritingService iUnderwritingService) : base(iUnitOfWork)
        {
            _iUnderwritingService = iUnderwritingService;
        }

        #region View function
        [AuthorizeFilter("underwriting:view")]
        public IActionResult UnderwritingIndex()
        {
            return View();
        }

        public IActionResult AddDocumentForm()
        {
            return View();
        }

        public IActionResult ChecklistForm()
        {
            return View();
        }

        public IActionResult RiskAssessmentForm()
        {
            return View();
        }


        #endregion

        #region Get data
        [HttpGet]
        [AuthorizeFilter("underwriting:search,user:search")]
        public async Task<IActionResult> GetListJson(UnderwritingListParam param)
        {
            TData<List<UnderwritingEntity>> obj = await _iUnderwritingService.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("underwriting:search,user:search")]
        public async Task<IActionResult> GetUnderwritingPageListJson(UnderwritingListParam param, Pagination pagination)
        {
            TData<List<UnderwritingEntity>> obj = await _iUnderwritingService.GetPageList(param, pagination);
            return Json(obj);
        }


        //[HttpGet]
        //[AuthorizeFilter("underwriting:search,user:search")]
        //public async Task<IActionResult> GetEtransactPageListJson(UnderwritingListParam param, Pagination pagination)
        //{
        //    TData<List<UnderwritingEntity>> obj = await _iUnderwritingService.GetPageList(param, pagination);
        //    return Json(obj);
        //}


        [HttpGet]
        [AuthorizeFilter("underwriting:search,user:search")]
        public async Task<IActionResult> GetunderwritingTreeListJson(UnderwritingListParam param)
        {
            TData<List<ZtreeInfo>> obj = await _iUnderwritingService.GetZtreeUnderwritingList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("underwriting:view")]
        public async Task<IActionResult> GetUserTreeListJson(UnderwritingListParam param)
        {
            TData<List<ZtreeInfo>> obj = await _iUnderwritingService.GetZtreeUserList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("underwriting:view")]
        public async Task<IActionResult> GetFormJson(int id)
        {
            TData<UnderwritingEntity> obj = await _iUnderwritingService.GetEntity(id);
            return Json(obj);
        }

        [HttpGet]
        public async Task<IActionResult> GetMaxSortJson()
        {
            TData<int> obj = await _iUnderwritingService.GetMaxSort();
            return Json(obj);
        }
        #endregion

        #region Submit data
        [HttpPost]
        [AuthorizeFilter("underwriting:add,underwriting:edit")]
        public async Task<IActionResult> SaveFormJson(UnderwritingEntity entity)
        {
            TData<string> obj = await _iUnderwritingService.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("underwriting:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await _iUnderwritingService.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}