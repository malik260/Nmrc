using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface ILoanInitiationRepository
    {
        Task<List<LoanInitiationEntity>> GetList(LoanInitiationListParam param);
        Task<List<LoanInitiationEntity>> GetPageList(LoanInitiationListParam param, Pagination pagination);
        Task<LoanInitiationEntity> GetEntity(long id);
        Task<int> GetMaxSort();
        Task SaveForm(LoanInitiationEntity entity);
        Task DeleteForm(string ids);
    }
}
