using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface IRoleRepository
    {
        Task<List<RoleEntity>> GetList(RoleListParam param);
        Task<List<RoleEntity>> GetPageList(RoleListParam param, Pagination pagination);
        Task<RoleEntity> GetEntity(long id);
        Task<int> GetMaxSort();
        bool ExistRoleName(RoleEntity entity);
        Task SaveForm(RoleEntity entity);
        Task DeleteForm(string ids);
    }
}
