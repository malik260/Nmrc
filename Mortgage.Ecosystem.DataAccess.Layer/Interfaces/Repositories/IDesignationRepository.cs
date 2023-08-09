using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface IDesignationRepository
    {
        Task<List<DesignationEntity>> GetList(DesignationListParam param);
        Task<List<DesignationEntity>> GetPageList(DesignationListParam param, Pagination pagination);
        Task<DesignationEntity> GetEntity(long id);
        Task<int> GetMaxSort();
        bool ExistDesignationName(DesignationEntity entity);
        Task SaveForm(DesignationEntity entity);
        Task DeleteForm(string ids);
    }
}
