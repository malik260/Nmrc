using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface IInternetBankingUsersService
    {
        Task<TData<List<InternetBankingUsersEntity>>> GetList(InternetBankingUsersListParam param);
        Task<TData<List<InternetBankingUsersEntity>>> GetPageList(InternetBankingUsersListParam param, Pagination pagination);
        Task<TData<List<ZtreeInfo>>> GetZtreeInternetBankingUsersList(InternetBankingUsersListParam param);
        Task<TData<List<ZtreeInfo>>> GetZtreeUserList(InternetBankingUsersListParam param);
        Task<TData<InternetBankingUsersEntity>> GetEntity(long id);
        Task<TData<int>> GetMaxSort();
        Task<TData<string>> SaveForm(InternetBankingUsersEntity entity);
        Task<TData> DeleteForm(string ids);
    }
}
