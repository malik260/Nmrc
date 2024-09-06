using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface IMenuRepository
    {
        Task<List<MenuEntity>> GetList(MenuListParam param);
        Task<List<MenuEntity>> GetEmployerMenuList();
        Task<List<MenuEntity>> GetPmbMenuList();
        Task<List<MenuEntity>> GetEmployeeMenuList();
        Task<List<MenuEntity>> GetSecondaryLenderMenuList();
        Task<MenuEntity> GetEntity(long id);
        Task<int> GetMaxSort(long parent);
        bool ExistMenuName(MenuEntity entity);
        Task SaveForm(MenuEntity entity);
        Task DeleteForm(string ids);
        Task<MenuEntity> GetEntitybyUrl(string url);
    }
}
