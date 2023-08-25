using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface IRefundRepository
    {
        Task<List<RefundEntity>> GetList(RefundListParam param);
        Task<List<RefundEntity>> GetPageList(RefundListParam param, Pagination pagination);
        Task<RefundEntity> GetEntity(long id);
        bool ExistingRefund(string nhfNo, string employerNumber, long id);
        Task<int> GetMaxSort();
        Task SaveForm(RefundEntity entity);
        Task DeleteForm(string ids);
    }
}