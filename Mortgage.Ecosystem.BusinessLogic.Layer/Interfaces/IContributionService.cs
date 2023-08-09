using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface IContributionService
    {
        Task<TData<List<ContributionEntity>>> GetList(ContributionParam param);
        Task<TData<List<ContributionEntity>>> GetPageList(ContributionParam param, Pagination pagination);
        Task<TData<List<ZtreeInfo>>> GetZtreeSingleContributionList(ContributionParam param);
        Task<TData<List<ZtreeInfo>>> GetZtreeUserList(ContributionParam param);
        Task<TData<ContributionEntity>> GetEntity(long id);
        Task<TData<int>> GetMaxSort();
        Task<TData<RemitaPaymentDetailsEntity>> SingleContribution(ContributionEntity entity);
        Task<TData<string>> SaveForm(ContributionEntity entity);
        Task<TData> DeleteForm(string ids);
    }
}
