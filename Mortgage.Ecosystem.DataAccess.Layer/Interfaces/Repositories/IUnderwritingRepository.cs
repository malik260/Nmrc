using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface IUnderwritingRepository
    {
        Task<List<UnderwritingEntity>> GetList(UnderwritingListParam param);
        Task<List<UnderwritingEntity>> GetPageList(UnderwritingListParam param, Pagination pagination);
        Task<UnderwritingEntity> GetEntity(long id);       
        Task<int> GetMaxSort();
        Task SaveForm(UnderwritingEntity entity);
        Task DeleteForm(string ids);
        Task<UnderwritingEntity> GetEntitybyNHF(string NHF);
        Task<UnderwritingEntity> GetEntitybyLoanId(string id);
        Task<List<UnderwritingEntity>> GetApprovalPageList();
        Task<List<UnderwritingEntity>> GetLoanForReview();
        Task<List<UnderwritingEntity>> GetLoanForUnderwriting();
        Task<List<UnderwritingEntity>> GetLoanForBatching();
        Task<List<UnderwritingEntity>> GetBatchedLoan();
        Task<List<UnderwritingEntity>> GetLoanBatches(string id, Pagination pagination);
    }
}
