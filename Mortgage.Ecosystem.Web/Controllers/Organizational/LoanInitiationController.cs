using Google.Protobuf.WellKnownTypes;
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
    public class LoanInitiationController : BaseController
    {
        private readonly ILoanInitiationService _iLoanInitiationService;
        private readonly IAuditTrailService _iAuditTrailService;
        public LoanInitiationController(IUnitOfWork iUnitOfWork, ILoanInitiationService iLoanInitiationService, IAuditTrailService AuditTrailService) : base(iUnitOfWork)
        {
            _iLoanInitiationService = iLoanInitiationService;
            _iAuditTrailService = AuditTrailService;
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

        public IActionResult NonMortgageLoanInitiationForm()
        {
            return View();
        }

        public IActionResult NonMortgageLoanInitiationIndex()
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
            var auditInstance = new AuditTrailEntity();
            auditInstance.Action = SystemOperationCode.GetLoanInitiationPageListJson.ToString();
            auditInstance.ActionRoute = SystemOperationCode.LoanInitiationController.ToString();

            var audit = await _iAuditTrailService.SaveForm(auditInstance);
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
            var auditInstance = new AuditTrailEntity();
            auditInstance.Action = SystemOperationCode.GetLoanInitiationPageListJson.ToString();
            auditInstance.ActionRoute = SystemOperationCode.LoanInitiationController.ToString();

            var audit = await _iAuditTrailService.SaveForm(auditInstance);
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
        //[AuthorizeFilter("loanInitiation:view")]
        public async Task<IActionResult> GetFormJson()
        {
            TData<LoanInitiationEntity> obj = await _iLoanInitiationService.GetEntity();
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
            //var auditInstance = new AuditTrailEntity();
            //auditInstance.Action = SystemOperationCode.LoanInitiation.ToString();
            //auditInstance.ActionRoute = SystemOperationCode.LoanInitiationController.ToString();

            //var audit = await _iAuditTrailService.SaveForm(auditInstance);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("loaninitiation:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await _iLoanInitiationService.DeleteForm(ids);
            return Json(obj);
        }

        [HttpPost]
        public async Task<JsonResult> PerformLoanAffordability(InitiateLoanDto initiateLoanDto)
        {
            List<AffordabilityResponseDto> result = new List<AffordabilityResponseDto>();
            var obj = await _iLoanInitiationService.Performaffordability(initiateLoanDto);
            var auditInstance = new AuditTrailEntity();
            auditInstance.Action = SystemOperationCode.PerformLoanAffordability.ToString();
            auditInstance.ActionRoute = SystemOperationCode.LoanInitiationController.ToString();

            var audit = await _iAuditTrailService.SaveForm(auditInstance);
            if (obj.message != null)
            {
                result.Add(obj);
                return Json(new { success = false, message = obj.message });

            }
            result.Add(obj);
            return Json(new { success = true, message = result });
        }
        #endregion
        #region Loan Initiation
        [HttpPost]
        public async Task<IActionResult> LoanInitiation( InitiateLoanDto initiateLoanDto)
        {
            
            TData obj = await _iLoanInitiationService.LoanApplication(initiateLoanDto);
            var auditInstance = new AuditTrailEntity();
            auditInstance.Action = SystemOperationCode.LoanInitiation.ToString();
            auditInstance.ActionRoute = SystemOperationCode.LoanInitiationController.ToString();

            var audit = await _iAuditTrailService.SaveForm(auditInstance);

            var auditInstance2 = new AuditTrailEntity();
            auditInstance2.Action = SystemOperationCode.LoanDocument.ToString();
            auditInstance2.ActionRoute = SystemOperationCode.LoanInitiationController.ToString();

            var audit2 = await _iAuditTrailService.SaveForm(auditInstance2);
            return Json(obj);
        }



        public async Task<IActionResult> NonMortgageLoanInitiation(InitiateLoanDto initiateLoanDto)
        {

            TData obj = await _iLoanInitiationService.LoanApplication(initiateLoanDto);
           

           
            return Json(obj);
        }


        [HttpGet]
        public async Task<IActionResult> ViewInformation()
        {
            var auditInstance = new AuditTrailEntity();
            auditInstance.Action = SystemOperationCode.ViewInformation.ToString();
            auditInstance.ActionRoute = SystemOperationCode.LoanInitiationController.ToString();

            var audit = await _iAuditTrailService.SaveForm(auditInstance);
            TData<CustomerDetailsViewModel> obj = await _iLoanInitiationService.GetCustomerDetails();
            return Json(obj);
        }
        #endregion
    }
}