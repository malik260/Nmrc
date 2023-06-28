using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface ICompanyTypeRepository
    {
        Task<List<CompanyTypeEntity>> GetList(CompanyTypeListParam param);
        Task<List<CompanyTypeEntity>> GetPageList(CompanyTypeListParam param, Pagination pagination);
        Task<CompanyTypeEntity> GetEntity(long id);
        Task SaveForm(CompanyTypeEntity entity);
        Task DeleteForm(string ids);
    }
}
