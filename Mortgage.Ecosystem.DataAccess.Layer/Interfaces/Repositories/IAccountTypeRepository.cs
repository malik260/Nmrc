using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface IAccountTypeRepository
    {
        Task<List<AccountTypeEntity>> GetList(AccountTypeListParam param);
        Task<List<AccountTypeEntity>> GetPageList(AccountTypeListParam param, Pagination pagination);
        Task<AccountTypeEntity> GetEntity(long id);
        Task SaveForm(AccountTypeEntity entity);
        Task DeleteForm(string ids);
    }
}
