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
    public class StatementOfAccountRepository : DataRepository, IStatementOfAccountRepository
    {
        #region Retrieve data
        public async Task<List<FinanceCounterpartyTransactionEntity>> GetList(StatementOfAccountListParam param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<FinanceCounterpartyTransactionEntity>> GetPageList(StatementOfAccountListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<StatementOfAccountEntity> GetEntity(long id)
        {
            return await BaseRepository().FindEntity<StatementOfAccountEntity>(id);
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
        public bool ExistCompany(StatementOfAccountEntity entity)
        {
            var expression = ExtensionLinq.True<StatementOfAccountEntity>();
            expression = expression.And(t => t.BaseIsDelete == 0);
            if (entity.Id.IsNullOrZero())
            {
                expression = expression.And(t => t.FirstName == entity.FirstName);
            }
            else
            {
                expression = expression.And(t => t.FirstName == entity.FirstName && t.Id != entity.Id);
            }
            return BaseRepository().IQueryable(expression).Count() > 0 ? true : false;
        }
        #endregion

        #region Submit data
        public async Task SaveForm(StatementOfAccountEntity entity)
        {
            var db = await BaseRepository().BeginTrans();
            try
            {
                if (entity.Id.IsNullOrZero())
                {
                    await entity.Create();
                    await BaseRepository().Insert<StatementOfAccountEntity>(entity);
                }
                else
                {
                    await entity.Modify();
                    await BaseRepository().Update<StatementOfAccountEntity>(entity);
                }
                await db.CommitTrans();
            }
            catch (Exception)
            {
                await db.RollbackTrans();

                throw;
            }
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await BaseRepository().Delete<StatementOfAccountEntity>(idArr);
        }
        #endregion

        #region Private method
        private Expression<Func<FinanceCounterpartyTransactionEntity, bool>> ListFilter(StatementOfAccountListParam param)
        {
            var expression = ExtensionLinq.True<FinanceCounterpartyTransactionEntity>();
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.NHFNumber))
                {
                    expression = expression.And(t => t.Ref == param.NHFNumber && t.PostDate >= Convert.ToDateTime(param.StartDate) && t.PostDate <= Convert.ToDateTime(param.EndDate) && t.Approved == 1);
                }
            }
            return expression;
        }
        #endregion
    }
}