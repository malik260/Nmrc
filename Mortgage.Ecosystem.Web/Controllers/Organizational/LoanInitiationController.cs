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
    public class LoanInitiationController : BaseController
    {
        private readonly ILoanInitiationService _iLoanInitiationService;

        public LoanInitiationController(IUnitOfWork iUnitOfWork, ILoanInitiationService iLoanInitiationService) : base(iUnitOfWork)
        {
            _iLoanInitiationService = iLoanInitiationService;
        }

        #region View function
        [AuthorizeFilter("loaninitiation:view")]
        public IActionResult LoanInitiationIndex()
        {
            return View();
        }

        public IActionResult LoanInitiationForm()
        {
            return View();
        }


        #endregion

        #region Get data
        [HttpGet]
        [AuthorizeFilter("loaninitiation:search,user:search")]
        public async Task<IActionResult> GetListJson(LoanInitiationListParam param)
        {
            TData<List<LoanInitiationEntity>> obj = await _iLoanInitiationService.GetList(param);
            return Json(obj);
        }

        //[HttpGet]
        //[AuthorizeFilter("paymenthistory:search,user:search")]
        //public async Task<IActionResult> GetRemitaPageListJson(LoanInitiationListParam param, Pagination pagination)
        //{
        //    TData<List<LoanInitiationEntity>> obj = await _iLoanInitiationService.GetPageList(param, pagination);
        //    return Json(obj);
        //}


        [HttpGet]
        [AuthorizeFilter("loaninitiation:search,user:search")]
        public async Task<IActionResult> GetLoanInitiationPageListJson(LoanInitiationListParam param, Pagination pagination)
        {
            TData<List<LoanInitiationEntity>> obj = await _iLoanInitiationService.GetPageList(param, pagination);
            return Json(obj);
        }


        [HttpGet]
        [AuthorizeFilter("loaninitiation:search,user:search")]
        public async Task<IActionResult> GetLoanInitiationTreeListJson(LoanInitiationListParam param)
        {
            TData<List<ZtreeInfo>> obj = await _iLoanInitiationService.GetZtreeLoanInitiationList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("loaninitiation:view")]
        public async Task<IActionResult> GetUserTreeListJson(LoanInitiationListParam param)
        {
            TData<List<ZtreeInfo>> obj = await _iLoanInitiationService.GetZtreeUserList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("loanInitiation:view")]
        public async Task<IActionResult> GetFormJson(int id)
        {
            TData<LoanInitiationEntity> obj = await _iLoanInitiationService.GetEntity(id);
            return Json(obj);
        }

        [HttpGet]
        public async Task<IActionResult> GetMaxSortJson()
        {
            TData<int> obj = await _iLoanInitiationService.GetMaxSort();
            return Json(obj);
        }
        #endregion

        #region Submit data
        [HttpPost]
        [AuthorizeFilter("loaninitiation:add,loaninitiation:edit")]
        public async Task<IActionResult> SaveFormJson(LoanInitiationEntity entity)
        {
            TData<string> obj = await _iLoanInitiationService.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        public async Task<IActionResult> PerformLoanAffordability(InitiateLoanDto initiateLoanDto)
        {
            TData obj = await _iLoanInitiationService.Performaffordability(initiateLoanDto);
            return Json(obj);
        }
        #endregion
        #region
        [HttpPost]
        public async Task<IActionResult> LoanInitiation( InitiateLoanDto initiateLoanDto)
        {
            TData obj = await _iLoanInitiationService.LoanApplication(initiateLoanDto);
            return Json(obj);
        }
        #endregion
    }
}