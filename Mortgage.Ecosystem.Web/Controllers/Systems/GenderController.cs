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
    public class GenderController : BaseController
    {
        private readonly IGenderService _iGenderService;
        private readonly IAuditTrailService _iAuditTrailService;

        public GenderController(IUnitOfWork iUnitOfWork, IGenderService iGenderService, IAuditTrailService iAuditTrailService) : base(iUnitOfWork)
        {
            _iGenderService = iGenderService;
            _iAuditTrailService = iAuditTrailService;
        }

        #region View function
        [AuthorizeFilter("gender:view")]
        public IActionResult GenderIndex()
        {
            return View();
        }

        [AuthorizeFilter("gender:view")]
        public IActionResult GenderForm()
        {
            return View();
        }
        #endregion

        #region Get data
        [HttpGet]
        //[AuthorizeFilter("gender:search,user:search")]
        public async Task<IActionResult> GetListJson(GenderListParam param)
        {
            TData<List<GenderEntity>> obj = await _iGenderService.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("gender:search,user:search")]
        public async Task<IActionResult> GetPageListJson(GenderListParam param, Pagination pagination)
        {
            TData<List<GenderEntity>> obj = await _iGenderService.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("gender:view")]
        public async Task<IActionResult> GetFormJson(int id)
        {
            TData<GenderEntity> obj = await _iGenderService.GetEntity(id);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("gender:view")]
        public async Task<IActionResult> GetGenderName(GenderListParam param)
        {
            TData<string> obj = new TData<string>();
            var list = await _iGenderService.GetList(param);
            if (list.Tag == 1)
            {
                obj.Data = string.Join(",", list.Data.Select(p => p.Name));
                obj.Tag = 1;
            }

            var auditInstance = new AuditTrailEntity();
            auditInstance.Action = SystemOperationCode.GetGenderName.ToString();
            auditInstance.ActionRoute = SystemOperationCode.Gender.ToString();

            var audit = await _iAuditTrailService.SaveForm(auditInstance);
            return Json(obj);
        }
        #endregion

        #region Submit data
        [HttpPost]
        [AuthorizeFilter("gender:add,gender:edit")]
        public async Task<IActionResult> SaveFormJson(GenderEntity entity)
        {
            TData<string> obj = await _iGenderService.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("gender:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await _iGenderService.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}