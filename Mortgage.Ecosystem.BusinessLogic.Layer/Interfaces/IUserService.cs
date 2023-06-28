using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface IUserService
    {
        Task<TData<List<UserEntity>>> GetList(UserListParam param);
        Task<TData<List<UserEntity>>> GetPageList(UserListParam param, Pagination pagination);
        Task<TData<UserEntity>> GetEntity(int id);
        Task<TData<UserEntity>> CheckLogin(string userName, string password, int platform);
        Task<TData<string>> SaveForm(UserEntity entity);
        Task<TData> DeleteForm(string ids);
        Task<TData<long>> ResetPassword(UserEntity entity);
        Task<TData<long>> ChangePassword(ChangePasswordParam param);
        Task<TData<long>> ChangeUser(UserEntity entity);
        Task<TData> UpdateUser(UserEntity entity);
        Task<TData> ImportUser(ImportParam param, List<UserEntity> list);
    }
}