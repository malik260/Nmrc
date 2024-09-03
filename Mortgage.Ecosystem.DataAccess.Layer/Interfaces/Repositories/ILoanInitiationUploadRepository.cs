using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface ILoanInitiationUploadRepository
    {
        Task<List<LoanInitiationUploadEntity>> GetList(long id);
        Task<List<LoanInitiationUploadEntity>> GetPageList(LoanInitiationUploadListParam param, Pagination pagination);
        Task<LoanInitiationUploadEntity> GetEntity(long id);
        Task SaveForm(LoanInitiationUploadEntity entity);
        Task DeleteForm(string ids);
    }
}
