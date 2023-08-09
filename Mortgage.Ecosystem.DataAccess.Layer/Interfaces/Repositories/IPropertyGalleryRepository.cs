using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface IPropertyGalleryRepository
    {
        Task<List<PropertyGalleryEntity>> GetList(PropertyGalleryListParam param);
        Task<List<PropertyGalleryEntity>> GetPageList(PropertyGalleryListParam param, Pagination pagination);
        Task<PropertyGalleryEntity> GetEntity(long id);
        Task<int> GetMaxSort();
        Task SaveForm(PropertyGalleryEntity entity);
        Task DeleteForm(string ids);
       
    }
}
