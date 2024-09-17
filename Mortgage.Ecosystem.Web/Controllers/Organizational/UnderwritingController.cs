using Microsoft.AspNetCore.Mvc;
using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.BusinessLogic.Layer.Services;
using Mortgage.Ecosystem.DataAccess.Layer.Enums;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;
using Mortgage.Ecosystem.Web.Filter;

namespace Mortgage.Ecosystem.Web.Controllers.Organizational
{
    [ExceptionFilter]
    public class UnderwritingController : BaseController
    {
        private readonly IUnderwritingService _iUnderwritingService;
        private readonly IAuditTrailService _iAuditTrailService;

        public UnderwritingController(IUnitOfWork iUnitOfWork, IUnderwritingService iUnderwritingService, IAuditTrailService iAuditTrailService) : base(iUnitOfWork)
        {
            _iUnderwritingService = iUnderwritingService;
            _iAuditTrailService = iAuditTrailService;
        }

        #region View function
        [AuthorizeFilter("underwriting:view")]
        public IActionResult UnderwritingIndex()
        {
            return View();
        }


        [AuthorizeFilter("LoanApproval:view")]
        public IActionResult LoanApprovalIndex()
        {
            return View();
        }

        [AuthorizeFilter("LoanReview:view")]
        public IActionResult LoanReviewIndex()
        {
            return View();
        }

        //[AuthorizeFilter("LoanReview:view")]
        public IActionResult BatchedLoans()
        {
            return View();
        }



        [AuthorizeFilter("LoanBatching:view")]
        public IActionResult LoanBatchingIndex()
        {
            return View();
        }

