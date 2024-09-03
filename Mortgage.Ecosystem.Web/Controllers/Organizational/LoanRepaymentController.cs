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
using System.Net.Mime;

namespace Mortgage.Ecosystem.Web.Controllers.Organizational
{
    public class LoanRepaymentController : BaseController
    {
        private readonly ILoanRepaymentService _iLoanRepaymentService;
        private readonly IAuditTrailService _iAuditTrailService;

        public LoanRepaymentController(IUnitOfWork iUnitOfWork, ILoanRepaymentService iLoanRepaymentService, IAuditTrailService iAuditTrailService) : base(iUnitOfWork)
        {
            _iLoanRepaymentService = iLoanRepaymentService;
            _iAuditTrailService = iAuditTrailService;
        }

        #region View function
        [AuthorizeFilter("loanrepayment:view")]
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
        [AuthorizeFilter("loanrepayment:search,user:search")]
        public async Task<IActionResult> GetListJson(LoanRepaymentListParam param)
        {
            TData<List<LoanRepaymentEntity>> obj = await _iLoanRepaymentService.GetList(param);
            return Json(obj);
        }

        public async Task<FileResult> BatchDownload()
        {
            //var transaction = db.Database.BeginTransaction();

            string fileName;
            //fileName = "MultipleUpload.xlsx";
            fileName = "LoanRepaymentTemplate.xlsx";


            var path = Path.Combine(
               Directory.GetCurrentDirectory(),
               "wwwroot\\", fileName);

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;

            var auditInstance = new AuditTrailEntity();
            auditInstance.Action = SystemOperationCode.TemplateDownload.ToString();
            auditInstance.ActionRoute = SystemOperationCode.LoanRepayment.ToString();

            var audit = await _iAuditTrailService.SaveForm(auditInstance);

            //transaction.Commit();
            return File(memory, MediaTypeNames.Application.Octet, Path.GetFileName(path));
        }


        [HttpGet]
        [AuthorizeFilter("loanrepayment:search,user:search")]
        public async Task<IActionResult> GetLoanRepaymentPageListJson(LoanRepaymentListParam param, Pagination pagination)
        {
            TData<List<LoanRepaymentEntity>> obj = await _iLoanRepaymentService.GetPageList(param, pagination);
            var auditInstance = new AuditTrailEntity();
            auditInstance.Action = SystemOperationCode.GetLoanRepaymentPageListJson.ToString();
            auditInstance.ActionRoute = SystemOperationCode.LoanRepayment.ToString();

            var audit = await _iAuditTrailService.SaveForm(auditInstance);
            return Json(obj);
        }



        [HttpGet]
        [AuthorizeFilter("loanrepayment:search,user:search")]
        public async Task<IActionResult> GetCompanyTreeListJson(LoanRepaymentListParam param)
        {
            TData<List<ZtreeInfo>> obj = await _iLoanRepaymentService.GetZtreeLoanRepaymentList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("loanrepayment:view")]
        public async Task<IActionResult> GetUserTreeListJson(LoanRepaymentListParam param)
        {
            TData<List<ZtreeInfo>> obj = await _iLoanRepaymentService.GetZtreeUserList(param);
            return Json(obj);
        }


        [HttpGet]
        [AuthorizeFilter("loanrepayment:view")]
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

        #region LOAN REPAYMENT
        [HttpPost]
        public async Task<IActionResult> SingleLoanRepayment(LoanRepaymentDto entity)
        {
            TData obj = await _iLoanRepaymentService.SingleLoanRepayment(entity);
            var auditInstance = new AuditTrailEntity();
            auditInstance.Action = SystemOperationCode.SingleLoanRepayment.ToString();
            auditInstance.ActionRoute = SystemOperationCode.LoanRepayment.ToString();

            var audit = await _iAuditTrailService.SaveForm(auditInstance);
            return Json(obj);
        }

        [HttpPost]
        public async Task<IActionResult> BatchLoanRepayment(BatchUploadVM entity)
        {
            var auditInstance = new AuditTrailEntity();
            auditInstance.Action = SystemOperationCode.SingleLoanRepayment.ToString();
            auditInstance.ActionRoute = SystemOperationCode.LoanRepayment.ToString();

            var audit = await _iAuditTrailService.SaveForm(auditInstance);
            throw new NotImplementedException();

        }

        #endregion
    }
}