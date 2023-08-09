using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface IUserBelongRepository
    {
        Task<List<UserBelongEntity>> GetList(UserBelongEntity entity);
        Task<UserBelongEntity> GetEntity(long id);
        Task SaveForm(UserBelongEntity entity);
        Task DeleteForm(long id);
    }
}
