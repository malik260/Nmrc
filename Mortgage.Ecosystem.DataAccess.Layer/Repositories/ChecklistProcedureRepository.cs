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
    public class ChecklistProcedureRepository : DataRepository, IChecklistProcedureRepository
    {

        #region Submit data
        public async Task SaveForm(ChecklistProcedureEntity entity)
        {
            if (entity.Id.IsNullOrZero())
            {
                await entity.Create();
                await BaseRepository().Insert<ChecklistProcedureEntity>(entity);
            }
            else
            {
                await BaseRepository().Update<ChecklistProcedureEntity>(entity);
            }
        }

        public async Task SaveForms(List<ChecklistProcedureEntity> entity)
        {
            foreach (var item in entity)
            {
                if (item.Id.IsNullOrZero())
                {
                    await item.Create();
                    await BaseRepository().Insert<ChecklistProcedureEntity>(item);
                }
                else
                {
                    await item.Modify();
                    await BaseRepository().Update<ChecklistProcedureEntity>(item);
                }

            }


        }
        public async Task<ChecklistProcedureEntity> GetEntity(string Nhf)
        {
            return await BaseRepository().FindEntity<ChecklistProcedureEntity>(x => x.NHFNo == Nhf);
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await BaseRepository().Delete<ChecklistEntity>(idArr);
        }
        #endregion

    }
}