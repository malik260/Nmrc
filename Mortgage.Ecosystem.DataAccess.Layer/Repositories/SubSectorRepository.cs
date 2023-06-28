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
    public class SubSectorRepository : DataRepository, ISubSectorRepository
    {
        #region Retrieve data
        public async Task<List<SubSectorEntity>> GetList(SubSectorListParam param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<SubSectorEntity>> GetPageList(SubSectorListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<SubSectorEntity> GetEntity(long id)
        {
            return await BaseRepository().FindEntity<SubSectorEntity>(id);
        }
        #endregion

        #region Submit data
        public async Task SaveForm(SubSectorEntity entity)
        {
            if (entity.Id.IsNullOrZero())
            {
                await entity.Create();
                await BaseRepository().Insert<SubSectorEntity>(entity);
            }
            else
            {
                await BaseRepository().Update<SubSectorEntity>(entity);
            }
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await BaseRepository().Delete<SubSectorEntity>(idArr);
        }
        #endregion

        #region Private method
        private Expression<Func<SubSectorEntity, bool>> ListFilter(SubSectorListParam param)
        {
            var expression = ExtensionLinq.True<SubSectorEntity>();
            if (param != null)
            {
                if (!param.Sector.IsNullOrZero())
                {
                    expression = expression.And(second: t => t.Sector == param.Sector);
                }
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