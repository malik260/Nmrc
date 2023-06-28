using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface IApproveEmployerAggregatorRepository
    {
        Task<List<ApproveEmployerAggregatorEntity>> GetList(ApproveEmployerAggregatorListParam param);
        Task<List<ApproveEmployerAggregatorEntity>> GetPageList(ApproveEmployerAggregatorListParam param, Pagination pagination);
        Task<ApproveEmployerAggregatorEntity> GetEntity(long id);
        Task<int> GetMaxSort();
        Task SaveForm(ApproveEmployerAggregatorEntity entity);
        Task DeleteForm(string ids);
    }
}
