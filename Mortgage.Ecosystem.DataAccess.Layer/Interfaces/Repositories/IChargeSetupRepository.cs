using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface IChargeSetupRepository
    {
        Task<List<ChargeSetupEntity>> GetList(ChargeSetupListParam param);
        Task<List<ChargeSetupEntity>> GetPageList(ChargeSetupListParam param, Pagination pagination);
        Task<ChargeSetupEntity> GetEntity(long id);
        Task<int> GetMaxSort();
        Task SaveForm(ChargeSetupEntity entity);
        Task DeleteForm(string ids);
    }
}
