using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface IBankRepository
    {
        Task<List<BankEntity>> GetList(BankListParam param);
        Task<List<BankEntity>> GetPageList(BankListParam param, Pagination pagination);
        Task<BankEntity> GetEntity(long id);
        Task SaveForm(BankEntity entity);
        Task DeleteForm(string ids);
    }
}
