using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface IUnlockNhfPortalRepository
    {
        Task<List<UnlockNhfPortalEntity>> GetList(UnlockNhfPortalListParam param);
        Task<List<UnlockNhfPortalEntity>> GetPageList(UnlockNhfPortalListParam param, Pagination pagination);
        Task<UnlockNhfPortalEntity> GetEntity(long id);
        Task<int> GetMaxSort();
        Task SaveForm(UnlockNhfPortalEntity entity);
        Task DeleteForm(string ids);
    }
}
