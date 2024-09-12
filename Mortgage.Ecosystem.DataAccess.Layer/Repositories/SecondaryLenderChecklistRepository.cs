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
    public class SecondaryLenderChecklistRepository : DataRepository, ISecondaryLenderChecklistRepository
    {

        #region Submit data
        public async Task SaveForm(SecondaryLenderChecklistEntity entity)
        {
            if (entity.Id.IsNullOrZero())
            {
                await entity.Create();
                await BaseRepository().Insert<SecondaryLenderChecklistEntity>(entity);
            }
            else
            {
                await BaseRepository().Update<SecondaryLenderChecklistEntity>(entity);
            }
        }


        public async Task SaveForms(List<SecondaryLenderChecklistEntity> entities)
        {
            // Start the database transaction
            var db = await BaseRepository().BeginTrans();

            try
            {
                // Loop through each entity in the list
                foreach (var entity in entities)
                {
                    if (entity.Id.IsNullOrZero())
                    {
                        // Handle creation for new entities
                        await entity.Create();
                        await BaseRepository().Insert<SecondaryLenderChecklistEntity>(entity);
                    }
                    else
                    {
                        // Handle modification for existing entities
                        await entity.Modify();
                        await BaseRepository().Update<SecondaryLenderChecklistEntity>(entity);
                    }


                    await db.Insert(entity);
                    await db.CommitTrans();
                }
            }
            catch (Exception ex)
            {
                // Rollback the transaction if any error occurs
                await db.RollbackTrans();
                throw new Exception("Error occurred while saving forms", ex);
            }
        }

        public async Task<SecondaryLenderChecklistEntity> GetEntity(string Nhf)
        {
            return await BaseRepository().FindEntity<SecondaryLenderChecklistEntity>(x => x.EmployeeNhfNumber == Nhf);
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await BaseRepository().Delete<SecondaryLenderChecklistEntity>(idArr);
        }
        #endregion

    }
}