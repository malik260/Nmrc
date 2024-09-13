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
    public class SecondaryLenderChecklistProcedureController : BaseController
    {
        private readonly ISecondaryLenderChecklistProcedureService _iSecondaryLenderChecklistProcedureService;

        public SecondaryLenderChecklistProcedureController(IUnitOfWork iUnitOfWork, ISecondaryLenderChecklistProcedureService iSecondaryLenderChecklistProcedureService) : base(iUnitOfWork)
        {
            _iSecondaryLenderChecklistProcedureService = iSecondaryLenderChecklistProcedureService;
        }



        #region Submit data
        [HttpPost]

        public async Task<IActionResult> SaveFormJson(List<SecondaryLenderCheckListVM> selectedData)
        {
            TData<SecondaryLenderChecklistProcedureEntity> obj = await _iSecondaryLenderChecklistProcedureService.SaveForm(selectedData);
            return Json(obj);
        }




        [HttpPost]
        [AuthorizeFilter("secondarylenderchecklist:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await _iSecondaryLenderChecklistProcedureService.DeleteForm(ids);
            return Json(obj);
        }


        #endregion
    }
}
