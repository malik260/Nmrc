using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface INmrcCategoryService
    {
        Task<TData<List<NmrcCategoryEntity>>> GetList(NmrcCategoryListParam param);
        Task<TData<List<NmrcCategoryEntity>>> GetPageList(NmrcCategoryListParam param, Pagination pagination);
        Task<TData<NmrcCategoryEntity>> GetEntity(int id);
        Task<TData<string>> SaveForm(NmrcCategoryEntity entity);
        Task<TData> DeleteForm(string ids);
    }
}
