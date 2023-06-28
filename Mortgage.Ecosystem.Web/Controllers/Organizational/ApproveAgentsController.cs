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
    public class ApproveAgentsController : BaseController
    {
        private readonly IApproveAgentsService _iApproveAgentsService;

        public ApproveAgentsController(IUnitOfWork iUnitOfWork, IApproveAgentsService iApproveAgentsService) : base(iUnitOfWork)
        {
            _iApproveAgentsService = iApproveAgentsService;
        }

        #region View function
        [AuthorizeFilter("approveagents:view")]
        public IActionResult ApproveAgentsIndex()
        {
            return View();
        }

        public IActionResult ApproveAgentsForm()
        {
            return View();
        }


        #endregion

        #region Get data
        [HttpGet]
        [AuthorizeFilter("approveagents:search,user:search")]
        public async Task<IActionResult> GetListJson(ApproveAgentsListParam param)
        {
            TData<List<ApproveAgentsEntity>> obj = await _iApproveAgentsService.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("approveagents:search,user:search")]
        public async Task<IActionResult> GetDevelopersPageListJson(ApproveAgentsListParam param, Pagination pagination)
        {
            TData<List<ApproveAgentsEntity>> obj = await _iApproveAgentsService.GetPageList(param, pagination);
            return Json(obj);
        }


        [HttpGet]
        [AuthorizeFilter("paymenthistory:search,user:search")]
        public async Task<IActionResult> GetPmbPageListJson(ApproveAgentsListParam param, Pagination pagination)
        {
            TData<List<ApproveAgentsEntity>> obj = await _iApproveAgentsService.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("paymenthistory:search,user:search")]
        public async Task<IActionResult> GetCooperativePageListJson(ApproveAgentsListParam param, Pagination pagination)
        {
            TData<List<ApproveAgentsEntity>> obj = await _iApproveAgentsService.GetPageList(param, pagination);
            return Json(obj);
        }


        [HttpGet]
        [AuthorizeFilter("approveagents:search,user:search")]
        public async Task<IActionResult> GetApproveAgentsTreeListJson(ApproveAgentsListParam param)
        {
            TData<List<ZtreeInfo>> obj = await _iApproveAgentsService.GetZtreeApproveAgentsList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("approveagents:view")]
        public async Task<IActionResult> GetUserTreeListJson(ApproveAgentsListParam param)
        {
            TData<List<ZtreeInfo>> obj = await _iApproveAgentsService.GetZtreeUserList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("paymenthistory:view")]
        public async Task<IActionResult> GetFormJson(int id)
        {
            TData<ApproveAgentsEntity> obj = await _iApproveAgentsService.GetEntity(id);
            return Json(obj);
        }

        [HttpGet]
        public async Task<IActionResult> GetMaxSortJson()
        {
            TData<int> obj = await _iApproveAgentsService.GetMaxSort();
            return Json(obj);
        }
        #endregion

        #region Submit data
        [HttpPost]
        [AuthorizeFilter("approveagents:add,approveagents:edit")]
        public async Task<IActionResult> SaveFormJson(ApproveAgentsEntity entity)
        {
            TData<string> obj = await _iApproveAgentsService.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("approveagents:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await _iApproveAgentsService.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}