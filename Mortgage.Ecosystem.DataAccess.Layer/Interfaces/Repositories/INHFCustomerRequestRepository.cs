using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface INHFCustomerRequestRepository
    {
        Task<List<NHFCustomerRequestEntity>> GetList(NHFCustomerRequestListParam param);
        Task<List<NHFCustomerRequestEntity>> GetPageList(NHFCustomerRequestListParam param, Pagination pagination);
        Task<NHFCustomerRequestEntity> GetEntity(long id);
        Task<int> GetMaxSort();
        Task SaveForm(NHFCustomerRequestEntity entity);
        Task DeleteForm(string ids);
    }
}
