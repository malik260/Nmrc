using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface ISectorRepository
    {
        Task<List<SectorEntity>> GetList(SectorListParam param);
        Task<List<SectorEntity>> GetPageList(SectorListParam param, Pagination pagination);
        Task<SectorEntity> GetEntity(long id);
        Task SaveForm(SectorEntity entity);
        Task DeleteForm(string ids);
    }
}
