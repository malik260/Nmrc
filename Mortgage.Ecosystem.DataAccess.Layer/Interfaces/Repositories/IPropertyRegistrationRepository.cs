using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface IPropertyRegistrationRepository
    {
        Task<List<PropertyRegistrationEntity>> GetList(PropertyRegistrationListParam param, Pagination pagination);
        Task<List<PropertyRegistrationEntity>> GetPageList(PropertyRegistrationListParam param, Pagination pagination);
        Task<PropertyRegistrationEntity> GetEntity(long id);
        Task<int> GetMaxSort();
        Task SaveForm(PropertyRegistrationEntity entity);
        Task DeleteForm(string ids);
        Task<PropertyRegistrationEntity> GetEntities(long id);
    }
}
