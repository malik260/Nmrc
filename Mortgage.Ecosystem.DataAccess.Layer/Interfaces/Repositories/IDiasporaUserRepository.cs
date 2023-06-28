using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface IDiasporaUserRepository
    {
        Task<List<DiasporaUserEntity>> GetList(DiasporaUserListParam param);
        Task<List<DiasporaUserEntity>> GetPageList(DiasporaUserListParam param, Pagination pagination);
        Task<DiasporaUserEntity> GetEntity(long id);
        Task<int> GetMaxSort();
        Task SaveForm(DiasporaUserEntity entity);
        Task DeleteForm(string ids);
    }
}
