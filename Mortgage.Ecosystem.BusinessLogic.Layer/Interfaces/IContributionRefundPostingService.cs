using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface IContributionRefundPostingService
    {
        Task<TData<List<ContributionRefundPostingEntity>>> GetList(ContributionRefundPostingListParam param);
        Task<TData<List<ContributionRefundPostingEntity>>> GetPageList(ContributionRefundPostingListParam param, Pagination pagination);
        Task<TData<List<ZtreeInfo>>> GetZtreeContributionRefundPostingList(ContributionRefundPostingListParam param);
        Task<TData<List<ZtreeInfo>>> GetZtreeUserList(ContributionRefundPostingListParam param);
        Task<TData<ContributionRefundPostingEntity>> GetEntity(long id);
        Task<TData<int>> GetMaxSort();
        Task<TData<string>> SaveForm(ContributionRefundPostingEntity entity);
        Task<TData> DeleteForm(string ids);
    }
}
