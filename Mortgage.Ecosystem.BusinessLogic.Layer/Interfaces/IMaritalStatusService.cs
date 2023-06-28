using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface IMaritalStatusService
    {
        Task<TData<List<MaritalStatusEntity>>> GetList(MaritalStatusListParam param);
        Task<TData<List<MaritalStatusEntity>>> GetPageList(MaritalStatusListParam param, Pagination pagination);
        Task<TData<MaritalStatusEntity>> GetEntity(long id);
        Task<TData<string>> SaveForm(MaritalStatusEntity entity);
        Task<TData> DeleteForm(string ids);
    }
}
