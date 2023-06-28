using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface IPropertyGalleryService
    {
        Task<TData<List<PropertyGalleryEntity>>> GetList(PropertyGalleryListParam param);
        Task<TData<List<PropertyGalleryEntity>>> GetPageList(PropertyGalleryListParam param, Pagination pagination);
        Task<TData<List<ZtreeInfo>>> GetZtreePropertyGalleryList(PropertyGalleryListParam param);
        Task<TData<List<ZtreeInfo>>> GetZtreeUserList(PropertyGalleryListParam param);
        Task<TData<PropertyGalleryEntity>> GetEntity(long id);
        Task<TData<int>> GetMaxSort();
        Task<TData<string>> SaveForm(PropertyGalleryEntity entity);
        Task<TData> DeleteForm(string ids);
        Task<TData<List<PropertyGalleryEntity>>> GetAllCards(PropertyGalleryListParam param, Pagination pagination);
    }
}
