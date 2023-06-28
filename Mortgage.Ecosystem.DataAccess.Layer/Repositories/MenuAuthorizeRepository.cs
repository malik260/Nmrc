using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
using Mortgage.Ecosystem.DataAccess.Layer.Extensions;
using Mortgage.Ecosystem.DataAccess.Layer.Helpers;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Operator;

namespace Mortgage.Ecosystem.DataAccess.Layer.Repositories
{
    public class MenuAuthorizeRepository : DataRepository, IMenuAuthorizeRepository
    {
        #region Get data
        public async Task<List<MenuAuthorizeEntity>> GetList(MenuAuthorizeEntity param)
        {
            var expression = ExtensionLinq.True<MenuAuthorizeEntity>();
            if (param != null)
            {
                if (param.AuthorizeId.ToLong() > 0)
                {
                    expression = expression.And(t => t.AuthorizeId == param.AuthorizeId);
                }
                if (param.AuthorizeType.ToInt() > 0)
                {
                    expression = expression.And(t => t.AuthorizeType == param.AuthorizeType);
                }
                if (!param.AuthorizeIds.IsNull())
                {
                    long[] authorizeIdArr = TextHelper.SplitToArray<long>(param.AuthorizeIds, ',');
                    expression = expression.And(t => authorizeIdArr.Contains(t.AuthorizeId));
                }
            }
            var list = await BaseRepository().FindList<MenuAuthorizeEntity>(expression);
            return list.ToList();
        }

        public async Task<MenuAuthorizeEntity> GetEntity(long id)
        {
            return await BaseRepository().FindEntity<MenuAuthorizeEntity>(id);
        }
        #endregion

        #region Submit data
        public async Task SaveForm(MenuAuthorizeEntity entity)
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
            await BaseRepository().Delete<MenuAuthorizeEntity>(id);
        }
        #endregion
    }
}