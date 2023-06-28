using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface ILoanScheduleRepository
    {
        Task<List<LoanScheduleEntity>> GetList(LoanScheduleListParam param);
        Task<List<LoanScheduleEntity>> GetPageList(LoanScheduleListParam param, Pagination pagination);
        Task<LoanScheduleEntity> GetEntity(long id);
        Task<int> GetMaxSort();
        Task SaveForm(LoanScheduleEntity entity);
        Task DeleteForm(string ids);
    }
}
