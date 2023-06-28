using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface INHFRegUsersRepository
    {
        Task<List<NHFRegUsersEntity>> GetList(NHFRegUsersListParam param);
        Task<List<NHFRegUsersEntity>> GetPageList(NHFRegUsersListParam param, Pagination pagination);
        Task<NHFRegUsersEntity> GetEntity(long id);
        Task<int> GetMaxSort();
        Task SaveForm(NHFRegUsersEntity entity);
        Task DeleteForm(string ids);
    }
}
