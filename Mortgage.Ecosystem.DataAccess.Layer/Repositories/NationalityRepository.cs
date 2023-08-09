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
    public class NationalityRepository : DataRepository, INationalityRepository
    {
        #region Retrieve data
        public async Task<List<NationalityEntity>> GetList(NationalityListParam param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<NationalityEntity>> GetPageList(NationalityListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<NationalityEntity> GetEntity(long id)
        {
            return await BaseRepository().FindEntity<NationalityEntity>(id);
        }
        #endregion

        #region Submit data
        public async Task SaveForm(NationalityEntity entity)
        {
            if (entity.Id.IsNullOrZero())
            {
                await entity.Create();
                await BaseRepository().Insert<NationalityEntity>(entity);
            }
            else
            {
                await BaseRepository().Update<NationalityEntity>(entity);
            }
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await BaseRepository().Delete<NationalityEntity>(idArr);
        }
        #endregion

        #region Private method
        private Expression<Func<NationalityEntity, bool>> ListFilter(NationalityListParam param)
        {
            var expression = ExtensionLinq.True<NationalityEntity>();
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