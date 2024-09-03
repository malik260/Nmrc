using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface INextOfKinService
    {
        Task<TData<List<NextOfKinEntity>>> GetList(NextOfKinListParam param);
        Task<TData<List<NextOfKinEntity>>> GetPageList(NextOfKinListParam param, Pagination pagination);
        Task<TData<NextOfKinEntity>> GetEntity(long id);
        Task<TData<string>> SaveForm(NextOfKinEntity entity);
        Task<TData> DeleteForm(string ids);
        Task<TData<NextOfKinEntity>> GetEntityByEmployee(long employeeId);
    }
}
