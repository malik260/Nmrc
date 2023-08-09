using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface IRefundProfilingRepository
    {
        Task<List<RefundProfilingEntity>> GetList(RefundProfilingListParam param);
        Task<List<RefundProfilingEntity>> GetPageList(RefundProfilingListParam param, Pagination pagination);
        Task<RefundProfilingEntity> GetEntity(long id);
        Task<int> GetMaxSort();
        Task SaveForm(RefundProfilingEntity entity);
        Task DeleteForm(string ids);
    }
}
