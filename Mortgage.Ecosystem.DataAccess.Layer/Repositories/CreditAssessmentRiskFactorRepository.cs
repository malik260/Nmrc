using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
using Mortgage.Ecosystem.DataAccess.Layer.Extensions;
using Mortgage.Ecosystem.DataAccess.Layer.Helpers;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Operator;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;
using System.Linq.Expressions;
using static Google.Protobuf.WellKnownTypes.Field.Types;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Runtime.Intrinsics.X86;

namespace Mortgage.Ecosystem.DataAccess.Layer.Repositories
{
    public class CreditAssessmentRiskFactorRepository : DataRepository, ICreditAssessmentRiskFactorRepository
    {
        #region Retrieve data
        public async Task<List<CreditAssessmentRiskFactorEntity>> GetList(string productcode)
        {
            var expression = ListFilter(productcode);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<CreditAssessmentRiskFactorEntity>> GetPageList(CreditAssessmentRiskFactorListParam param, Pagination pagination)
        {
            var expression = ListFilters(param);
            var list = await BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        private Expression<Func<CreditAssessmentRiskFactorEntity, bool>> ListFilters(CreditAssessmentRiskFactorListParam param)
        {
            var expression = ExtensionLinq.True<CreditAssessmentRiskFactorEntity>();
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.ProductCode))
                {
                    expression = expression.And(second: t => t.ProductCode.Contains(param.ProductCode));
                }
            }
            return expression;
        }

        //public async Task<List<CreditAssessmentRiskFactor>> GetPageList(CreditAssessmentRiskFactorListParam param, Pagination pagination)
        //{
        //    var expression = ListFilter(param);
        //    var list = await BaseRepository().FindList(expression, pagination);
        //    return list.ToList();
        //}

        public async Task<CreditAssessmentRiskFactorEntity> GetEntities(int id)
        {
            return await BaseRepository().FindEntity<CreditAssessmentRiskFactorEntity>(id)
;
        }

        public async Task<CreditAssessmentRiskFactorEntity> GetEntity(long id)
        {
            return await BaseRepository().FindEntity<CreditAssessmentRiskFactorEntity>(id)
;
        }

        public async Task<CreditAssessmentRiskFactorEntity> GetEntitiesByRiskId(int id)
        {

            return await BaseRepository().FindEntity<CreditAssessmentRiskFactorEntity>(x => x.RiskFactorId == id)
;
        }
        #endregion




        #region Submit data
        public async Task SaveForm(CreditAssessmentRiskFactorEntity entity)
        {
            var db = await BaseRepository().BeginTrans();
            
            try
            {
                if (entity.RiskFactorId.IsNullOrZero())
                {
                    await entity.Create();
                    await db.Insert(entity);
                }
                else
                {
                    await entity.Modify();
                    await db.Update(entity);
                }
                await db.CommitTrans();
            }
            catch
            {
                await db.RollbackTrans();
                throw;
            }
        }










            public async Task DeleteForm(string ids)
    {
        long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
        await BaseRepository().Delete<CreditAssessmentRiskFactorEntity>(idArr);
    }

    #endregion
    #region Private method
        private Expression<Func<CreditAssessmentRiskFactorEntity, bool>> ListFilter(string productcode)
    {
        var expression = ExtensionLinq.True<CreditAssessmentRiskFactorEntity>();
        if (productcode != null)
        {
            if (!string.IsNullOrEmpty(productcode))
            {
                expression = expression.And(second: t => t.ProductCode == productcode);
            }
        }
        return expression;
    }
    #endregion
    }
}