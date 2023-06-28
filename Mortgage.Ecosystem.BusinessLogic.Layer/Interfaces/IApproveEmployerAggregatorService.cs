using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface IApproveEmployerAggregatorService
    {
        Task<TData<List<ApproveEmployerAggregatorEntity>>> GetList(ApproveEmployerAggregatorListParam param);
        Task<TData<List<ApproveEmployerAggregatorEntity>>> GetPageList(ApproveEmployerAggregatorListParam param, Pagination pagination);
        Task<TData<List<ZtreeInfo>>> GetZtreeApproveEmployerAggregatorList(ApproveEmployerAggregatorListParam param);
        Task<TData<List<ZtreeInfo>>> GetZtreeUserList(ApproveEmployerAggregatorListParam param);
        Task<TData<ApproveEmployerAggregatorEntity>> GetEntity(long id);
        Task<TData<int>> GetMaxSort();
        Task<TData<string>> SaveForm(ApproveEmployerAggregatorEntity entity);
        Task<TData> DeleteForm(string ids);
    }
}
