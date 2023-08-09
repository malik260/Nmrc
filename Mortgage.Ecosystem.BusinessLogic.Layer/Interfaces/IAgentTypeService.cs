using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface IAgentTypeService
    {
        Task<TData<List<AgentTypeEntity>>> GetList(AgentTypeListParam param);
        Task<TData<List<AgentTypeEntity>>> GetPageList(AgentTypeListParam param, Pagination pagination);
        Task<TData<AgentTypeEntity>> GetEntity(long id);
        Task<TData<string>> SaveForm(AgentTypeEntity entity);
        Task<TData> DeleteForm(string ids);
    }
}
