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
    public class FinanceCounterpartyTransactionRepository : DataRepository, IFinanceCounterpartyTransactionRepository
    {
        #region Retrieve data
        public async Task<List<FinanceCounterpartyTransactionEntity>> GetList(FinanceCounterpartyTransactionListParam param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<FinanceCounterpartyTransactionEntity>> GetPageList(FinanceCounterpartyTransactionListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<FinanceCounterpartyTransactionEntity> GetEntity(long id)
        {
            return await BaseRepository().FindEntity<FinanceCounterpartyTransactionEntity>(id);
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
        public bool ExistCompany(FinanceCounterpartyTransactionEntity entity)
        {
            var expression = ExtensionLinq.True<FinanceCounterpartyTransactionEntity>();
            expression = expression.And(t => t.BaseIsDelete == 0);
            if (entity.Id.IsNullOrZero())
            {
                expression = expression.And(t => t.TransactionId == entity.TransactionId);
            }
            else
            {
                expression = expression.And(t => t.TransactionId == entity.TransactionId && t.Id != entity.Id);
            }
            return BaseRepository().IQueryable(expression).Count() > 0 ? true : false;
        }
        #endregion

        #region Submit data
        public async Task SaveForm(FinanceCounterpartyTransactionEntity entity)
        {
            if (entity.Id.IsNullOrZero())
            {
                await entity.Create();
                await BaseRepository().Insert<FinanceCounterpartyTransactionEntity>(entity);
            }
            else
            {
                await entity.Modify();
                await BaseRepository().Update<FinanceCounterpartyTransactionEntity>(entity);
            }
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await BaseRepository().Delete<FinanceCounterpartyTransactionEntity>(idArr);
        }
        #endregion

        #region Private method
        private Expression<Func<FinanceCounterpartyTransactionEntity, bool>> ListFilter(FinanceCounterpartyTransactionListParam param)
        {
            var expression = ExtensionLinq.True<FinanceCounterpartyTransactionEntity>();
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.Ref))
                {
                    expression = expression.And(t => t.Ref.Contains(param.Ref));
                }
            }
            return expression;
        }
        #endregion
    }
}