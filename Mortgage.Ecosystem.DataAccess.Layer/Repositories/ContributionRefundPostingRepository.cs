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
    public class ContributionRefundPostingRepository : DataRepository, IContributionRefundPostingRepository
    {
        #region Retrieve data
        public async Task<List<ContributionRefundPostingEntity>> GetList(ContributionRefundPostingListParam param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<ContributionRefundPostingEntity>> GetPageList(ContributionRefundPostingListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<ContributionRefundPostingEntity> GetEntity(long id)
        {
            return await BaseRepository().FindEntity<ContributionRefundPostingEntity>(id);
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
        public bool ExistCompany(ContributionRefundPostingEntity entity)
        {
            var expression = ExtensionLinq.True<ContributionRefundPostingEntity>();
            expression = expression.And(t => t.BaseIsDelete == 0);
            if (entity.Id.IsNullOrZero())
            {
                expression = expression.And(t => t.LedgerCr == entity.LedgerCr);
            }
            else
            {
                expression = expression.And(t => t.LedgerCr == entity.LedgerCr && t.Id != entity.Id);
            }
            return BaseRepository().IQueryable(expression).Count() > 0 ? true : false;
        }
        #endregion

        #region Submit data
        public async Task SaveForm(ContributionRefundPostingEntity entity)
        {
            if (entity.Id.IsNullOrZero())
            {
                await entity.Create();
                await BaseRepository().Insert<ContributionRefundPostingEntity>(entity);
            }
            else
            {
                await entity.Modify();
                await BaseRepository().Update<ContributionRefundPostingEntity>(entity);
            }
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await BaseRepository().Delete<ContributionRefundPostingEntity>(idArr);
        }
        #endregion

        #region Private method
        private Expression<Func<ContributionRefundPostingEntity, bool>> ListFilter(ContributionRefundPostingListParam param)
        {
            var expression = ExtensionLinq.True<ContributionRefundPostingEntity>();
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.LedgerCr))
                {
                    expression = expression.And(t => t.LedgerCr.Contains(param.LedgerCr));
                }
            }
            return expression;
        }
        #endregion
    }
}