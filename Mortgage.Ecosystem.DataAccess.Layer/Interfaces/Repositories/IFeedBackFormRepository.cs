using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface IFeedBackFormRepository
    {
        Task<List<FeedBackFormEntity>> GetList(FeedBackFormListParam param);
        Task<List<FeedBackFormEntity>> GetPageList(FeedBackFormListParam param, Pagination pagination);
        Task<FeedBackFormEntity> GetEntity(long id);
        Task<int> GetMaxSort();
        Task SaveForm(FeedBackFormEntity entity);
        Task DeleteForm(string ids);
    }
}
