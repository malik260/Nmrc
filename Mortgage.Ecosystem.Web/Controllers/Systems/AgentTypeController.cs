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
    public class AgentTypeController : BaseController
    {
        private readonly IAgentTypeService _iAgentTypeService;
        private readonly IAuditTrailService _iAuditTrailService;
        public AgentTypeController(IUnitOfWork iUnitOfWork, IAgentTypeService iAgentTypeService, IAuditTrailService iAuditTrailService) : base(iUnitOfWork)
        {
            _iAgentTypeService = iAgentTypeService;
            _iAuditTrailService = iAuditTrailService;
        }

        #region View function
        [AuthorizeFilter("agenttype:view")]
        public IActionResult AgentTypeIndex()
        {
            return View();
        }

        [AuthorizeFilter("agenttype:view")]
        public IActionResult AgentTypeForm()
        {
            return View();
        }
        #endregion

        #region Get data
        [HttpGet]
        //[AuthorizeFilter("agenttype:search,user:search")]
        public async Task<IActionResult> GetListJson(AgentTypeListParam param)
        {
            TData<List<AgentTypeEntity>> obj = await _iAgentTypeService.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("agenttype:search,user:search")]
        public async Task<IActionResult> GetPageListJson(AgentTypeListParam param, Pagination pagination)
        {
            TData<List<AgentTypeEntity>> obj = await _iAgentTypeService.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("agenttype:view")]
        public async Task<IActionResult> GetFormJson(long id)
        {
            TData<AgentTypeEntity> obj = await _iAgentTypeService.GetEntity(id);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("agenttype:view")]
        public async Task<IActionResult> GetAgentTypeName(AgentTypeListParam param)
        {
            TData<string> obj = new TData<string>();
            var list = await _iAgentTypeService.GetList(param);
            if (list.Tag == 1)
            {
                obj.Data = string.Join(",", list.Data.Select(p => p.Name));
                obj.Tag = 1;
            }
            var auditInstance = new AuditTrailEntity();
            auditInstance.Action = SystemOperationCode.GetAgentTypeName.ToString();
            auditInstance.ActionRoute = SystemOperationCode.AgentType.ToString();

            var audit = await _iAuditTrailService.SaveForm(auditInstance);
            return Json(obj);
        }
        #endregion

        #region Submit data
        [HttpPost]
        [AuthorizeFilter("agenttype:add,agenttype:edit")]
        public async Task<IActionResult> SaveFormJson(AgentTypeEntity entity)
        {
            TData<string> obj = await _iAgentTypeService.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("agenttype:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await _iAgentTypeService.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}