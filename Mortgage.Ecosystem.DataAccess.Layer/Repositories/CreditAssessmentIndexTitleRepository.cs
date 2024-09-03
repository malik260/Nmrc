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

        public async Task<List<CreditAssessmentIndexTitleEntity>> GetListbyProduct(string productcode)
        {
            var expression = ListFilter2(productcode);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<CreditAssessmentIndexTitleEntity> GetEntity(long id)
        {
            return await BaseRepository().FindEntity<CreditAssessmentIndexTitleEntity>(id)
;
        }

        public async Task<CreditAssessmentIndexTitleEntity> GetEntities(int id)
        {
            return await BaseRepository().FindEntity<CreditAssessmentIndexTitleEntity>(id)
;
        }

        public async Task<CreditAssessmentIndexTitleEntity> GetEntityByIndextitleId(int id)
        {
            return await BaseRepository().FindEntity<CreditAssessmentIndexTitleEntity>(x => x.IndexTitleId == id)
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


        public async Task<List<CreditAssessmentIndexTitleEntity>> GetPageList(CreditAssessmentIndexTitleListParam param, Pagination pagination)
        {
            var expression = ListFilters(param);
            var list = await BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        private Expression<Func<CreditAssessmentIndexTitleEntity, bool>> ListFilters(CreditAssessmentIndexTitleListParam param)
        {
            var expression = ExtensionLinq.True<CreditAssessmentIndexTitleEntity>();
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.ProductCode))
                {
                    expression = expression.And(second: t => t.ProductCode.Contains(param.ProductCode));
                }
            }
            return expression;
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

        private Expression<Func<CreditAssessmentIndexTitleEntity, bool>> ListFilter2(string productcode)
        {
            var expression = ExtensionLinq.True<CreditAssessmentIndexTitleEntity>();
            if (productcode != null)
            {
                if (productcode != null)
                {
                    expression = expression.And(t => t.ProductCode == productcode);
                }


            }
            return expression;
        }
        #endregion
    }
}