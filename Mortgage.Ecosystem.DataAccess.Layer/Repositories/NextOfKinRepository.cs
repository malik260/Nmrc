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
    public class NextOfKinRepository : DataRepository, INextOfKinRepository
    {
        #region Retrieve data
        public async Task<List<NextOfKinEntity>> GetList(NextOfKinListParam param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<NextOfKinEntity>> GetPageList(NextOfKinListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<NextOfKinEntity> GetEntity(long id)
        {
            return await BaseRepository().FindEntity<NextOfKinEntity>(i => i.Employee == id);
        }
        public async Task<NextOfKinEntity> GetEntityByEmployeeId(long employeeId)
        {
            return await BaseRepository().FindEntity<NextOfKinEntity>(x => x.Employee == employeeId);
        }

        #endregion

        #region Submit data
        public async Task SaveForm(NextOfKinEntity entity)
        {
            if (entity.Id.IsNullOrZero())
            {
                await entity.Create();
                await BaseRepository().Insert<NextOfKinEntity>(entity);
            }
            else
            {
                await BaseRepository().Update<NextOfKinEntity>(entity);
            }
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await BaseRepository().Delete<NextOfKinEntity>(idArr);
        }
        #endregion

        #region Private method
        private Expression<Func<NextOfKinEntity, bool>> ListFilter(NextOfKinListParam param)
        {
            var expression = ExtensionLinq.True<NextOfKinEntity>();
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.Employee.ToString()) && (!string.IsNullOrEmpty(param.FirstName) || !string.IsNullOrEmpty(param.LastName)))
                {
                    expression = expression.And(second: t => t.Employee == param.Employee && t.LastName.Contains(param.LastName) && t.FirstName.Contains(param.FirstName));
                }
            }
            return expression;
        }
        #endregion
    }
}