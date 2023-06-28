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
    public class AlertTypeRepository : DataRepository, IAlertTypeRepository
    {
        #region Retrieve data
        public async Task<List<AlertTypeEntity>> GetList(AlertTypeListParam param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<AlertTypeEntity>> GetPageList(AlertTypeListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<AlertTypeEntity> GetEntity(long id)
        {
            return await BaseRepository().FindEntity<AlertTypeEntity>(id);
        }
        #endregion

        #region Submit data
        public async Task SaveForm(AlertTypeEntity entity)
        {
            if (entity.Id.IsNullOrZero())
            {
                await entity.Create();
                await BaseRepository().Insert<AlertTypeEntity>(entity);
            }
            else
            {
                await BaseRepository().Update<AlertTypeEntity>(entity);
            }
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await BaseRepository().Delete<AlertTypeEntity>(idArr);
        }
        #endregion

        #region Private method
        private Expression<Func<AlertTypeEntity, bool>> ListFilter(AlertTypeListParam param)
        {
            var expression = ExtensionLinq.True<AlertTypeEntity>();
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