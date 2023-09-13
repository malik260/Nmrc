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
    public class StatementOfAccountController : BaseController
    {
        private readonly IStatementOfAccountService _iStatementOfAccountService;

        public StatementOfAccountController(IUnitOfWork iUnitOfWork, IStatementOfAccountService iStatementOfAccountService) : base(iUnitOfWork)
        {
            _iStatementOfAccountService = iStatementOfAccountService;
        }

        #region View function
        [AuthorizeFilter("statementofaccount:view")]
        public IActionResult StatementOfAccountIndex()
        {
            return View();
        }

        public IActionResult StatementOfAccountForm()
        {
            return View();
        }


        #endregion

        #region Get data
        [HttpGet]
        [AuthorizeFilter("statementofaccount:search,user:search")]
        public async Task<IActionResult> GetListJson(StatementOfAccountListParam param)
        {
            TData<List<FinanceCounterpartyTransactionEntity>> obj = await _iStatementOfAccountService.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("statementofaccount:search,user:search")]
        public async Task<IActionResult> GetStatementOfAccountPageListJson(StatementOfAccountListParam param, Pagination pagination)
        {
            TData<List<FinanceCounterpartyTransactionEntity>> obj = await _iStatementOfAccountService.GetPageList(param, pagination);
            return Json(obj);
        }


        //[HttpGet]
        //[AuthorizeFilter("statementofaccount:search,user:search")]
        //public async Task<IActionResult> GetStatementOfAccountTreeListJson(StatementOfAccountListParam param)
        //{
        //    TData<List<ZtreeInfo>> obj = await _iStatementOfAccountService.GetZtreeStatementOfAccountList(param);
        //    return Json(obj);
        //}

        //[HttpGet]
        //[AuthorizeFilter("riskassessmentsetup:view")]
        //public async Task<IActionResult> GetUserTreeListJson(StatementOfAccountListParam param)
        //{
        //    TData<List<ZtreeInfo>> obj = await _iStatementOfAccountService.GetZtreeUserList(param);
        //    return Json(obj);
        //}

        [HttpGet]
        [AuthorizeFilter("riskassessmentsetup:view")]
        public async Task<IActionResult> GetFormJson(int id)
        {
            TData<StatementOfAccountEntity> obj = await _iStatementOfAccountService.GetEntity(id);
            return Json(obj);
        }

        [HttpGet]
        public async Task<IActionResult> GetMaxSortJson()
        {
            TData<int> obj = await _iStatementOfAccountService.GetMaxSort();
            return Json(obj);
        }
        #endregion

        #region Submit data
        [HttpPost]
        [AuthorizeFilter("riskassessmentsetup:add,riskassessmentsetup:edit")]
        public async Task<IActionResult> SaveFormJson(StatementOfAccountEntity entity)
        {
            TData<string> obj = await _iStatementOfAccountService.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("riskassessmentsetup:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await _iStatementOfAccountService.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}