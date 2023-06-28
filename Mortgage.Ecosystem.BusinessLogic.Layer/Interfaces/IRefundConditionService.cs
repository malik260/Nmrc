using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface IRefundConditionService
    {
        Task<TData<List<RefundConditionEntity>>> GetList(RefundConditionListParam param);
        Task<TData<List<RefundConditionEntity>>> GetPageList(RefundConditionListParam param, Pagination pagination);
        Task<TData<List<ZtreeInfo>>> GetZtreeRefundConditionList(RefundConditionListParam param);
        Task<TData<List<ZtreeInfo>>> GetZtreeUserList(RefundConditionListParam param);
        Task<TData<RefundConditionEntity>> GetEntity(long id);
        Task<TData<int>> GetMaxSort();
        Task<TData<string>> SaveForm(RefundConditionEntity entity);
        Task<TData> DeleteForm(string ids);
    }
}
