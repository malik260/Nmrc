using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface IRefundProfilingService
    {
        Task<TData<List<RefundProfilingEntity>>> GetList(RefundProfilingListParam param);
        Task<TData<List<RefundProfilingEntity>>> GetPageList(RefundProfilingListParam param, Pagination pagination);
        Task<TData<List<ZtreeInfo>>> GetZtreeRefundProfilingList(RefundProfilingListParam param);
        Task<TData<List<ZtreeInfo>>> GetZtreeUserList(RefundProfilingListParam param);
        Task<TData<RefundProfilingEntity>> GetEntity(long id);
        Task<TData<int>> GetMaxSort();
        Task<TData<string>> SaveForm(RefundProfilingEntity entity);
        Task<TData> DeleteForm(string ids);
    }
}
