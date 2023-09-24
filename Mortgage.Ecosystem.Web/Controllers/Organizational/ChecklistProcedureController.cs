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
    public class ChecklistProcedureController : BaseController
    {
        private readonly IChecklistProcedureService _iChecklistProcedureService;

        public ChecklistProcedureController(IUnitOfWork iUnitOfWork, IChecklistProcedureService iChecklistProcedureService) : base(iUnitOfWork)
        {
            _iChecklistProcedureService = iChecklistProcedureService;
        }



        #region Submit data
        [HttpPost]

        public async Task<IActionResult> SaveFormJson(List<CheckListVM> selectedData)
        {
            TData<ChecklistProcedureEntity> obj = await _iChecklistProcedureService.SaveForm(selectedData);
            return Json(obj);
        }




        [HttpPost]
        [AuthorizeFilter("checklist:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await _iChecklistProcedureService.DeleteForm(ids);
            return Json(obj);
        }


        #endregion
    }
}
