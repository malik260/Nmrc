using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface ILoanInitiationUploadService
    {
        Task<TData<List<LoanInitiationUploadEntity>>> GetList(long id);
        Task<TData<List<LoanInitiationUploadEntity>>> GetPageList(LoanInitiationUploadListParam param, Pagination pagination);
        Task<TData<LoanInitiationUploadEntity>> GetEntity(long id);
        Task<TData<string>> SaveForm(LoanInitiationUploadEntity entity);
        Task<TData> DeleteForm(string ids);
    }
}
