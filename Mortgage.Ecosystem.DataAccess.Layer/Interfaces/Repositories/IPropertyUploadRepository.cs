using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface IPropertyUploadRepository
    {
        Task<List<PropertyUploadEntity>> GetList(long id);
        Task<List<PropertyUploadEntity>> GetPageList(PropertyUploadListParam param, Pagination pagination);
        Task<PropertyUploadEntity> GetEntity(long id);
        Task SaveForm(PropertyUploadEntity entity);
        Task DeleteForm(string ids);
        Task<List<PropertyUploadEntity>> GetList2(PropertyUploadListParam param);
    }
}
