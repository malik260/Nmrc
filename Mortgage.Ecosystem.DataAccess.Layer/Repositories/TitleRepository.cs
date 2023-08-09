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
    public class TitleRepository : DataRepository, ITitleRepository
    {
        #region Retrieve data
        public async Task<List<TitleEntity>> GetList(TitleListParam param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<TitleEntity>> GetPageList(TitleListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<TitleEntity> GetEntity(long id)
        {
            return await BaseRepository().FindEntity<TitleEntity>(id);
        }
        #endregion

        #region Submit data
        public async Task SaveForm(TitleEntity entity)
        {
            if (entity.Id.IsNullOrZero())
            {
                await entity.Create();
                await BaseRepository().Insert<TitleEntity>(entity);
            }
            else
            {
                await BaseRepository().Update<TitleEntity>(entity);
            }
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await BaseRepository().Delete<TitleEntity>(idArr);
        }
        #endregion

        #region Private method
        private Expression<Func<TitleEntity, bool>> ListFilter(TitleListParam param)
        {
            var expression = ExtensionLinq.True<TitleEntity>();
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.Name))
                {
                    expression = expression.And(second: t => t.Name.Contains(param.Name));
                }
            }
            return expression;
        }
        #endregion
    }
}