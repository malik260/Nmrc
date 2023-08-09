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
    public class CreditScoreRepository : DataRepository, ICreditScoreRepository
    {
        #region Retrieve data
        public async Task<List<CreditScoreEntity>> GetList(CreditScoreListParam param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<CreditScoreEntity>> GetPageList(CreditScoreListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<CreditScoreEntity> GetEntity(long id)
        {
            return await BaseRepository().FindEntity<CreditScoreEntity>(id);
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
        public bool ExistCompany(CreditScoreEntity entity)
        {
            var expression = ExtensionLinq.True<CreditScoreEntity>();
            expression = expression.And(t => t.BaseIsDelete == 0);
            if (entity.Id.IsNullOrZero())
            {
                expression = expression.And(t => t.CreditType == entity.CreditType);
            }
            else
            {
                expression = expression.And(t => t.CreditType == entity.CreditType && t.Id != entity.Id);
            }
            return BaseRepository().IQueryable(expression).Count() > 0 ? true : false;
        }
        #endregion

        #region Submit data
        public async Task SaveForm(CreditScoreEntity entity)
        {
            if (entity.Id.IsNullOrZero())
            {
                await entity.Create();
                await BaseRepository().Insert<CreditScoreEntity>(entity);
            }
            else
            {
                await entity.Modify();
                await BaseRepository().Update<CreditScoreEntity>(entity);
            }
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await BaseRepository().Delete<RiskAssessmentSetupEntity>(idArr);
        }
        #endregion

        #region Private method
        private Expression<Func<CreditScoreEntity, bool>> ListFilter(CreditScoreListParam param)
        {
            var expression = ExtensionLinq.True<CreditScoreEntity>();
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.CreditType))
                {
                    expression = expression.And(t => t.CreditType.Contains(param.CreditType));
                }
            }
            return expression;
        }
        #endregion
    }
}