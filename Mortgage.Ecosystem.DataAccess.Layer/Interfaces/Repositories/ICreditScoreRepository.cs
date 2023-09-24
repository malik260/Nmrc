using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface ICreditScoreRepository
    {
        Task<List<CreditScoreEntity>> GetList(CreditScoreListParam param);
        Task<List<CreditScoreEntity>> GetPageList(CreditScoreListParam param, Pagination pagination);
        Task<CreditScoreEntity> GetEntity(long id);
        Task<int> GetMaxSort();
        Task SaveForm(CreditScoreEntity entity);
        Task DeleteForm(string ids);
        Task<CreditScoreEntity> GetScorebyWeight(int Weight);
    }
}
