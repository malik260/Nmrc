using Microsoft.AspNetCore.Mvc;
using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.BusinessLogic.Layer.Services;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.Web.Filter;

namespace Mortgage.Ecosystem.Web.Controllers.Organizational
{
    public class SchemeLenderController : BaseController
    {
        private readonly ISchemeLenderService _iSchemeLenderService;
        public SchemeLenderController(IUnitOfWork iUnitOfWork, ISchemeLenderService iSchemeService) : base(iUnitOfWork)
        {
            _iSchemeLenderService = iSchemeService;
        }


        #region View function
        [AuthorizeFilter("schemelender:view")]
        public IActionResult SchemeLenderIndex()
        {
            return View();
        }

        public IActionResult SchemeLenderForm()
        {
            return View();
        }
        #endregion


        #region Submit data
        [HttpPost]
        [AuthorizeFilter("scheme:add,company:edit")]
        public async Task<IActionResult> SaveFormJson(string Lenders, int SchemeId)
        {
            var schemelender = new SchemeLenderEntity();
            schemelender.SchemeId = SchemeId;
            var lender = Lenders.Split(",").Select(int.Parse).ToList();
            schemelender.Lenders = lender;
            TData<string> obj = await _iSchemeLenderService.SaveForm(schemelender);
            return Json(obj);
        }

        public async Task<IActionResult> GetListJson(SchemeLenderListParam param)
        {
            TData<List<SchemeLenderEntity>> obj = await _iSchemeLenderService.GetList(param);
            return Json(obj);
        }




        #endregion
    }
}
