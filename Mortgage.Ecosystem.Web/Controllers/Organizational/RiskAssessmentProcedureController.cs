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
    [ExceptionFilter]
    public class RiskAssessmentProcedureController : BaseController
    {
        private readonly IRiskAssessmentProcedureService _iRiskAssementProcedureService;

        public RiskAssessmentProcedureController(IUnitOfWork iUnitOfWork, IRiskAssessmentProcedureService iRiskAssessmentProcedureService) : base(iUnitOfWork)
        {
            _iRiskAssementProcedureService = iRiskAssessmentProcedureService;
        }



        #region Submit data
        [HttpPost]

        public async Task<IActionResult> SaveFormJson(RiskAssessmentProcedureDTO selectedData)
        {
            try
            {
                TData obj = await _iRiskAssementProcedureService.SaveForm(selectedData);
                return Json(obj);
            }
            catch (Exception e)
            {

                throw;
            }
        }





        [HttpPost]
        [AuthorizeFilter("checklist:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await _iRiskAssementProcedureService.DeleteForm(ids);
            return Json(obj);
        }


        #endregion
    }
}
