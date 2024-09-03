using Microsoft.AspNetCore.Http;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface IAddDocumentProcedureService
    {

        Task<TData<AddDocumentProcedureEntity>> SaveForm(IFormFile imageFile, string DocumentTitle, string TextEditor, string Comment, string NhfNumber, string ProductCode, string LoanId);
        // Task SaveForms(List<ContributionEntity> entity);
        Task<TData> DeleteForm(string ids);
    }
}
