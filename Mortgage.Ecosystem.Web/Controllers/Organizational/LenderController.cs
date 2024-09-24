using Microsoft.AspNetCore.Mvc;
using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.BusinessLogic.Layer.Services;
using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
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
    public class LenderController : BaseController
    {
        private readonly ILenderService _iLenderService;
        private readonly IAuditTrailService _iAuditTrailService;
        public LenderController(IUnitOfWork iUnitOfWork, ILenderService iLenderService, IAuditTrailService iAuditTrailService) : base(iUnitOfWork)
        {
            _iLenderService = iLenderService;
            _iAuditTrailService = iAuditTrailService;
        }

        #region View function
       [AuthorizeFilter("lender:view")]
        public IActionResult LenderIndex()
        {
            return View();
        }

        public IActionResult LenderForm()
        {
            return View();
        }

        public IActionResult LenderEditForm()
        {
            return View();
        }
        #endregion

        #region Get data
        [HttpGet]
        [AuthorizeFilter("lender:search,user:search")]
        public async Task<IActionResult> GetListJson(LenderListParam param)
        {
            TData<List<LenderSetupEntity>> obj = await _iLenderService.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("lender:search,user:search")]
        public async Task<IActionResult> GetLenderPageListJson(LenderListParam param, Pagination pagination)
        {
            TData<List<LenderSetupEntity>> obj = await _iLenderService.GetPageList(param, pagination);
            var auditInstance = new AuditTrailEntity();
            auditInstance.Action = SystemOperationCode.GetLenderPageListJson.ToString();
            auditInstance.ActionRoute = SystemOperationCode.Lender.ToString();

            var audit = await _iAuditTrailService.SaveForm(auditInstance);
            return Json(obj);
        }


        [HttpGet]
        [AuthorizeFilter("lender:search,user:search")]
        public async Task<IActionResult> GetPageListJson(LenderListParam param, Pagination pagination)
        {
            TData<List<LenderSetupEntity>> obj = await _iLenderService.GetPageList(param, pagination);
            return Json(obj);
        }


        [HttpGet]
        [AuthorizeFilter("lender:view")]
        public async Task<IActionResult> GetFormJson(int id)
        {
            TData<LenderSetupEntity> obj = await _iLenderService.GetEntity(id);
            return Json(obj);
        }

        [HttpGet]
        //[AuthorizeFilter("propertyregistration:view")]
        public async Task<IActionResult> GetFormJsonn(int id)
        {
            TData<LenderSetupEntity> obj = await _iLenderService.GetEntities(id)
;
            return Json(obj);
        }

        #endregion

        #region Submit data
        [HttpPost]
        [AuthorizeFilter("lender:add,company:edit")]
        public async Task<IActionResult> SaveFormJson(string Lenders, int LenderTypeId)
        {
            var entity = new LenderSetupEntity();
            entity.LenderTypeId = LenderTypeId;
            
            var lender = Lenders.Split(",").Select(int.Parse).ToList();
            entity.Lender = lender;
            TData<string> obj = await _iLenderService.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("lender:add,employee:edit")]
        public async Task<IActionResult> UpdateFormJson(LenderSetupEntity entity)
        {
            TData<string> obj = await _iLenderService.UpdateForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("lender:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await _iLenderService.DeleteForm(ids);
            return Json(obj);
        }


        #endregion
    }
}
