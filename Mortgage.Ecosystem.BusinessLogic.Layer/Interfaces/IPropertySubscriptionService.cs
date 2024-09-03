using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface IPropertySubscriptionService
    {
        Task<TData<List<PropertySubscriptionEntity>>> GetList(PropertySubscriptionListParam param);
        Task<TData<List<PropertySubscriptionEntity>>> GetPageList(PropertySubscriptionListParam param, Pagination pagination);
        Task<TData<List<ZtreeInfo>>> GetZtreePropertySubscriptionList(PropertySubscriptionListParam param);
        Task<TData<List<ZtreeInfo>>> GetZtreeUserList(PropertySubscriptionListParam param);
        Task<TData<PropertySubscriptionEntity>> GetEntity(long id);
        Task<TData<int>> GetMaxSort();
        Task<TData<string>> SaveForm(PropertySubscriptionEntity entity);
        Task<TData> DeleteForm(string ids);
        Task<TData<string>> Subscribe(long id);
    }
}
