using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface IRefundConditionRepository
    {
        Task<List<RefundConditionEntity>> GetList(RefundConditionListParam param);
        Task<List<RefundConditionEntity>> GetPageList(RefundConditionListParam param, Pagination pagination);
        Task<RefundConditionEntity> GetEntity(long id);
        Task<int> GetMaxSort();
        Task SaveForm(RefundConditionEntity entity);
        Task DeleteForm(string ids);
    }
}
