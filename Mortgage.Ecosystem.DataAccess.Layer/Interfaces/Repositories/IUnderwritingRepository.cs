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
    }
}
