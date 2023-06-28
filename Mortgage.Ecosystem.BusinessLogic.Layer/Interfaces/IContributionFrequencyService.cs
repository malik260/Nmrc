using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface IContributionFrequencyService
    {
        Task<TData<List<ContributionFrequencyEntity>>> GetList(ContributionFrequencyListParam param);
        Task<TData<List<ContributionFrequencyEntity>>> GetPageList(ContributionFrequencyListParam param, Pagination pagination);
        Task<TData<ContributionFrequencyEntity>> GetEntity(long id);
        Task<TData<string>> SaveForm(ContributionFrequencyEntity entity);
        Task<TData> DeleteForm(string ids);
    }
}
