using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface INationalityRepository
    {
        Task<List<NationalityEntity>> GetList(NationalityListParam param);
        Task<List<NationalityEntity>> GetPageList(NationalityListParam param, Pagination pagination);
        Task<NationalityEntity> GetEntity(long id);
        Task SaveForm(NationalityEntity entity);
        Task DeleteForm(string ids);
    }
}
