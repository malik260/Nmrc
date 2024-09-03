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
    public class ETicketController : BaseController
    {
        private readonly IETicketService _iETicketService;
        private readonly IAuditTrailService _iAuditTrailService;
        public ETicketController(IUnitOfWork iUnitOfWork, IETicketService iETicketService, IAuditTrailService iAuditTrailService) : base(iUnitOfWork)
        {
            _iETicketService = iETicketService;
            _iAuditTrailService = iAuditTrailService;
        }

        #region View function
        // [AuthorizeFilter("eticket:view")]
        public IActionResult ETicketIndex()
        {
            return View();
        }

        public IActionResult ETicketForm()
        {
            return View();
        }

        [AuthorizeFilter("approveeticket:view")]
        public IActionResult ApproveETicketIndex()
        {
            return View();
        }

        public IActionResult ApproveETicketForm()
        {
            return View();
        }

        [AuthorizeFilter("admineticket:view")]

        public IActionResult AdminETicketIndex()
        {
            return View();
        }


        public IActionResult EticketDetails()
        {
            return View();
        }



        #endregion

        #region Get data
        [HttpGet]
        // [AuthorizeFilter("eticket:search,user:search")]
        public async Task<IActionResult> GetListJson(ETicketListParam param)
        {
            TData<List<ETicketEntity>> obj = await _iETicketService.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        // [AuthorizeFilter("eticket:search,user:search")]
        public async Task<IActionResult> GetEticketPageListJson(ETicketListParam param, Pagination pagination)
        {
            TData<List<ETicketEntity>> obj = await _iETicketService.GetPageList(param, pagination);
            var auditInstance = new AuditTrailEntity();
            auditInstance.Action = SystemOperationCode.GetEticketPageListJson.ToString();
            auditInstance.ActionRoute = SystemOperationCode.ETicket.ToString();

            var audit = await _iAuditTrailService.SaveForm(auditInstance);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("customerprofileupdate:search,user:search")]
        public async Task<IActionResult> GetEmployeeEticketPageListJson(ETicketListParam param, Pagination pagination)
        {
            TData<List<ETicketEntity>> obj = await _iETicketService.GetEmployeePageList(param, pagination);
            var auditInstance = new AuditTrailEntity();
            auditInstance.Action = SystemOperationCode.GetEticketPageListJson.ToString();
            auditInstance.ActionRoute = SystemOperationCode.ETicket.ToString();

            var audit = await _iAuditTrailService.SaveForm(auditInstance);
            return Json(obj);
        }

        [HttpGet]
        //  [AuthorizeFilter("eticket:search,user:search")]
        public async Task<IActionResult> GetApprovalPageListJson(ETicketListParam param, Pagination pagination)
        {
            TData<List<ETicketEntity>> obj = await _iETicketService.GetApprovalPageList(param, pagination);
            return Json(obj);
        }


        [HttpGet]
        // [AuthorizeFilter("eticket:search,user:search")]
        public async Task<IActionResult> GetETicketTreeListJson(ETicketListParam param)
        {
            TData<List<ZtreeInfo>> obj = await _iETicketService.GetZtreeETicketList(param);
            return Json(obj);
        }

        [HttpGet]
        //   [AuthorizeFilter("eticket:view")]
        public async Task<IActionResult> GetFormJson(long id)
        {
            TData<ETicketEntity> obj = await _iETicketService.GetEntity(id);
            return Json(obj);
        }


        [HttpGet]
        //  [AuthorizeFilter("eticket:view")]
        public async Task<IActionResult> GetUserTreeListJson(ETicketListParam param)
        {
            TData<List<ZtreeInfo>> obj = await _iETicketService.GetZtreeUserList(param);
            return Json(obj);
        }

        //[HttpGet]
        //[AuthorizeFilter("eticket:view")]
        //public async Task<IActionResult> GetFormJson(int id)
        //{
        //    TData<ETicketEntity> obj = await _iETicketService.GetEntity(id);
        //    return Json(obj);
        //}

        [HttpGet]
        public async Task<IActionResult> GetMaxSortJson()
        {
            TData<int> obj = await _iETicketService.GetMaxSort();
            return Json(obj);
        }

        [HttpGet]
        public async Task<IActionResult> ViewStatus(long id)
        {
            TData<CustomerDetailsViewModel> obj = await _iETicketService.GetStatus(id);
            return Json(obj);
        }
        #endregion

        #region Submit data
        //[HttpPost]
        //[AuthorizeFilter("eticket:add,eticket:edit")]
        //public async Task<IActionResult> SaveFormJson(ETicketEntity entity)
        //{
        //    TData<string> obj = await _iETicketService.SaveForm(entity);
        //    return Json(obj);
        //}

        [HttpPost]
        // [AuthorizeFilter("eticket:add,eticket:edit")]
        public async Task<IActionResult> SaveFormDetails(ETicketEntity entity)
        {
            TData obj = await _iETicketService.SaveForms(entity);
            return Json(obj);
        }

        [HttpPost]
        //   [AuthorizeFilter("eticket:add,eticket:edit")]
        public async Task<IActionResult> SaveFormJson(ETicketEntity entity)
        {
            TData<string> obj = new TData<string>();

            // Generate a random five-digit number
            Random random = new Random();
            int randomNumber = random.Next(10000, 99999);

            // Set the RequestNumber with the generated random number
            entity.RequestNumber = randomNumber.ToString();
            obj = await _iETicketService.SaveForm(entity);
            return Json(obj);

        }

        [HttpPost]
        //   [AuthorizeFilter("eticket:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await _iETicketService.DeleteForm(ids);
            return Json(obj);
        }

        [HttpPost]
        //   [AuthorizeFilter("eticket:add,eticket:edit")]
        public async Task<IActionResult> ApproveETicketForm(ETicketEntity entity)
        {
            TData<string> obj = await _iETicketService.ApproveForm(entity);
            var auditInstance = new AuditTrailEntity();
            auditInstance.Action = SystemOperationCode.ApproveETicketForm.ToString();
            auditInstance.ActionRoute = SystemOperationCode.ETicket.ToString();

            var audit = await _iAuditTrailService.SaveForm(auditInstance);
            return Json(obj);
        }

        #endregion
    }
}