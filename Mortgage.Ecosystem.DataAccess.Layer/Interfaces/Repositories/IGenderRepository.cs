using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface IGenderRepository
    {
        Task<List<GenderEntity>> GetList(GenderListParam param);
        Task<List<GenderEntity>> GetPageList(GenderListParam param, Pagination pagination);
        Task<GenderEntity> GetEntity(int id);
        Task SaveForm(GenderEntity entity);
        Task DeleteForm(string ids);
    }
}
