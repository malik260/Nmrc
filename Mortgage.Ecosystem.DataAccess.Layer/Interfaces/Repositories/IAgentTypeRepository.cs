using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface IAgentTypeRepository
    {
        Task<List<AgentTypeEntity>> GetList(AgentTypeListParam param);
        Task<List<AgentTypeEntity>> GetPageList(AgentTypeListParam param, Pagination pagination);
        Task<AgentTypeEntity> GetEntity(long id);
        Task SaveForm(AgentTypeEntity entity);
        Task DeleteForm(string ids);
    }
}