        [AuthorizeFilter("LoanDisbursement:view")]
        public IActionResult NonMortgageDisbursment()
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
            try
            {
                TData<List<UnderwritingEntity>> obj = await _iUnderwritingService.GetPageList(param, pagination);
                var auditInstance = new AuditTrailEntity();
                auditInstance.Action = SystemOperationCode.GetUnderwritingPageListJson.ToString();
                auditInstance.ActionRoute = SystemOperationCode.Underwriting.ToString();

                var audit = await _iAuditTrailService.SaveForm(auditInstance);
                return Json(obj);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpGet]
        //[AuthorizeFilter("employee:search,user:search")]
        public async Task<IActionResult> GetApprovalPageListJson(EmployeeListParam param, Pagination pagination)
        {
            try
            {
                TData<List<UnderwritingEntity>> obj = await _iUnderwritingService.GetApprovalPageList();
                return Json(obj);
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        [HttpGet]
        //[AuthorizeFilter("propertyregistration:view")]
        public async Task<IActionResult> GetBatchedLoans(long id)
        {
            //throw new NotImplementedException();
            try
            {
                TData<List<UnderwritingEntity>> obj = await _iUnderwritingService.GetBatchedLoans(id);
                var auditInstance = new AuditTrailEntity();
                auditInstance.Action = SystemOperationCode.GetBatchedLoans.ToString();
                auditInstance.ActionRoute = SystemOperationCode.Underwriting.ToString();

                var audit = await _iAuditTrailService.SaveForm(auditInstance);
                return Json(obj);

            }
            catch (Exception ex)
            {

                throw;
            }
        }


        [HttpGet]
        //[AuthorizeFilter("employee:search,user:search")]
        public async Task<IActionResult> GetLoanForReview(EmployeeListParam param, Pagination pagination)
        {
            try
            {
                TData<List<UnderwritingEntity>> obj = await _iUnderwritingService.GetLoanForReview();
                var auditInstance = new AuditTrailEntity();
                auditInstance.Action = SystemOperationCode.GetLoanForReview.ToString();
                auditInstance.ActionRoute = SystemOperationCode.Underwriting.ToString();

                var audit = await _iAuditTrailService.SaveForm(auditInstance);
                return Json(obj);
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public async Task<IActionResult> GetLoanForBatching(EmployeeListParam param, Pagination pagination)
        {
            try
            {
                TData<List<UnderwritingEntity>> obj = await _iUnderwritingService.GetLoanForBatching();
                var auditInstance = new AuditTrailEntity();
                auditInstance.Action = SystemOperationCode.GetLoanForBatching.ToString();
                auditInstance.ActionRoute = SystemOperationCode.Underwriting.ToString();

                var audit = await _iAuditTrailService.SaveForm(auditInstance);
                return Json(obj);
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public async Task<IActionResult> GetLoanNonMortgageForDis(EmployeeListParam param, Pagination pagination)
        {
            try
            {
                TData<List<UnderwritingEntity>> obj = await _iUnderwritingService.GetLoanForDisbursment();
                var auditInstance = new AuditTrailEntity();
                auditInstance.Action = SystemOperationCode.GetLoanForBatching.ToString();
                auditInstance.ActionRoute = SystemOperationCode.Underwriting.ToString();

                var audit = await _iAuditTrailService.SaveForm(auditInstance);
                return Json(obj);
            }
            catch (Exception ex)
            {

                throw;
            }
        }



        public async Task<IActionResult> GetBatchedLoan(EmployeeListParam param, Pagination pagination)
        {
            try
            {
                TData<List<UnderwritingEntity>> obj = await _iUnderwritingService.GetBatched();
                var auditInstance = new AuditTrailEntity();
                auditInstance.Action = SystemOperationCode.GetBatchedLoan.ToString();
                auditInstance.ActionRoute = SystemOperationCode.Underwriting.ToString();

                var audit = await _iAuditTrailService.SaveForm(auditInstance);
                return Json(obj);

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpGet]
        //[AuthorizeFilter("employee:search,user:search")]
        public async Task<IActionResult> GetLoanForUnderwriting(EmployeeListParam param, Pagination pagination)
        {
            try
            {
                TData<List<UnderwritingEntity>> obj = await _iUnderwritingService.GetLoanForUnderwriting();
                var auditInstance = new AuditTrailEntity();
                auditInstance.Action = SystemOperationCode.GetLoanForUnderwriting.ToString();
                auditInstance.ActionRoute = SystemOperationCode.Underwriting.ToString();

                var audit = await _iAuditTrailService.SaveForm(auditInstance);
                return Json(obj);
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        //[HttpPost]
        //public async Task<IActionResult> ApproveLoan(string id)
        //{

        //    TData<String> obj = await _iUnderwritingService.ProceedLoan(id);
        //    return Json(obj);
        //}


        [HttpPost]
        public async Task<IActionResult> BatchLoan(string lists)
        {

            try
            {
                TData<String> obj = await _iUnderwritingService.batchLoan(lists);
                var auditInstance = new AuditTrailEntity();
                auditInstance.Action = SystemOperationCode.BatchLoan.ToString();
                auditInstance.ActionRoute = SystemOperationCode.Underwriting.ToString();

                var audit = await _iAuditTrailService.SaveForm(auditInstance);

                return Json(obj);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> UnBatchLoan(string lists)
        {

            try
            {
                TData<String> obj = await _iUnderwritingService.UnbatchLoan(lists);
                var auditInstance = new AuditTrailEntity();
                auditInstance.Action = SystemOperationCode.UnBatchLoan.ToString();
                auditInstance.ActionRoute = SystemOperationCode.Underwriting.ToString();

                var audit = await _iAuditTrailService.SaveForm(auditInstance);
                return Json(obj);
            }
            catch (Exception ex)
            {

                throw;
            }
        }



        [HttpPost]
        public async Task<IActionResult> ApplyLoan(string lists)
        {

            try
            {
                TData<String> obj = await _iUnderwritingService.ApproveBatchedLoan(lists);
                var auditInstance = new AuditTrailEntity();
                auditInstance.Action = SystemOperationCode.ApplyLoan.ToString();
                auditInstance.ActionRoute = SystemOperationCode.Underwriting.ToString();

                var audit = await _iAuditTrailService.SaveForm(auditInstance);
                return Json(obj);

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> ApproveReview(long id)
        {

            try
            {
                TData<String> obj = await _iUnderwritingService.ApproveLoanReview(id);
                var auditInstance = new AuditTrailEntity();
                auditInstance.Action = SystemOperationCode.ApproveReview.ToString();
                auditInstance.ActionRoute = SystemOperationCode.Underwriting.ToString();

                var audit = await _iAuditTrailService.SaveForm(auditInstance);
                return Json(obj);
            }
            catch (Exception ex)
            {

                throw;
            }
        }



        [HttpPost]
        public async Task<IActionResult> ApproveUnderwriting(long id)
        {

            try
            {
                TData<String> obj = await _iUnderwritingService.ApproveUnderwriting(id);
                var auditInstance = new AuditTrailEntity();
                auditInstance.Action = SystemOperationCode.ApproveUnderwriting.ToString();
                auditInstance.ActionRoute = SystemOperationCode.Underwriting.ToString();

                var audit = await _iAuditTrailService.SaveForm(auditInstance);
                return Json(obj);
            }
            catch (Exception ex)
            {

                throw;
            }
        }




        public async Task<IActionResult> DisburseNonNhfLoan(long id)
        {

            try
            {
                TData<String> obj = await _iUnderwritingService.DisburseNonNhfLoan(id);

                return Json(obj);
            }
            catch (Exception ex)
            {

                throw;
            }
        }



        [HttpPost]
        public async Task<IActionResult> DisApproveReview(long id, string remark)
        {

            try
            {
                TData<String> obj = await _iUnderwritingService.RejectLoanReview(id, remark);
                var auditInstance = new AuditTrailEntity();
                auditInstance.Action = SystemOperationCode.DisApproveReview.ToString();
                auditInstance.ActionRoute = SystemOperationCode.Underwriting.ToString();

                var audit = await _iAuditTrailService.SaveForm(auditInstance);
                return Json(obj);
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        [HttpPost]
        public async Task<IActionResult> DisapproveUnderwriting(long id, string remark)
        {

            try
            {
                TData<String> obj = await _iUnderwritingService.RejectLoanUnderwriting(id, remark);
                var auditInstance = new AuditTrailEntity();
                auditInstance.Action = SystemOperationCode.DisApproveReview.ToString();
                auditInstance.ActionRoute = SystemOperationCode.Underwriting.ToString();

                var audit = await _iAuditTrailService.SaveForm(auditInstance);
                return Json(obj);
            }
            catch (Exception ex)
            {

                throw;
            }
        }



        [HttpPost]
        public async Task<IActionResult> Affordability(string id)
        {

            try
            {
                TData<List<AffordabilityDetails>> obj = await _iUnderwritingService.PerformAffordability(id);
                var auditInstance = new AuditTrailEntity();
                auditInstance.Action = SystemOperationCode.PerformAffordability.ToString();
                auditInstance.ActionRoute = SystemOperationCode.Underwriting.ToString();

                var audit = await _iAuditTrailService.SaveForm(auditInstance);
                return Json(obj);
            }
            catch (Exception ex)
            {

                throw;
            }
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
            try
            {
                TData<string> obj = await _iUnderwritingService.SaveForm(entity);
                return Json(obj);
            }
            catch (Exception ex)
            {

                throw;
            }
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