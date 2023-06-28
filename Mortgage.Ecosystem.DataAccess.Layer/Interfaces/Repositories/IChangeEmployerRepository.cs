using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface IChangeEmployerRepository
    {
        Task<List<ChangeEmployerEntity>> GetList(ChangeEmployerListParam param);
        Task<List<ChangeEmployerEntity>> GetPageList(ChangeEmployerListParam param, Pagination pagination);
        Task<ChangeEmployerEntity> GetEntity(long id);
        Task<int> GetMaxSort();
        Task SaveForm(ChangeEmployerEntity entity);
        Task DeleteForm(string ids);
    }
}
