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
    public class FinanceTransactionRepository : DataRepository, IFinanceTransactionRepository
    {
        #region Retrieve data
        public async Task<List<FinanceTransactionEntity>> GetList(FinanceTransactionListParam param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<FinanceTransactionEntity>> GetPageList(FinanceTransactionListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<FinanceTransactionEntity> GetEntity(long id)
        {
            return await BaseRepository().FindEntity<FinanceTransactionEntity>(id);
        }

        public async Task<int> GetMaxSort()
        {
            object result = await BaseRepository().FindObject("SELECT MAX(Sort) FROM tbl_Conpany");
            int sort = result.ToInt();
            sort++;
            return sort;
        }

        // Whether the company name exists
        // <param name="entity"></param>
        // <returns></returns>
        public bool ExistCompany(FinanceTransactionEntity entity)
        {
            var expression = ExtensionLinq.True<FinanceTransactionEntity>();
            expression = expression.And(t => t.BaseIsDelete == 0);
            if (entity.Id.IsNullOrZero())
            {
                expression = expression.And(t => t.AccountId == entity.AccountId);
            }
            else
            {
                expression = expression.And(t => t.AccountId == entity.AccountId && t.Id != entity.Id);
            }
            return BaseRepository().IQueryable(expression).Count() > 0 ? true : false;
        }
        #endregion

        #region Submit data
        public async Task SaveForm(FinanceTransactionEntity entity)
        {
            if (entity.Id.IsNullOrZero())
            {
                await entity.Create();
                await BaseRepository().Insert<FinanceTransactionEntity>(entity);
            }
            else
            {
                await entity.Modify();
                await BaseRepository().Update<FinanceTransactionEntity>(entity);
            }
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await BaseRepository().Delete<FinanceTransactionEntity>(idArr);
        }
        #endregion

        #region Private method
        private Expression<Func<FinanceTransactionEntity, bool>> ListFilter(FinanceTransactionListParam param)
        {
            var expression = ExtensionLinq.True<FinanceTransactionEntity>();
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.AccountId))
                {
                    expression = expression.And(t => t.AccountId.Contains(param.AccountId));
                }
            }
            return expression;
        }
        #endregion
    }
}