using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface IDesignationService
    {
        Task<TData<List<DesignationEntity>>> GetList(DesignationListParam param);
        Task<TData<List<DesignationEntity>>> GetPageList(DesignationListParam param, Pagination pagination);
        Task<TData<DesignationEntity>> GetEntity(long id);
        Task<TData<int>> GetMaxSort();
        Task<TData<string>> SaveForm(DesignationEntity entity);
        Task<TData> DeleteForm(string ids);
    }
}