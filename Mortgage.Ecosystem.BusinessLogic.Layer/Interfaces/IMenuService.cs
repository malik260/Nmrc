using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface IMenuService
    {
        Task<TData<List<MenuEntity>>> GetList(MenuListParam param);
        Task<TData<List<ZtreeInfo>>> GetZtreeList(MenuListParam param);
        Task<TData<MenuEntity>> GetEntity(long id);
        Task<TData<int>> GetMaxSort(long parent);
        Task<TData<string>> SaveForm(MenuEntity entity);
        Task<TData> DeleteForm(string ids);
    }
}