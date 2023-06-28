using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
using Mortgage.Ecosystem.DataAccess.Layer.Extensions;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Operator;

namespace Mortgage.Ecosystem.DataAccess.Layer.Repositories
{
    public class UserBelongRepository : DataRepository, IUserBelongRepository
    {
        #region Get data
        public async Task<List<UserBelongEntity>> GetList(UserBelongEntity entity)
        {
            var expression = ExtensionLinq.True<UserBelongEntity>();
            if (entity != null)
            {
                if (!entity.BelongType.IsNullOrZero() && entity.BelongType > 0)
                {
                    expression = expression.And(t => t.BelongType == entity.BelongType);
                }
                if (entity.Employee.IsNotNull())
                {
                    expression = expression.And(t => t.Employee == entity.Employee);
                }
            }
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<UserBelongEntity> GetEntity(long id)
        {
            return await BaseRepository().FindEntity<UserBelongEntity>(id);
        }
        #endregion

        #region Submit data
        public async Task SaveForm(UserBelongEntity entity)
        {
            if (entity.Id.IsNullOrZero())
            {
                await entity.Create();
                await BaseRepository().Insert(entity);
            }
            else
            {
                await BaseRepository().Update(entity);
            }
        }

        public async Task DeleteForm(long id)
        {
            await BaseRepository().Delete<UserBelongEntity>(id);
        }
        #endregion
    }
}