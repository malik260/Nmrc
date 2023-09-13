using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface IChargeSetupService
    {
        Task<TData<List<ChargeSetupEntity>>> GetList(ChargeSetupListParam param);
        Task<TData<List<ChargeSetupEntity>>> GetPageList(ChargeSetupListParam param, Pagination pagination);
        Task<TData<List<ZtreeInfo>>> GetZtreeChargeSetupList(ChargeSetupListParam param);
        Task<TData<List<ZtreeInfo>>> GetZtreeUserList(ChargeSetupListParam param);
        Task<TData<ChargeSetupEntity>> GetEntity(long id);
        Task<TData<int>> GetMaxSort();
        Task<TData<string>> SaveForm(ChargeSetupEntity entity);

        Task<TData> DeleteForm(string ids);
    }
}
