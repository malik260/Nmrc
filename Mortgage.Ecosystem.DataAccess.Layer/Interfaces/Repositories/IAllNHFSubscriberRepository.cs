using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface IAllNHFSubscriberRepository
    {
        Task<List<AllNHFSubscriberEntity>> GetList(AllNHFSubscriberListParam param);
        Task<List<AllNHFSubscriberEntity>> GetPageList(AllNHFSubscriberListParam param, Pagination pagination);
        Task<AllNHFSubscriberEntity> GetEntity(long id);
        Task<int> GetMaxSort();
        Task SaveForm(AllNHFSubscriberEntity entity);
        Task DeleteForm(string ids);
    }
}
