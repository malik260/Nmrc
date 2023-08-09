using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface IUnlockNhfPortalService
    {
        Task<TData<List<UnlockNhfPortalEntity>>> GetList(UnlockNhfPortalListParam param);
        Task<TData<List<UnlockNhfPortalEntity>>> GetPageList(UnlockNhfPortalListParam param, Pagination pagination);
        Task<TData<List<ZtreeInfo>>> GetZtreeUnlockNhfPortalList(UnlockNhfPortalListParam param);
        Task<TData<List<ZtreeInfo>>> GetZtreeUserList(UnlockNhfPortalListParam param);
        Task<TData<UnlockNhfPortalEntity>> GetEntity(long id);
        Task<TData<int>> GetMaxSort();
        Task<TData<string>> SaveForm(UnlockNhfPortalEntity entity);
        Task<TData> DeleteForm(string ids);
    }
}
