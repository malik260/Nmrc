using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface IContributionRefundPostingRepository
    {
        Task<List<ContributionRefundPostingEntity>> GetList(ContributionRefundPostingListParam param);
        Task<List<ContributionRefundPostingEntity>> GetPageList(ContributionRefundPostingListParam param, Pagination pagination);
        Task<ContributionRefundPostingEntity> GetEntity(long id);
        Task<int> GetMaxSort();
        Task SaveForm(ContributionRefundPostingEntity entity);
        Task DeleteForm(string ids);
    }
}
