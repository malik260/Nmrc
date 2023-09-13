using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface IUnlockAdminUserRepository
    {
        Task<List<UnlockAdminUserEntity>> GetList(UnlockAdminUserListParam param);
        Task<List<UnlockAdminUserEntity>> GetPageList(UnlockAdminUserListParam param, Pagination pagination);
        Task<UnlockAdminUserEntity> GetEntity(long id);
        Task<int> GetMaxSort();
        Task SaveForm(UnlockAdminUserEntity entity);
        Task DeleteForm(string ids);
    }
}
