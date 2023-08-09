using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface IApproveAgentsRepository
    {
        Task<List<ApproveAgentsEntity>> GetList(ApproveAgentsListParam param);
        Task<List<ApproveAgentsEntity>> GetPageList(ApproveAgentsListParam param, Pagination pagination);
        Task<ApproveAgentsEntity> GetEntity(long id);
        Task<int> GetMaxSort();
        Task SaveForm(ApproveAgentsEntity entity);
        Task DeleteForm(string ids);
    }
}
