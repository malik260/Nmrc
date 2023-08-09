using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface IAlertTypeService
    {
        Task<TData<List<AlertTypeEntity>>> GetList(AlertTypeListParam param);
        Task<TData<List<AlertTypeEntity>>> GetPageList(AlertTypeListParam param, Pagination pagination);
        Task<TData<AlertTypeEntity>> GetEntity(long id);
        Task<TData<string>> SaveForm(AlertTypeEntity entity);
        Task<TData> DeleteForm(string ids);
    }
}
