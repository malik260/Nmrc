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
    public class CreditAssessmentIndexTitleRepository : DataRepository, ICreditAssessmentIndexTitleRepository
    {
        #region Retrieve data
        public async Task<List<CreditAssessmentIndexTitleEntity>> GetList(int factorIndexId)
        {
            var expression = ListFilter(factorIndexId);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<CreditAssessmentIndexTitleEntity> GetEntity(long id)
        {
            return await BaseRepository().FindEntity<CreditAssessmentIndexTitleEntity>(id)
;
        }
        #endregion

        #region Submit data
        public async Task SaveForm(CreditAssessmentIndexTitleEntity entity)
        {
            if (entity.Id.IsNullOrZero())
            {
                await entity.Create();
                await BaseRepository().Insert<CreditAssessmentIndexTitleEntity>(entity);
            }
            else
            {
                await BaseRepository().Update<CreditAssessmentIndexTitleEntity>(entity);
            }
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await BaseRepository().Delete<CreditAssessmentIndexTitleEntity>(idArr);
        }
        #endregion

        #region Private method
        private Expression<Func<CreditAssessmentIndexTitleEntity, bool>> ListFilter(int factorIndexId)
        {
           var expression = ExtensionLinq.True<CreditAssessmentIndexTitleEntity>();
            if (factorIndexId != null)
            {
                if (factorIndexId > 0)
                {
                    expression = expression.And(t => t.FactorIndexId == factorIndexId);
                }


            }
            return expression;
        }
        #endregion
    }
}