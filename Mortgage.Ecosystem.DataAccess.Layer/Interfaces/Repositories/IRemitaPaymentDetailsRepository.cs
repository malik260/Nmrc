using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface IRemitaPaymentDetailsRepository
    {
        Task<List<RemitaPaymentDetailsEntity>> GetList(RemitaPaymentDetailsListParam param);
        Task<List<RemitaPaymentDetailsEntity>> GetPageList(RemitaPaymentDetailsListParam param, Pagination pagination);
        Task<RemitaPaymentDetailsEntity> GetEntity(long id);
        Task<int> GetMaxSort();
        Task SaveForm(RemitaPaymentDetailsEntity entity);
        Task DeleteForm(string ids);
    }
}
