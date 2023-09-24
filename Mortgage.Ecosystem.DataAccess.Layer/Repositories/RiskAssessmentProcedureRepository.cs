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
    public class RiskAssessmentProcedureRepository : DataRepository, IRiskAssessmentProcedureRepository
    {

        #region Submit data
        public async Task SaveForm(RiskAssessmentProcedureEntity entity)
        {
            try
            {
                if (entity.Id.IsNullOrZero())
                {
                    await entity.Create();
                    await BaseRepository().Insert<RiskAssessmentProcedureEntity>(entity);
                }
                else
                {
                    await BaseRepository().Update<RiskAssessmentProcedureEntity>(entity);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await BaseRepository().Delete<ChecklistEntity>(idArr);
        }
        #endregion

    }
}