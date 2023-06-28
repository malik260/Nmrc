using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface IBankService
    {
        Task<TData<List<BankEntity>>> GetList(BankListParam param);
        Task<TData<List<BankEntity>>> GetPageList(BankListParam param, Pagination pagination);
        Task<TData<BankEntity>> GetEntity(long id);
        Task<TData<string>> SaveForm(BankEntity entity);
        Task<TData> DeleteForm(string ids);
    }
}
