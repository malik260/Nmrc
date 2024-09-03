using Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Base;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<List<UserEntity>> GetList(UserListParam param);

        Task<List<UserEntity>> GetPageList(UserListParam param, Pagination pagination);

        Task<UserEntity> GetEntity(int id);
        Task<UserEntity> GetEntityByCompany(long company);

        Task<UserEntity> GetEntity(string userName);

        Task<UserEntity> CheckLogin(string userName);

        Task<UserEntity> GetEntity(long company, long employee);

        bool ExistUserName(UserEntity entity);

        bool CheckUserName(string? userName);

        Task UpdateUser(UserEntity entity);

        Task SaveForm(UserEntity entity);

        Task DeleteForm(string ids);

        Task ResetPassword(UserEntity entity);

        Task ChangeUser(UserEntity entity);
        Task<UserEntity> GetEntityByPmb(long pmb);

        Task<UserEntity> GetEntityByUsername(string username);
    }
}