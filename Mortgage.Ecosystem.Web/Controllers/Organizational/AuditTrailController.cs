using Microsoft.AspNetCore.Mvc;
using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.BusinessLogic.Layer.Services;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;
using Mortgage.Ecosystem.Web.Filter;

namespace Mortgage.Ecosystem.Web.Controllers.Systems
{
    public class AuditTrailController : BaseController
    {
        private readonly IAuditTrailService _iAuditTrailService;

        public AuditTrailController(IUnitOfWork iUnitOfWork, IAuditTrailService iAuditTrailService) : base(iUnitOfWork)
        {
            _iAuditTrailService = iAuditTrailService;
        }

        #region View function
        [AuthorizeFilter("auditTrail:view")]
        public IActionResult AuditTrailIndex()
        {
            return View();
        }

        [AuthorizeFilter("auditTrail:view")]
        public IActionResult AdminAuditTrailIndex()
        {
            return View();
        }
        #endregion

        #region Get data
        [HttpGet]
        [AuthorizeFilter("auditTrail:search")]
        public async Task<IActionResult> GetListJson(AuditTrailListParam param)
        {
            TData<List<AuditTrailEntity>> obj = await _iAuditTrailService.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("auditTrail:search")]
        public async Task<IActionResult> GetAuditTailPage(AuditTrailListParam param, Pagination pagination)
        {
            TData<List<AuditTrailEntity>> obj = await _iAuditTrailService.GetPageList(param, pagination);
            return Json(obj);
        }

        [AuthorizeFilter("auditTrail:search")]
        public async Task<IActionResult> GetAdminAuditTailPage(AuditTrailListParam param, Pagination pagination)
        {
            TData<List<AuditTrailEntity>> obj = await _iAuditTrailService.GetAdminPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("auditTrail:view")]
        public async Task<IActionResult> GetFormJson(long id)
        {
            TData<AuditTrailEntity> obj = await _iAuditTrailService.GetEntity(id);
            return Json(obj);
        }
        #endregion

        #region Submit data
        [HttpPost]
        [AuthorizeFilter("auditTrail:add")]
        public async Task<IActionResult> SaveAuditTrail(AuditTrailEntity entity)
        {
            TData<string> obj = await _iAuditTrailService.SaveForm(entity);
            return Json(obj);
        }


        #endregion
    }
}