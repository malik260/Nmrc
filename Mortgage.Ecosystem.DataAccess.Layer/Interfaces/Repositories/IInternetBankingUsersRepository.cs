using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface IInternetBankingUsersRepository
    {
        Task<List<InternetBankingUsersEntity>> GetList(InternetBankingUsersListParam param);
        Task<List<InternetBankingUsersEntity>> GetPageList(InternetBankingUsersListParam param, Pagination pagination);
        Task<InternetBankingUsersEntity> GetEntity(long id);
        Task<int> GetMaxSort();
        Task SaveForm(InternetBankingUsersEntity entity);
        Task DeleteForm(string ids);
    }
}
