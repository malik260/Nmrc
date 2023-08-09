using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface IDiasporaUserService
    {
        Task<TData<List<DiasporaUserEntity>>> GetList(DiasporaUserListParam param);
        Task<TData<List<DiasporaUserEntity>>> GetPageList(DiasporaUserListParam param, Pagination pagination);
        Task<TData<List<ZtreeInfo>>> GetZtreeDiasporaUserList(DiasporaUserListParam param);
        Task<TData<List<ZtreeInfo>>> GetZtreeUserList(DiasporaUserListParam param);
        Task<TData<DiasporaUserEntity>> GetEntity(long id);
        Task<TData<int>> GetMaxSort();
        Task<TData<string>> SaveForm(DiasporaUserEntity entity);
        Task<TData> DeleteForm(string ids);
    }
}
