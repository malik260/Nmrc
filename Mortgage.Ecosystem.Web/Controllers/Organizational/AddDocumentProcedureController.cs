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
    public class AddDocumentProcedureController : BaseController
    {
        private readonly IAddDocumentProcedureService _iAddDocumentProcedureService;

        public AddDocumentProcedureController(IUnitOfWork iUnitOfWork, IAddDocumentProcedureService iAddDocumentProcedureService) : base(iUnitOfWork)
        {
            _iAddDocumentProcedureService = iAddDocumentProcedureService;
        }



        #region Submit data
        [HttpPost]

        public async Task<IActionResult> SaveFormJson(IFormFile imageFile, string DocumentTitle, string TextEditor, string Comment, string NhfNumber, string ProductCode, string LoanId)
        {
            TData<AddDocumentProcedureEntity> obj = await _iAddDocumentProcedureService.SaveForm(imageFile, DocumentTitle, TextEditor, Comment, NhfNumber, ProductCode, LoanId);
            return Json(obj);
        }




        [HttpPost]
        [AuthorizeFilter("checklist:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await _iAddDocumentProcedureService.DeleteForm(ids);
            return Json(obj);
        }


        #endregion
    }
}
