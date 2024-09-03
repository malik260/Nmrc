using Microsoft.AspNetCore.Mvc;
using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Enums;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;
using Mortgage.Ecosystem.Web.Filter;

namespace Mortgage.Ecosystem.Web.Controllers.Systems
{
    public class ContributionFrequencyController : BaseController
    {
        private readonly IContributionFrequencyService _iContributionFrequencyService;
        private readonly IAuditTrailService _iAuditTrailService;

        public ContributionFrequencyController(IUnitOfWork iUnitOfWork, IContributionFrequencyService iContributionFrequencyService, IAuditTrailService iAuditTrailService) : base(iUnitOfWork)
        {
            _iContributionFrequencyService = iContributionFrequencyService;
            _iAuditTrailService = iAuditTrailService;
        }

        #region View function
        [AuthorizeFilter("contributionfrequency:view")]
        public IActionResult ContributionFrequencyIndex()
        {
            return View();
        }

        [AuthorizeFilter("contributionfrequency:view")]
        public IActionResult ContributionFrequencyForm()
        {
            return View();
        }
        #endregion

        #region Get data
        [HttpGet]
        //[AuthorizeFilter("contributionfrequency:search,user:search")]
        public async Task<IActionResult> GetListJson(ContributionFrequencyListParam param)
        {
            TData<List<ContributionFrequencyEntity>> obj = await _iContributionFrequencyService.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("contributionfrequency:search,user:search")]
        public async Task<IActionResult> GetPageListJson(ContributionFrequencyListParam param, Pagination pagination)
        {
            TData<List<ContributionFrequencyEntity>> obj = await _iContributionFrequencyService.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("contributionfrequency:view")]
        public async Task<IActionResult> GetFormJson(long id)
        {
            TData<ContributionFrequencyEntity> obj = await _iContributionFrequencyService.GetEntity(id);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("contributionfrequency:view")]
        public async Task<IActionResult> GetContributionFrequencyName(ContributionFrequencyListParam param)
        {
            TData<string> obj = new TData<string>();
            var list = await _iContributionFrequencyService.GetList(param);
            if (list.Tag == 1)
            {
                obj.Data = string.Join(",", list.Data.Select(p => p.Name));
                obj.Tag = 1;
            }

            var auditInstance = new AuditTrailEntity();
            auditInstance.Action = SystemOperationCode.GetContributionFrequencyName.ToString();
            auditInstance.ActionRoute = SystemOperationCode.ContributionFrequency.ToString();

            var audit = await _iAuditTrailService.SaveForm(auditInstance);

            return Json(obj);
        }
        #endregion

        #region Submit data
        [HttpPost]
        [AuthorizeFilter("contributionfrequency:add,contributionfrequency:edit")]
        public async Task<IActionResult> SaveFormJson(ContributionFrequencyEntity entity)
        {
            TData<string> obj = await _iContributionFrequencyService.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("contributionfrequency:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await _iContributionFrequencyService.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}