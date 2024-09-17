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
    public class SecondaryLenderChecklistProcedureRepository : DataRepository, ISecondaryLenderChecklistProcedureRepository
    {
        #region Get data
        public async Task<SecondaryLenderChecklistProcedureEntity> GetEntity(string Nhf)
        {
            return await BaseRepository().FindEntity<SecondaryLenderChecklistProcedureEntity>(x => x.EmployeeNhfNumber == Nhf);
        }
        #endregion

        #region Submit data
        public async Task SaveForm(SecondaryLenderChecklistProcedureEntity entity)
        {
            if (entity.Id.IsNullOrZero())
            {
                await entity.Create();
                await BaseRepository().Insert<SecondaryLenderChecklistProcedureEntity>(entity);
            }
            else
            {
                await BaseRepository().Update<SecondaryLenderChecklistProcedureEntity>(entity);
            }
        }

        public async Task SaveForms(List<SecondaryLenderChecklistProcedureEntity> entity)
        {
            foreach (var item in entity)
            {
                if (item.Id.IsNullOrZero())
                {
                    await item.Create();
                    await BaseRepository().Insert<SecondaryLenderChecklistProcedureEntity>(item);
                }
                else
                {
                    await item.Modify();
                    await BaseRepository().Update<SecondaryLenderChecklistProcedureEntity>(item);
                }

            }


        }
        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await BaseRepository().Delete<SecondaryLenderChecklistProcedureEntity>(idArr);
        }
        #endregion

    }
}