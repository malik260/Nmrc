using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface INHFRegUsersService
    {
        Task<TData<List<NHFRegUsersEntity>>> GetList(NHFRegUsersListParam param);
        Task<TData<List<NHFRegUsersEntity>>> GetPageList(NHFRegUsersListParam param, Pagination pagination);
        Task<TData<List<ZtreeInfo>>> GetZtreeNHFRegUsersList(NHFRegUsersListParam param);
        Task<TData<List<ZtreeInfo>>> GetZtreeUserList(NHFRegUsersListParam param);
        Task<TData<NHFRegUsersEntity>> GetEntity(long id);
        Task<TData<int>> GetMaxSort();
        Task<TData<string>> SaveForm(NHFRegUsersEntity entity);
        Task<TData> DeleteForm(string ids);
    }
}
