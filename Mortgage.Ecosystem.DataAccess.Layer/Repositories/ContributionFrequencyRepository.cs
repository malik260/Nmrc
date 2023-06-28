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
    public class ContributionFrequencyRepository : DataRepository, IContributionFrequencyRepository
    {
        #region Retrieve data
        public async Task<List<ContributionFrequencyEntity>> GetList(ContributionFrequencyListParam param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<ContributionFrequencyEntity>> GetPageList(ContributionFrequencyListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<ContributionFrequencyEntity> GetEntity(long id)
        {
            return await BaseRepository().FindEntity<ContributionFrequencyEntity>(id);
        }
        #endregion

        #region Submit data
        public async Task SaveForm(ContributionFrequencyEntity entity)
        {
            if (entity.Id.IsNullOrZero())
            {
                await entity.Create();
                await BaseRepository().Insert<ContributionFrequencyEntity>(entity);
            }
            else
            {
                await BaseRepository().Update<ContributionFrequencyEntity>(entity);
            }
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await BaseRepository().Delete<ContributionFrequencyEntity>(idArr);
        }
        #endregion

        #region Private method
        private Expression<Func<ContributionFrequencyEntity, bool>> ListFilter(ContributionFrequencyListParam param)
        {
            var expression = ExtensionLinq.True<ContributionFrequencyEntity>();
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