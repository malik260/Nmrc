using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface IUnlockAdminUserService
    {
        Task<TData<List<UnlockAdminUserEntity>>> GetList(UnlockAdminUserListParam param);
        Task<TData<List<UnlockAdminUserEntity>>> GetPageList(UnlockAdminUserListParam param, Pagination pagination);
        Task<TData<List<ZtreeInfo>>> GetZtreeUnlockAdminUserList(UnlockAdminUserListParam param);
        Task<TData<List<ZtreeInfo>>> GetZtreeUserList(UnlockAdminUserListParam param);
        Task<TData<UnlockAdminUserEntity>> GetEntity(long id);
        Task<TData<int>> GetMaxSort();
        Task<TData<string>> SaveForm(UnlockAdminUserEntity entity);
        Task<TData> DeleteForm(string ids);
    }
}
