using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface INmrcEligibilityRepository
    {
        Task<List<NmrcEligibilityEntity>> GetList(NmrcEligibilityListParam param);
        Task<NmrcEligibilityEntity> GetEntity(int id);
        Task<NmrcEligibilityEntity> GetEntitybyName(string item);
        //Task<int> GetMaxSort();
        Task SaveForm(NmrcEligibilityEntity entity);
        Task DeleteForm(string ids);
        Task<List<NmrcEligibilityEntity>> GetPageList(NmrcEligibilityListParam param, Pagination pagination);
        Task<NmrcEligibilityEntity> GetEntities(int id);
        Task<List<NmrcEligibilityEntity>> GetListbyCategory(string category);
        Task<NmrcEligibilityEntity> GetEntitiesbyCategoryid(int id);
    }
}