using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface IRefinancingService
    {
        Task<TData<List<RefinancingEntity>>> GetList(RefinancingListParam param);
        Task<TData<List<RefinancingEntity>>> GetPageList(RefinancingListParam param, Pagination pagination);
        Task<TData<RefinancingEntity>> GetEntity();
        Task<TData<string>> SaveForm(RefinancingEntity entity);
        Task<TData> DeleteForm(string ids);
    }
}
