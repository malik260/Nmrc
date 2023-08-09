using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface ICreditScoreService
    {
        Task<TData<List<CreditScoreEntity>>> GetList(CreditScoreListParam param);
        Task<TData<List<CreditScoreEntity>>> GetPageList(CreditScoreListParam param, Pagination pagination);
        Task<TData<List<ZtreeInfo>>> GetZtreeCreditScoreList(CreditScoreListParam param);
        Task<TData<List<ZtreeInfo>>> GetZtreeUserList(CreditScoreListParam param);
        Task<TData<CreditScoreEntity>> GetEntity(long id);
        Task<TData<int>> GetMaxSort();
        Task<TData<string>> SaveForm(CreditScoreEntity entity);
        Task<TData> DeleteForm(string ids);
    }
}
