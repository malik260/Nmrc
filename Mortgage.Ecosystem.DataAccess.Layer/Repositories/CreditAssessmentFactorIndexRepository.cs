﻿using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
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
    public class CreditAssessmentFactorIndexRepository : DataRepository, ICreditAssessmentFactorIndexRepository
    {
        #region Retrieve data
        public async Task<List<CreditAssessmentFactorIndexEntity>> GetList(int riskFactorId)
        {
            var expression = ListFilter(riskFactorId);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<CreditAssessmentFactorIndexEntity> GetEntity(long id)
        {
            return await BaseRepository().FindEntity<CreditAssessmentFactorIndexEntity>(id)
;
        }
        #endregion

        #region Submit data
        public async Task SaveForm(CreditAssessmentFactorIndexEntity entity)
        {
            var db = await BaseRepository().BeginTrans();

            try
            {
                if (entity.Id.IsNullOrZero())
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
            await BaseRepository().Delete<CreditAssessmentFactorIndexEntity>(idArr);
        }
        #endregion

        #region Private method
        private Expression<Func<CreditAssessmentFactorIndexEntity, bool>> ListFilter(int riskFactorId)
        {
           var expression = ExtensionLinq.True<CreditAssessmentFactorIndexEntity>();
            if (riskFactorId != null)
            {
                if (riskFactorId > 0)
                {
                    expression = expression.And(t => t.RiskFactorId == riskFactorId);
                }


            }
            return expression;
        }
        #endregion
    }
}