using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface IMenuAuthorizeRepository
    {
        Task<List<MenuAuthorizeEntity>> GetList(MenuAuthorizeEntity param);
        Task<MenuAuthorizeEntity> GetEntity(long id);
        Task SaveForm(MenuAuthorizeEntity entity);
        Task DeleteForm(long id);
    }
}
