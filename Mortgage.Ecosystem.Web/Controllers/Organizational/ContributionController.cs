using Microsoft.AspNetCore.Mvc;
using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.BusinessLogic.Layer.Services;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Operator;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;
using Mortgage.Ecosystem.Web.Filter;

namespace Mortgage.Ecosystem.Web.Controllers.Organizational
{


    public class ContributionController : BaseController
    {
        private readonly IContributionService _iContributionService;
        private readonly IPaymentIntegrationService _ipaymentIntegrationService;

        public ContributionController(IUnitOfWork iUnitOfWork, IContributionService iContributionService, IPaymentIntegrationService paymentIntegrationService) : base(iUnitOfWork)
        {
            _iContributionService = iContributionService;
            _ipaymentIntegrationService = paymentIntegrationService;
        }
        #region View function
        [AuthorizeFilter("contribution:view")]
        public IActionResult ContributionIndex()
        {
            return View();
        }

        public IActionResult ContributionForm()
        {

            return View();
        }
        #endregion




        #region Get data
        [HttpGet]
        [AuthorizeFilter("contribution:search,user:search")]
        public async Task<IActionResult> GetListJson(ContributionParam param)
        {
            TData<List<ContributionEntity>> obj = await _iContributionService.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("contribution:search,user:search")]
        public async Task<IActionResult> GetCompanyTreeListJson(ContributionParam param)
        {
            TData<List<ZtreeInfo>> obj = await _iContributionService.GetZtreeSingleContributionList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("contribution:view")]
        public async Task<IActionResult> GetUserTreeListJson(ContributionParam param)
        {
            TData<List<ZtreeInfo>> obj = await _iContributionService.GetZtreeUserList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("contribution:view")]
        public async Task<IActionResult> GetFormJson(int id)
        {
            TData<ContributionEntity> obj = await _iContributionService.GetEntity(id);
            return Json(obj);
        }

        [HttpGet]
        public async Task<IActionResult> GetMaxSortJson()
        {
            TData<int> obj = await _iContributionService.GetMaxSort();
            return Json(obj);
        }
        #endregion

        #region Submit data
        [HttpPost]
        [AuthorizeFilter("singlecontribution:add,company:edit")]
        public async Task<IActionResult> SaveFormJson(ContributionEntity entity)
        {
            TData<string> obj = await _iContributionService.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("singlecontribution:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await _iContributionService.DeleteForm(ids);
            return Json(obj);
        }
        #endregion

        #region single contribution
        public async Task<IActionResult> SingleContribution(ContributionEntity entity)
        {
            TData obj = await _iContributionService.SingleContribution(entity);
            return Json(obj);
        }
        #endregion

        public async Task<IActionResult> GetEmployeeDetails()
        {
            TData obj = await _iContributionService.GetCustomerDetails();
            return Json(obj);
        }

        #region Check RRR status 
        public async Task<IActionResult> CheckPaymentStatus(string RRR)
        {
            TData obj = await _ipaymentIntegrationService.CheckRRRStatus(RRR);
            return Json(obj);
        }
        #endregion
    }
}