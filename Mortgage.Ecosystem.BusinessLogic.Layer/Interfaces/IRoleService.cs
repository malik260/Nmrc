using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface IRoleService
    {
        Task<TData<List<RoleEntity>>> GetList(RoleListParam param);
        Task<TData<List<RoleEntity>>> GetPageList(RoleListParam param, Pagination pagination);
        Task<TData<List<ZtreeInfo>>> GetZtreeRoleList(RoleListParam param);
        Task<TData<RoleEntity>> GetEntity(long id);
        Task<TData<int>> GetMaxSort();
        Task<TData<string>> SaveForm(RoleEntity entity);
        Task<TData> DeleteForm(string ids);
    }
}