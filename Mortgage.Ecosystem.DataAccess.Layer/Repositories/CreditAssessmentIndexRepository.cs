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
    public class CreditAssessmentIndexRepository : DataRepository, ICreditAssessmentIndexRepository
    {
        #region Retrieve data
        public async Task<List<CreditAssessmentIndexEntity>> GetList(int indexTitleId)
        {
            var expression = ListFilter(indexTitleId);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<CreditAssessmentIndexEntity> GetEntity(long id)
        {
            return await BaseRepository().FindEntity<CreditAssessmentIndexEntity>(id)
;
        }
        #endregion

        #region Submit data
        public async Task SaveForm(CreditAssessmentIndexEntity entity)
        {
            if (entity.Indexid.IsNullOrZero())
            {
                await entity.Create();
                await BaseRepository().Insert<CreditAssessmentIndexEntity>(entity);
            }
            else
            {
                await BaseRepository().Update<CreditAssessmentIndexEntity>(entity);
            }
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await BaseRepository().Delete<CreditAssessmentIndexEntity>(idArr);
        }
        #endregion

        #region Private method
        private Expression<Func<CreditAssessmentIndexEntity, bool>> ListFilter(int indexTitleId)
        {
           var expression = ExtensionLinq.True<CreditAssessmentIndexEntity>();
            if (indexTitleId != null)
            {
                if (indexTitleId > 0)
                {
                    expression = expression.And(t => t.Indexid == indexTitleId);
                }


            }
            return expression;
        }
        #endregion
    }
}