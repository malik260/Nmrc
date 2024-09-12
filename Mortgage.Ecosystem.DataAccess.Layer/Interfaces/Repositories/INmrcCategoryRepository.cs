using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface INmrcCategoryRepository
    {
        Task<List<NmrcCategoryEntity>> GetList(NmrcCategoryListParam param);
        Task<NmrcCategoryEntity> GetEntityByCategoryId(long Id);
        Task<List<NmrcCategoryEntity>> GetPageList(NmrcCategoryListParam param, Pagination pagination);
        Task<NmrcCategoryEntity> GetEntity(int id);
        Task SaveForm(NmrcCategoryEntity entity);
        Task DeleteForm(string ids);
    }
}
