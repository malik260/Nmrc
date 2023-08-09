using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface ICompanyClassRepository
    {
        Task<List<CompanyClassEntity>> GetList(CompanyClassListParam param);
        Task<List<CompanyClassEntity>> GetPageList(CompanyClassListParam param, Pagination pagination);
        Task<CompanyClassEntity> GetEntity(long id);
        Task SaveForm(CompanyClassEntity entity);
        Task DeleteForm(string ids);
    }
}
