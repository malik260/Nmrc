using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface IAuditTrailService
    {
        Task<TData<List<AuditTrailEntity>>> GetList(AuditTrailListParam param);
        Task<TData<List<AuditTrailEntity>>> GetPageList(AuditTrailListParam param, Pagination pagination);
        Task<TData<List<ZtreeInfo>>> GetZtreeFinanceTransactionList(AuditTrailListParam param);
        Task<TData<List<ZtreeInfo>>> GetZtreeUserList(AuditTrailListParam param);
        Task<TData<AuditTrailEntity>> GetEntity(long id);
        Task<TData<int>> GetMaxSort();
        Task<TData<List<AuditTrailEntity>>> GetAdminPageList(AuditTrailListParam param, Pagination pagination);
        Task<TData<string>> SaveForm(AuditTrailEntity entity);
        Task<TData> DeleteForm(string ids);
    }
}