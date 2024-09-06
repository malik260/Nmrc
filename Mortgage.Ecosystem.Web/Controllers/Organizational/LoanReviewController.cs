using Microsoft.AspNetCore.Mvc;
using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.BusinessLogic.Layer.Services;
using Mortgage.Ecosystem.DataAccess.Layer.Enums;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;
using Mortgage.Ecosystem.Web.Filter;

namespace Mortgage.Ecosystem.Web.Controllers.Systems
{
    public class LoanReviewController : BaseController
    {
        private readonly ILoanReviewService _iLoanReviewService;
        private readonly IAuditTrailService _iAuditTrailService;

        public LoanReviewController(IUnitOfWork iUnitOfWork, ILoanReviewService iLoanReviewService, IAuditTrailService iAuditTrailService) : base(iUnitOfWork)
        {
            _iLoanReviewService = iLoanReviewService;
            _iAuditTrailService = iAuditTrailService;
        }

        #region View function
        [AuthorizeFilter("loanreview:view")]
        public IActionResult LoanReviewIndex()
        {
            return View();
        }

        [AuthorizeFilter("loanreview:view")]
        public IActionResult LoanReviewForm()
        {
            return View();
        }
        #endregion

        #region Get data
        [HttpGet]
        //[AuthorizeFilter("gender:search,user:search")]
        public async Task<IActionResult> GetListJson(LoanReviewListParam param)
        {
            TData<List<LoanReviewEntity>> obj = await _iLoanReviewService.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("loanreview:search,user:search")]
        public async Task<IActionResult> GetPageListJson(LoanReviewListParam param, Pagination pagination)
        {
            TData<List<LoanReviewEntity>> obj = await _iLoanReviewService.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("loanreview:view")]
        public async Task<IActionResult> GetFormJson()
        {
            TData<LoanReviewEntity> obj = await _iLoanReviewService.GetEntity();
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("loanreview:view")]
        public async Task<IActionResult> GetLenderName(LoanReviewListParam param)
        {
            TData<string> obj = new TData<string>();
            var list = await _iLoanReviewService.GetList(param);
            if (list.Tag == 1)
            {
                obj.Data = string.Join(",", list.Data.Select(p => p.LenderID));
                obj.Tag = 1;
            }

            var auditInstance = new AuditTrailEntity();
            auditInstance.Action = SystemOperationCode.GetLoanReviewName.ToString();
            auditInstance.ActionRoute = SystemOperationCode.LoanReview.ToString();

            var audit = await _iAuditTrailService.SaveForm(auditInstance);
            return Json(obj);
        }
        #endregion

        #region Submit data
        [HttpPost]
        [AuthorizeFilter("loanreview:add,refinancing:edit")]
        public async Task<IActionResult> SaveFormJson(LoanReviewEntity entity)
        {
            TData<string> obj = await _iLoanReviewService.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("loanreview:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await _iLoanReviewService.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}