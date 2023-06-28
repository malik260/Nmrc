using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface ISubSectorRepository
    {
        Task<List<SubSectorEntity>> GetList(SubSectorListParam param);
        Task<List<SubSectorEntity>> GetPageList(SubSectorListParam param, Pagination pagination);
        Task<SubSectorEntity> GetEntity(long id);
        Task SaveForm(SubSectorEntity entity);
        Task DeleteForm(string ids);
    }
}
