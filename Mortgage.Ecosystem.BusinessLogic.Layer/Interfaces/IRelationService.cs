using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface IRelationService
    {
        Task<TData<List<RelationEntity>>> GetList(RelationListParam param);
        Task<TData<List<RelationEntity>>> GetPageList(RelationListParam param, Pagination pagination);
        Task<TData<RelationEntity>> GetEntity(long id);
        Task<TData<string>> SaveForm(RelationEntity entity);
        Task<TData> DeleteForm(string ids);
    }
}
