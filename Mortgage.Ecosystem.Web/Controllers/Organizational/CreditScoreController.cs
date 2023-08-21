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
    public class CreditScoreController : BaseController
    {
        private readonly ICreditScoreService _iCreditScoreService;

        public CreditScoreController(IUnitOfWork iUnitOfWork, ICreditScoreService iCreditScoreService) : base(iUnitOfWork)
        {
            _iCreditScoreService = iCreditScoreService;
        }

        #region View function
        [AuthorizeFilter("creditscore:view")]
        public IActionResult CreditScoreIndex()
        {
            return View();
        }

        public IActionResult CreditScoreForm()
        {
            return View();
        }


        #endregion

        #region Get data
        [HttpGet]
        [AuthorizeFilter("creditscore:search,user:search")]
        public async Task<IActionResult> GetListJson(CreditScoreListParam param)
        {
            TData<List<CreditScoreEntity>> obj = await _iCreditScoreService.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("creditscore:search,user:search")]
        public async Task<IActionResult> GetCreditScorePageListJson(CreditScoreListParam param, Pagination pagination)
        {
            TData<List<CreditScoreEntity>> obj = await _iCreditScoreService.GetPageList(param, pagination);
            return Json(obj);
        }




        [HttpGet]
        [AuthorizeFilter("creditscore:search,user:search")]
        public async Task<IActionResult> GetCreditScoreTreeListJson(CreditScoreListParam param)
        {
            TData<List<ZtreeInfo>> obj = await _iCreditScoreService.GetZtreeCreditScoreList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("creditscore:view")]
        public async Task<IActionResult> GetUserTreeListJson(CreditScoreListParam param)
        {
            TData<List<ZtreeInfo>> obj = await _iCreditScoreService.GetZtreeUserList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("creditscore:view")]
        public async Task<IActionResult> GetFormJson(int id)
        {
            TData<CreditScoreEntity> obj = await _iCreditScoreService.GetEntity(id);
            return Json(obj);
        }

        [HttpGet]
        public async Task<IActionResult> GetMaxSortJson()
        {
            TData<int> obj = await _iCreditScoreService.GetMaxSort();
            return Json(obj);
        }
        #endregion

        #region Submit data
        [HttpPost]
        [AuthorizeFilter("creditscore:add,creditscore:edit")]
        public async Task<IActionResult> SaveFormJson(CreditScoreEntity entity)
        {
            TData<string> obj = await _iCreditScoreService.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("creditscore:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await _iCreditScoreService.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}