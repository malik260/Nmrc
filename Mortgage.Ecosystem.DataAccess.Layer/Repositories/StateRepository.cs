using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
using Mortgage.Ecosystem.DataAccess.Layer.Extensions;
using Mortgage.Ecosystem.DataAccess.Layer.Helpers;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Operator;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;
using System.Linq.Expressions;

namespace Mortgage.Ecosystem.DataAccess.Layer.Repositories
{
    public class StateRepository : DataRepository, IStateRepository
    {
        #region Retrieve data
        public async Task<List<StateEntity>> GetList(StateListParam param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<StateEntity>> GetPageList(StateListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<StateEntity> GetEntity(long id)
        {
            return await BaseRepository().FindEntity<StateEntity>(x=> x.Id == id);
        }
        #endregion

        #region Submit data
        public async Task SaveForm(StateEntity entity)
        {
            if (entity.Id.IsNullOrZero())
            {
                await entity.Create();
                await BaseRepository().Insert<StateEntity>(entity);
            }
            else
            {
                await BaseRepository().Update<StateEntity>(entity);
            }
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await BaseRepository().Delete<StateEntity>(idArr);
        }
        #endregion

        #region Private method
        private Expression<Func<StateEntity, bool>> ListFilter(StateListParam param)
        {
            var expression = ExtensionLinq.True<StateEntity>();
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.Code))
                {
                    expression = expression.And(second: t => t.Code.Contains(param.Code));
                }
            }
            return expression;
        }
        #endregion
    }
}