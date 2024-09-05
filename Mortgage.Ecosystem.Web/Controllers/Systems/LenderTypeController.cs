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
    public class LenderTypeController : BaseController
    {
        private readonly ILenderTypeService _iLenderTypeService;
        private readonly IAuditTrailService _iAuditTrailService;

        public LenderTypeController(IUnitOfWork iUnitOfWork, ILenderTypeService iLenderTypeService, IAuditTrailService iAuditTrailService) : base(iUnitOfWork)
        {
            _iLenderTypeService = iLenderTypeService;
            _iAuditTrailService = iAuditTrailService;   
        }

        #region View function
        [AuthorizeFilter("lendertype:view")]
        public IActionResult LenderTypeIndex()
        {
            return View();
        }

        [AuthorizeFilter("lendertype:view")]
        public IActionResult LenderTypeForm()
        {
            return View();
        }
        #endregion

    
        [HttpGet]
        //[AuthorizeFilter("state:search,user:search")]
        public async Task<IActionResult> GetListJson(LenderTypeListParam param)
        {
            TData<List<LenderTypeEntity>> obj = await _iLenderTypeService.GetList(param);
            return Json(obj);
        }


    }
}