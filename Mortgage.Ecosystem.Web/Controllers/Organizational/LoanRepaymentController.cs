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
    public class LoanRepaymentController : BaseController
    {
        private readonly ILoanRepaymentService _iLoanRepaymentService;

        public LoanRepaymentController(IUnitOfWork iUnitOfWork, ILoanRepaymentService iLoanRepaymentService) : base(iUnitOfWork)
        {
            _iLoanRepaymentService = iLoanRepaymentService;
        }

        #region View function
        [AuthorizeFilter("loanRepayment:view")]
        public IActionResult LoanRepaymentIndex()
        {
            return View();
        }

        public IActionResult LoanRepaymentForm()
        {
            return View();
        }
        #endregion

        #region Get data
        [HttpGet]
        [AuthorizeFilter("loanRepayment:search,user:search")]
        public async Task<IActionResult> GetListJson(LoanRepaymentListParam param)
        {
            TData<List<LoanRepaymentEntity>> obj = await _iLoanRepaymentService.GetList(param);
            return Json(obj);
        }


        [HttpGet]
        [AuthorizeFilter("loanRepayment:search,user:search")]
        public async Task<IActionResult> GetLoanRepaymentPageListJson(LoanRepaymentListParam param, Pagination pagination)
        {
            TData<List<LoanRepaymentEntity>> obj = await _iLoanRepaymentService.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("loanRepayment:search,user:search")]
        public async Task<IActionResult> GetCompanyTreeListJson(LoanRepaymentListParam param)
        {
            TData<List<ZtreeInfo>> obj = await _iLoanRepaymentService.GetZtreeLoanRepaymentList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("loanRepayment:view")]
        public async Task<IActionResult> GetUserTreeListJson(LoanRepaymentListParam param)
        {
            TData<List<ZtreeInfo>> obj = await _iLoanRepaymentService.GetZtreeUserList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("loanRepayment:view")]
        public async Task<IActionResult> GetFormJson(int id)
        {
            TData<LoanRepaymentEntity> obj = await _iLoanRepaymentService.GetEntity(id);
            return Json(obj);
        }

        [HttpGet]
        public async Task<IActionResult> GetMaxSortJson()
        {
            TData<int> obj = await _iLoanRepaymentService.GetMaxSort();
            return Json(obj);
        }
        #endregion

        #region Submit data
        [HttpPost]
        [AuthorizeFilter("loanRepayment:add,company:edit")]
        public async Task<IActionResult> SaveFormJson(LoanRepaymentEntity entity)
        {
            TData<string> obj = await _iLoanRepaymentService.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("loanRepayment:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await _iLoanRepaymentService.DeleteForm(ids);
            return Json(obj);
        }
        #endregion

        #region
        [HttpPost]
        public async Task<IActionResult> SingleLoanRepayment(LoanRepaymentDto entity)
        {
            TData obj = await _iLoanRepaymentService.SingleLoanRepayment(entity);
            return Json(obj);
        }
        #endregion
    }
}