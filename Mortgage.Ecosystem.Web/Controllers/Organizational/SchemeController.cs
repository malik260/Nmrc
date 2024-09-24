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
    public class SchemeController : BaseController
    {
        private readonly ISchemeService _iSchemeService;
        private readonly IAuditTrailService _iAuditTrailService;
        public SchemeController(IUnitOfWork iUnitOfWork, ISchemeService iSchemeService, IAuditTrailService iAuditTrailService) : base(iUnitOfWork)
        {
            _iSchemeService = iSchemeService;
            _iAuditTrailService = iAuditTrailService;
        }

        #region View function
        [AuthorizeFilter("scheme:view")]
        public IActionResult SchemeIndex()
        {
            return View();
        }

        public IActionResult SchemeForm()
        {
            return View();
        }

        public IActionResult SchemeEditForm()
        {
            return View();
        }
        #endregion

        #region Get data
        [HttpGet]
        [AuthorizeFilter("scheme:search,user:search")]
        public async Task<IActionResult> GetListJson(SchemeListParam param)
        {
            TData<List<SchemeSetupEntity>> obj = await _iSchemeService.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("scheme:search,user:search")]
        public async Task<IActionResult> GetSchemePageListJson(SchemeListParam param, Pagination pagination)
        {
            TData<List<SchemeSetupEntity>> obj = await _iSchemeService.GetPageList(param, pagination);
            var auditInstance = new AuditTrailEntity();
            auditInstance.Action = SystemOperationCode.GetSchemePageListJson.ToString();
            auditInstance.ActionRoute = SystemOperationCode.Scheme.ToString();

            var audit = await _iAuditTrailService.SaveForm(auditInstance);
            return Json(obj);
        }



        [HttpGet]
        [AuthorizeFilter("scheme:search,user:search")]
        public async Task<IActionResult> GetPageListJson(SchemeListParam param, Pagination pagination)
        {
            TData<List<SchemeSetupEntity>> obj = await _iSchemeService.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]


        [HttpGet]
        [AuthorizeFilter("scheme:view")]
        public async Task<IActionResult> GetFormJson(string code)
        {
            TData<SchemeSetupEntity> obj = await _iSchemeService.GetEntity(code);
            return Json(obj);
        }

        public async Task<IActionResult> GetFormJsonn(int id)
        {
            TData<SchemeSetupEntity> obj = await _iSchemeService.GetEntities(id)
;
            return Json(obj);
        }

        //[HttpGet]
        //public async Task<IActionResult> GetMaxSortJson()
        //{
        //    TData<int> obj = await _iCreditTypeService.GetMaxSort();
        //    return Json(obj);
        //}
        #endregion

        #region Submit data
        [HttpPost]
        [AuthorizeFilter("scheme:add,company:edit")]
        public async Task<IActionResult> SaveFormJson(SchemeSetupEntity entity)
        {
<<<<<<< HEAD
            var entity = new SchemeSetupEntity();
            entity.SchemeName = inputName;

            entity.LendersId = selectedIds.Split(",").Select(long.Parse).ToList();
=======
            
>>>>>>> 582dc07cedb5466adfe3cfbdf5ed12e5278f65e7
            TData<string> obj = await _iSchemeService.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("scheme:add,employee:edit")]
        public async Task<IActionResult> UpdateFormJson(SchemeSetupEntity entity)
        {
            TData<string> obj = await _iSchemeService.UpdateForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("scheme:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await _iSchemeService.DeleteForm(ids);
            return Json(obj);
        }


        #endregion
    }
}
