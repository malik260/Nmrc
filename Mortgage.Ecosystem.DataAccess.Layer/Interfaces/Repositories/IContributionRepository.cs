using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface IContributionRepository
    {
        Task<List<ContributionEntity>> GetList(ContributionListParam param);
        Task<List<ContributionEntity>> GetPageList(ContributionListParam param, Pagination pagination);
        Task<ContributionEntity> GetEntity(long id);
        Task<int> GetMaxSort();
        Task SaveForm(ContributionEntity entity);
        Task<List<ContributionEntity>> GetEmployerPageList(ContributionListParam param, Pagination pagination);
        Task SaveForms(List<ContributionEntity> entity);
        Task DeleteForm(string ids);
    }
}
