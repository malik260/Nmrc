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
    public class MaritalStatusRepository : DataRepository, IMaritalStatusRepository
    {
        #region Retrieve data
        public async Task<List<MaritalStatusEntity>> GetList(MaritalStatusListParam param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<MaritalStatusEntity>> GetPageList(MaritalStatusListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<MaritalStatusEntity> GetEntity(long id)
        {
            return await BaseRepository().FindEntity<MaritalStatusEntity>(id);
        }
        #endregion

        #region Submit data
        public async Task SaveForm(MaritalStatusEntity entity)
        {
            if (entity.Id.IsNullOrZero())
            {
                await entity.Create();
                await BaseRepository().Insert<MaritalStatusEntity>(entity);
            }
            else
            {
                await BaseRepository().Update<MaritalStatusEntity>(entity);
            }
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await BaseRepository().Delete<MaritalStatusEntity>(idArr);
        }
        #endregion

        #region Private method
        private Expression<Func<MaritalStatusEntity, bool>> ListFilter(MaritalStatusListParam param)
        {
            var expression = ExtensionLinq.True<MaritalStatusEntity>();
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