using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
using Mortgage.Ecosystem.DataAccess.Layer.Extensions;
using Mortgage.Ecosystem.DataAccess.Layer.Helpers;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Operator;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Mortgage.Ecosystem.DataAccess.Layer.Repositories
{
    public class ChecklistRepository : DataRepository, IChecklistRepository
    {
        #region Retrieve data
        public async Task<List<ChecklistEntity>> GetList(ChecklistListParam param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<ChecklistEntity>> GetPageList(ChecklistListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<ChecklistEntity> GetEntity(int id)
        {
            return await BaseRepository().FindEntity<ChecklistEntity>(id);
        }


        #endregion

        #region Submit data
        public async Task SaveForm(ChecklistEntity entity)
        {
            if (entity.Id.IsNullOrZero())
            {
                await entity.Create();
                await BaseRepository().Insert<ChecklistEntity>(entity);
            }
            else
            {
                await BaseRepository().Update<ChecklistEntity>(entity);
            }
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await BaseRepository().Delete<ChecklistEntity>(idArr);
        }
        #endregion

        #region Private method
        private Expression<Func<ChecklistEntity, bool>> ListFilter(ChecklistListParam param)
        {
            var expression = ExtensionLinq.True<ChecklistEntity>();
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.Checklist))
                {
                    expression = expression.And(second: t => t.Checklist.Contains(param.Checklist));
                }
            }
            return expression;
        }
        #endregion
    }
}