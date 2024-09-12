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
    public class SecondaryLenderChecklistController : BaseController
    {
        private readonly ISecondaryLenderChecklistService _iSecondaryLenderChecklistService;

        public SecondaryLenderChecklistController(IUnitOfWork iUnitOfWork, ISecondaryLenderChecklistService iSecondaryLenderChecklistService) : base(iUnitOfWork)
        {
            _iSecondaryLenderChecklistService = iSecondaryLenderChecklistService;
        }



        #region Submit data
        [HttpPost]

        public async Task<IActionResult> SaveFormJson(List<SecondaryLenderChecklisVM> selectedData)
        {
            TData<SecondaryLenderChecklistEntity> obj = await _iSecondaryLenderChecklistService.SaveForm(selectedData);
            return Json(obj);
        }




        [HttpPost]
        [AuthorizeFilter("checklist:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await _iSecondaryLenderChecklistService.DeleteForm(ids);
            return Json(obj);
        }


        #endregion
    }
}
