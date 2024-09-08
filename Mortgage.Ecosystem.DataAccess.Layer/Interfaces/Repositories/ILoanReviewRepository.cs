using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface ILoanReviewRepository
    {
        Task<List<LoanReviewEntity>> GetList(LoanReviewListParam param);
        Task<List<LoanReviewEntity>> GetPageList(LoanReviewListParam param, Pagination pagination);
        Task<LoanReviewEntity> GetEntity(string code);
        Task<int> GetMaxSort();
        Task SaveForm(LoanReviewEntity entity);
        Task DeleteForm(string ids);
        Task<LoanReviewEntity> GetEntityById(long id);
    }
}
