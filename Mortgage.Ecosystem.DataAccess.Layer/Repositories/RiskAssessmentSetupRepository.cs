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
    public class RiskAssessmentSetupRepository : DataRepository, IRiskAssessmentSetupRepository
    {
        #region Retrieve data
        public async Task<List<RiskAssessmentSetupEntity>> GetList(RiskAssessmentSetupListParam param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<RiskAssessmentSetupEntity>> GetPageList(RiskAssessmentSetupListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<RiskAssessmentSetupEntity> GetEntity(long id)
        {
            return await BaseRepository().FindEntity<RiskAssessmentSetupEntity>(id);
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
        public bool ExistCompany(RiskAssessmentSetupEntity entity)
        {
            var expression = ExtensionLinq.True<RiskAssessmentSetupEntity>();
            expression = expression.And(t => t.BaseIsDelete == 0);
            if (entity.Id.IsNullOrZero())
            {
                expression = expression.And(t => t.AssessmentFactors == entity.AssessmentFactors);
            }
            else
            {
                expression = expression.And(t => t.AssessmentFactors == entity.AssessmentFactors && t.Id != entity.Id);
            }
            return BaseRepository().IQueryable(expression).Count() > 0 ? true : false;
        }
        #endregion

        #region Submit data
        public async Task SaveForm(RiskAssessmentSetupEntity entity)
        {
            if (entity.Id.IsNullOrZero())
            {
                await entity.Create();
                await BaseRepository().Insert<RiskAssessmentSetupEntity>(entity);
            }
            else
            {
                await entity.Modify();
                await BaseRepository().Update<RiskAssessmentSetupEntity>(entity);
            }
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await BaseRepository().Delete<RiskAssessmentSetupEntity>(idArr);
        }
        #endregion

        #region Private method
        private Expression<Func<RiskAssessmentSetupEntity, bool>> ListFilter(RiskAssessmentSetupListParam param)
        {
            var expression = ExtensionLinq.True<RiskAssessmentSetupEntity>();
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.AssessmentFactors))
                {
                    expression = expression.And(t => t.AssessmentFactors.Contains(param.AssessmentFactors));
                }
            }
            return expression;
        }
        #endregion
    }
}