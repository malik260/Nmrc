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
    public class InternetBankingUsersController : BaseController
    {
        private readonly IInternetBankingUsersService _iInternetBankingUsersService;
        private readonly IAuditTrailService _iAuditTrailService;
        public InternetBankingUsersController(IUnitOfWork iUnitOfWork, IInternetBankingUsersService iInternetBankingUsersService, IAuditTrailService iAuditTrailService) : base(iUnitOfWork)
        {
            _iInternetBankingUsersService = iInternetBankingUsersService;
            _iAuditTrailService = iAuditTrailService;
        }

        #region View function
        [AuthorizeFilter("internetbankingusers:view")]
        public IActionResult InternetBankingUsersIndex()
        {
            return View();
        }

        public IActionResult InternetBankingUsersForm()
        {
            return View();
        }

       
        #endregion

        #region Get data
        [HttpGet]
        [AuthorizeFilter("internetbankingusers:search,user:search")]
        public async Task<IActionResult> GetListJson(InternetBankingUsersListParam param)
        {
            TData<List<InternetBankingUsersEntity>> obj = await _iInternetBankingUsersService.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("internetbankingusers:search,user:search")]
        public async Task<IActionResult> GetInternetBankingUsersPageListJson(InternetBankingUsersListParam param, Pagination pagination)
        {
            TData<List<InternetBankingUsersEntity>> obj = await _iInternetBankingUsersService.GetPageList(param, pagination);
            var auditInstance = new AuditTrailEntity();
            auditInstance.Action = SystemOperationCode.GetInternetBankingUsersPageListJson.ToString();
            auditInstance.ActionRoute = SystemOperationCode.InternetBanking.ToString();

            var audit = await _iAuditTrailService.SaveForm(auditInstance);
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
        [AuthorizeFilter("internetbankingusers:search,user:search")]
        public async Task<IActionResult> GetInternetBankingUsersTreeListJson(InternetBankingUsersListParam param)
        {
            TData<List<ZtreeInfo>> obj = await _iInternetBankingUsersService.GetZtreeInternetBankingUsersList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("eticket:view")]
        public async Task<IActionResult> GetUserTreeListJson(InternetBankingUsersListParam param)
        {
            TData<List<ZtreeInfo>> obj = await _iInternetBankingUsersService.GetZtreeUserList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("internetbankingusers:view")]
        public async Task<IActionResult> GetFormJson(int id)
        {
            TData<InternetBankingUsersEntity> obj = await _iInternetBankingUsersService.GetEntity(id);
            return Json(obj);
        }

        [HttpGet]
        public async Task<IActionResult> GetMaxSortJson()
        {
            TData<int> obj = await _iInternetBankingUsersService.GetMaxSort();
            return Json(obj);
        }
        #endregion

        #region Submit data
        [HttpPost]
        [AuthorizeFilter("internetbankingusers:add,internetbankingusers:edit")]
        public async Task<IActionResult> SaveFormJson(InternetBankingUsersEntity entity)
        {
            TData<string> obj = await _iInternetBankingUsersService.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("internetbankingusers:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await _iInternetBankingUsersService.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}