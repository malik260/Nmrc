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
    public class LoanScheduleController : BaseController
    {
        private readonly ILoanScheduleService _iLoanScheduleService;
        private readonly ILoanInitiationService iloanInitiationService;
        public LoanScheduleController(IUnitOfWork iUnitOfWork, ILoanScheduleService iLoanScheduleService, ILoanInitiationService loanInitiationService) : base(iUnitOfWork)
        {
            _iLoanScheduleService = iLoanScheduleService;
            iloanInitiationService = loanInitiationService; ;
        }


        #region View function
        [AuthorizeFilter("loanschedule:view")]
        public IActionResult LoanScheduleIndex()
        {
            return View();
        }

        public IActionResult LoanScheduleForm()
        {
            return View();
        }


        #endregion

        #region Get data
        [HttpGet]
        [AuthorizeFilter("loanschedule:search,user:search")]
        public async Task<IActionResult> GetListJson(LoanScheduleListParam param)
        {
            TData<List<LoanScheduleEntity>> obj = await _iLoanScheduleService.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("loanshedule:search,user:search")]
        public async Task<IActionResult> GetLoanSchedulePageListJson(LoanScheduleListParam param, Pagination pagination)
        {
            TData<List<LoanScheduleEntity>> obj = await _iLoanScheduleService.GetPageList(param, pagination);
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
        [AuthorizeFilter("loanschedule:search,user:search")]
        public async Task<IActionResult> GetLoanInitiationTreeListJson(LoanScheduleListParam param)
        {
            TData<List<ZtreeInfo>> obj = await _iLoanScheduleService.GetZtreeLoanScheduleList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("loanschedule:view")]
        public async Task<IActionResult> GetUserTreeListJson(LoanScheduleListParam param)
        {
            TData<List<ZtreeInfo>> obj = await _iLoanScheduleService.GetZtreeUserList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("loanschedule:view")]
        public async Task<IActionResult> GetFormJson(int id)
        {
            TData<LoanScheduleEntity> obj = await _iLoanScheduleService.GetEntity(id);
            return Json(obj);
        }

        [HttpGet]
        public async Task<IActionResult> GetMaxSortJson()
        {
            TData<int> obj = await _iLoanScheduleService.GetMaxSort();
            return Json(obj);
        }
        #endregion

        #region Submit data
        [HttpPost]
        [AuthorizeFilter("loanschedule:add,loanschedule:edit")]
        public async Task<IActionResult> SaveFormJson(LoanScheduleEntity entity)
        {
            TData<string> obj = await _iLoanScheduleService.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("loanschedule:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await _iLoanScheduleService.DeleteForm(ids);
            return Json(obj);
        }
        #endregion

        #region Existing Loan 
        public async Task<IActionResult> GetCustomerLoan(string nhfNo)
        {
            TData<List<LoanApplications>> obj = await iloanInitiationService.GetLoans(nhfNo);
            return Json(obj);
        }

        #endregion

        #region Loan Schedule  
        public async Task<IActionResult> GetCustomerLoanSchedule(string nhfNo)
        {
            TData<List<LoanSchedule>> obj = await _iLoanScheduleService.LoanSchedule(nhfNo);
            return Json(obj);
        }

        #endregion
    }
}