using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface IChangePasswordRepository
    {
        Task<List<ChangePasswordEntity>> GetList(ChangePasswordListParam param);
        Task<List<ChangePasswordEntity>> GetPageList(ChangePasswordListParam param, Pagination pagination);
        Task<ChangePasswordEntity> GetEntity(long id);
        Task<int> GetMaxSort();
        Task SaveForm(ChangePasswordEntity entity);
        Task DeleteForm(string ids);
    }
}
