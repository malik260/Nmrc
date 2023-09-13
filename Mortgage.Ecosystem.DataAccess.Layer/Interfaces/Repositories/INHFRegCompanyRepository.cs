using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface INHFRegCompanyRepository
    {
        Task<List<NHFRegCompanyEntity>> GetList(NHFRegCompanyListParam param);
        Task<List<NHFRegCompanyEntity>> GetPageList(NHFRegCompanyListParam param, Pagination pagination);
        Task<NHFRegCompanyEntity> GetEntity(long id);
        Task<int> GetMaxSort();
        Task SaveForm(NHFRegCompanyEntity entity);
        Task DeleteForm(string ids);
    }
}
