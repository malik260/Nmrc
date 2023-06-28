using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface ILoanScheduleService
    {
        Task<TData<List<LoanScheduleEntity>>> GetList(LoanScheduleListParam param);
        Task<TData<List<LoanScheduleEntity>>> GetPageList(LoanScheduleListParam param, Pagination pagination);
        Task<TData<List<ZtreeInfo>>> GetZtreeLoanScheduleList(LoanScheduleListParam param);
        Task<TData<List<ZtreeInfo>>> GetZtreeUserList(LoanScheduleListParam param);
        Task<TData<LoanScheduleEntity>> GetEntity(long id);
        Task<TData<int>> GetMaxSort();
        Task<TData<string>> SaveForm(LoanScheduleEntity entity);
        Task<TData> DeleteForm(string ids);
        Task<TData<List<LoanSchedule>>> LoanSchedule(string applicationRefNo);
    }
}
