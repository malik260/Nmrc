using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface IStateService
    {
        Task<TData<List<StateEntity>>> GetList(StateListParam param);
        Task<TData<List<StateEntity>>> GetPageList(StateListParam param, Pagination pagination);
        Task<TData<StateEntity>> GetEntity(long id);
        Task<TData<string>> SaveForm(StateEntity entity);
        Task<TData> DeleteForm(string ids);
    }
}
