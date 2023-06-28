using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface ICompanyRepository
    {
        Task<List<CompanyEntity>> GetList(CompanyListParam param);
        Task<List<CompanyEntity>> GetPageList(CompanyListParam param, Pagination pagination);
        Task<CompanyEntity> GetEntity(long id);
        bool ExistCompany(CompanyEntity entity);
        bool ExistRCNumber(CompanyEntity entity);
        Task SaveForm(CompanyEntity entity);
        Task SaveForms(CompanyEntity entity);
        Task DeleteForm(string ids);
    }
}
