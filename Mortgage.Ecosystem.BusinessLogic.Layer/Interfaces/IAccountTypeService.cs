using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface IAccountTypeService
    {
        Task<TData<List<AccountTypeEntity>>> GetList(AccountTypeListParam param);
        Task<TData<List<AccountTypeEntity>>> GetPageList(AccountTypeListParam param, Pagination pagination);
        Task<TData<AccountTypeEntity>> GetEntity(long id);
        Task<TData<string>> SaveForm(AccountTypeEntity entity);
        Task<TData> DeleteForm(string ids);
    }
}
