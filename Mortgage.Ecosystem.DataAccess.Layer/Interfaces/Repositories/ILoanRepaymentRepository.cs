using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface ILoanRepaymentRepository
    {
        Task<List<LoanRepaymentEntity>> GetList(LoanRepaymentListParam param);
        Task<List<LoanRepaymentEntity>> GetPageList(LoanRepaymentListParam param, Pagination pagination);
        Task<LoanRepaymentEntity> GetEntity(long id);
        Task<int> GetMaxSort();
        Task SaveForm(LoanRepaymentEntity entity);
        Task DeleteForm(string ids);
    }
}
