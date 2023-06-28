using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface IPropertySubscriptionRepository
    {
        Task<List<PropertySubscriptionEntity>> GetList(PropertySubscriptionListParam param);
        Task<List<PropertySubscriptionEntity>> GetPageList(PropertySubscriptionListParam param, Pagination pagination);
        Task<PropertySubscriptionEntity> GetEntity(long id);
        Task<int> GetMaxSort();
        Task SaveForm(PropertySubscriptionEntity entity);
        Task DeleteForm(string ids);
    }
}
