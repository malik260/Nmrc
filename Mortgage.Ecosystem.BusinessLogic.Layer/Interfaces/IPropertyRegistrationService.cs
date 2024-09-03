using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface IPropertyRegistrationService
    {
        Task<TData<List<PropertyRegistrationEntity>>> GetList(PropertyRegistrationListParam param, Pagination pagination);
        Task<TData<List<PropertyRegistrationEntity>>> GetPageList(PropertyRegistrationListParam param, Pagination pagination);
        //Task<TData<List<ZtreeInfo>>> GetZtreePropertyRegistrationList(PropertyRegistrationListParam param);
        //Task<TData<List<ZtreeInfo>>> GetZtreeUserList(PropertyRegistrationListParam param);
        //Task<TData<PropertyRegistrationEntity>> GetEntity(long id);
        Task<TData<List<PropertyRegistrationListParam>>> GetEntity(long id);
        Task<TData<int>> GetMaxSort();
        Task<TData<string>> SaveForm(PropertyRegistrationEntity entity);
        Task<TData> DeleteForm(string ids);
        Task<TData<CustomerDetailsViewModel>> GetPmbCompanyName();
        Task<TData<PropertyRegistrationEntity>> GetEntities(long id);
    }
}
