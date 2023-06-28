using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface IContributionFrequencyRepository
    {
        Task<List<ContributionFrequencyEntity>> GetList(ContributionFrequencyListParam param);
        Task<List<ContributionFrequencyEntity>> GetPageList(ContributionFrequencyListParam param, Pagination pagination);
        Task<ContributionFrequencyEntity> GetEntity(long id);
        Task SaveForm(ContributionFrequencyEntity entity);
        Task DeleteForm(string ids);
    }
}
