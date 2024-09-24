using Microsoft.AspNetCore.Mvc;
using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.BusinessLogic.Layer.Services;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.Web.Filter;

namespace Mortgage.Ecosystem.Web.Controllers.Organizational
{
    public class LenderCategoryController : BaseController
    {
        private readonly ILenderCategoryService _lenderCategoryService;
        public LenderCategoryController(IUnitOfWork iUnitOfWork, ILenderCategoryService ilenderCategoryService) : base(iUnitOfWork)
        {
            _lenderCategoryService = ilenderCategoryService;
        }

        #region Get data
        [HttpGet]
        //[AuthorizeFilter("lender:search,user:search")]
        public async Task<IActionResult> GetListJson(LenderCategoryEntity param)
        {
            TData<List<LenderCategoryEntity>> obj = await _lenderCategoryService.GetList(param);
            return Json(obj);
        }
        #endregion

    }
}
