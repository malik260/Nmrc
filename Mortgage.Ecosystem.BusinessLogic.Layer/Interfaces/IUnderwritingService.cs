using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface IUnderwritingService
    {
        Task<TData<List<UnderwritingEntity>>> GetList(UnderwritingListParam param);
        Task<TData<List<UnderwritingEntity>>> GetPageList(UnderwritingListParam param, Pagination pagination);
        Task<TData<List<ZtreeInfo>>> GetZtreeUnderwritingList(UnderwritingListParam param);
        Task<TData<List<ZtreeInfo>>> GetZtreeUserList(UnderwritingListParam param);
        Task<TData<UnderwritingEntity>> GetEntity(long id);
        Task<TData<int>> GetMaxSort();
        Task<TData<string>> SaveForm(UnderwritingEntity entity);
        Task<TData> DeleteForm(string ids);
    }
}
