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
    public class ContributionHistoryController : BaseController
    {
        private readonly IContributionHistoryService _iContributionHistoryService;
        private readonly IAuditTrailService _iAuditTrailService;
        public ContributionHistoryController(IUnitOfWork iUnitOfWork, IContributionHistoryService iContributionHistoryService, IAuditTrailService iAuditTrailService) : base(iUnitOfWork)
        {
            _iContributionHistoryService = iContributionHistoryService;
            _iAuditTrailService = iAuditTrailService;
        }

        #region View function
        //[AuthorizeFilter("contributionhistory:view")]
        public IActionResult ContributionHistoryIndex()
        {
            return View();
        }

        public IActionResult ContributionHistoryForm()
        {
            return View();
        }

        public IActionResult EmployerContributionHistory()
        {
            return View();
        }

        #endregion

        #region Get data
        [HttpGet]
        [AuthorizeFilter("contributionhistory:search,user:search")]
        public async Task<IActionResult> GetListJson(ContributionHistoryListParam param)
        {
            TData<List<ContributionHistoryEntity>> obj = await _iContributionHistoryService.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("contributionhistory:search,user:search")]
        public async Task<IActionResult> GetContributionHistoryPageListJson(ContributionHistoryListParam param, Pagination pagination)
        {
            var auditInstance = new AuditTrailEntity();
            auditInstance.Action = SystemOperationCode.GetContributionHistoryPageListJson.ToString();
            auditInstance.ActionRoute = SystemOperationCode.ContributionHistory.ToString();

            var audit = await _iAuditTrailService.SaveForm(auditInstance);
            TData<List<ContributionHistoryEntity>> obj = await _iContributionHistoryService.GetPageList(param, pagination);
            return Json(obj);
        }





        [HttpGet]
        [AuthorizeFilter("contributionhistory:search,user:search")]
        public async Task<IActionResult> GetContributionHistoryTreeListJson(ContributionHistoryListParam param)
        {
            TData<List<ZtreeInfo>> obj = await _iContributionHistoryService.GetZtreeContributionHistoryList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("contributionhistory:view")]
        public async Task<IActionResult> GetUserTreeListJson(ContributionHistoryListParam param)
        {
            TData<List<ZtreeInfo>> obj = await _iContributionHistoryService.GetZtreeUserList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("contributionhistory:view")]
        public async Task<IActionResult> GetFormJson(int id)
        {
            TData<ContributionHistoryEntity> obj = await _iContributionHistoryService.GetEntity(id);
            return Json(obj);
        }

        [HttpGet]
        public async Task<IActionResult> GetMaxSortJson()
        {
            TData<int> obj = await _iContributionHistoryService.GetMaxSort();
            return Json(obj);
        }
        #endregion

        #region Submit data
        [HttpPost]
        [AuthorizeFilter("contributionhistory:add,contributionhistory:edit")]
        public async Task<IActionResult> SaveFormJson(ContributionHistoryEntity entity)
        {
            TData<string> obj = await _iContributionHistoryService.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("contributionhistory:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await _iContributionHistoryService.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}
