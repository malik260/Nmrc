using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface IRelationRepository
    {
        Task<List<RelationEntity>> GetList(RelationListParam param);
        Task<List<RelationEntity>> GetPageList(RelationListParam param, Pagination pagination);
        Task<RelationEntity> GetEntity(long id);
        Task SaveForm(RelationEntity entity);
        Task DeleteForm(string ids);
    }
}
