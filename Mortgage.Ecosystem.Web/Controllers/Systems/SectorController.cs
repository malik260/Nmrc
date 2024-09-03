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
    public class SectorController : BaseController
    {
        private readonly ISectorService _iSectorService;
        private readonly IAuditTrailService _iAuditTrailService;

        public SectorController(IUnitOfWork iUnitOfWork, ISectorService iSectorService, IAuditTrailService iAuditTrailService) : base(iUnitOfWork)
        {
            _iSectorService = iSectorService;
            _iAuditTrailService = iAuditTrailService;
        }

        #region View function
        [AuthorizeFilter("sector:view")]
        public IActionResult SectorIndex()
        {
            return View();
        }

        [AuthorizeFilter("sector:view")]
        public IActionResult SectorForm()
        {
            return View();
        }
        #endregion

        #region Get data
        [HttpGet]
        //[AuthorizeFilter("sector:search,user:search")]
        public async Task<IActionResult> GetListJson(SectorListParam param)
        {
            TData<List<SectorEntity>> obj = await _iSectorService.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("sector:search,user:search")]
        public async Task<IActionResult> GetPageListJson(SectorListParam param, Pagination pagination)
        {
            TData<List<SectorEntity>> obj = await _iSectorService.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("sector:view")]
        public async Task<IActionResult> GetFormJson(long id)
        {
            TData<SectorEntity> obj = await _iSectorService.GetEntity(id);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("sector:view")]
        public async Task<IActionResult> GetSectorName(SectorListParam param)
        {
            TData<string> obj = new TData<string>();
            var list = await _iSectorService.GetList(param);
            if (list.Tag == 1)
            {
                obj.Data = string.Join(",", list.Data.Select(p => p.Name));
                obj.Tag = 1;
            }

            var auditInstance = new AuditTrailEntity();
            auditInstance.Action = SystemOperationCode.GetSectorName.ToString();
            auditInstance.ActionRoute = SystemOperationCode.Sector.ToString();

            var audit = await _iAuditTrailService.SaveForm(auditInstance);
            return Json(obj);
        }
        #endregion

        #region Submit data
        [HttpPost]
        [AuthorizeFilter("sector:add,sector:edit")]
        public async Task<IActionResult> SaveFormJson(SectorEntity entity)
        {
            TData<string> obj = await _iSectorService.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("sector:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await _iSectorService.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}