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
    public class BankRepository : DataRepository, IBankRepository
    {
        #region Retrieve data

        public async Task<List<BankEntity>> GetList(BankListParam param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<BankEntity>> GetPageList(BankListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<BankEntity> GetEntity(long id)
        {
            return await BaseRepository().FindEntity<BankEntity>(id);
        }

        public bool ExistJob(BankEntity entity)
        {
            var expression = ExtensionLinq.True<BankEntity>();
            expression = expression.And(t => t.BaseIsDelete == 0);
            if (entity.Id.IsNullOrZero())
            {
                expression = expression.And(t => t.Code == entity.Code && t.Name == entity.Name);
            }
            else
            {
                expression = expression.And(t => t.Code == entity.Code && t.Name == entity.Name && t.Id != entity.Id);
            }
            return BaseRepository().IQueryable(expression).Count() > 0 ? true : false;
        }

        #endregion Retrieve data

        #region Submit data

        public async Task SaveForm(BankEntity entity)
        {
            if (entity.Id.IsNullOrZero())
            {
                await entity.Create();
                await BaseRepository().Insert<BankEntity>(entity);
            }
            else
            {
                await entity.Modify();
                await BaseRepository().Update<BankEntity>(entity);
            }
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await BaseRepository().Delete<BankEntity>(idArr);
        }

        #endregion Submit data

        #region Private method

        private Expression<Func<BankEntity, bool>> ListFilter(BankListParam param)
        {
            var expression = ExtensionLinq.True<BankEntity>();
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.Code))
                {
                    expression = expression.And(t => t.Code.Contains(param.Code));
                }
            }
            return expression;
        }

        #endregion Private method
    }
}