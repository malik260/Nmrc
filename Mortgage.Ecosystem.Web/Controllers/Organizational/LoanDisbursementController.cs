using Microsoft.AspNetCore.Mvc;
using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;
using Mortgage.Ecosystem.Web.Filter;

namespace Mortgage.Ecosystem.Web.Controllers.Organizational
{
    public class LoanDisbursementController : BaseController
    {
        private readonly ILoanDisbursementService _loanDisbursementService;

        public LoanDisbursementController(IUnitOfWork iUnitOfWork, ILoanDisbursementService iloanDisbursementService) : base(iUnitOfWork)
        {
            _loanDisbursementService = iloanDisbursementService;
        }

        #region Get data
        [HttpGet]
        //[AuthorizeFilter("loandisbursement:search")]
        public async Task<IActionResult> GetListJson(LoanDisbursementDto param)
        {
            TData<List<LoanDisbursementEntity>> obj = await _loanDisbursementService.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        //[AuthorizeFilter("loandisbursement:search,user:search")]
        public async Task<IActionResult> GetLoanDisbursementPageListJson(LoanDisbursementDto param, Pagination pagination)
        {
            try
            {
                TData<List<LoanDisbursementEntity>> obj = await _loanDisbursementService.GetPageList(param, pagination);

                return Json(obj);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        #endregion

    }
}
