using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface IPropertyUploadService
    {
        Task<TData<List<PropertyUploadEntity>>> GetList(long id);
        Task<TData<List<PropertyUploadEntity>>> GetPageList(PropertyUploadListParam param, Pagination pagination);
        Task<TData<PropertyUploadEntity>> GetEntity(long id);
        Task<TData<string>> SaveForm(PropertyUploadEntity entity);
        Task<TData> DeleteForm(string ids);
        Task<TData<List<PropertyUploadEntity>>> GetPropertyList(PropertyUploadListParam param);
    }
}
