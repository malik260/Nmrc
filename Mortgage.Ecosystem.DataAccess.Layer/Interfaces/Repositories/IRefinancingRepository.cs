using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface IRefinancingRepository
    {
        Task<List<RefinancingEntity>> GetList(RefinancingListParam param);
        Task<List<RefinancingEntity>> GetPageList(RefinancingListParam param, Pagination pagination);
        Task<RefinancingEntity> GetEntity(string code);
        Task<int> GetMaxSort();
        Task SaveForm(RefinancingEntity entity);
        Task DeleteForm(string ids);
        Task<RefinancingEntity> GetEntityById(long id);
    }
}
