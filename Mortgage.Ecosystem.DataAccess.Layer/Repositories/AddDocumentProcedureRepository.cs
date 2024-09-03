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
    public class AddDocumentProcedureRepository : DataRepository, IAddDocumentProcedureRepository
    {

        #region Submit data
        public async Task SaveForm(AddDocumentProcedureEntity entity)
        {

            if (entity.Id.IsNullOrZero())
            {
                await entity.Create();
                await BaseRepository().Insert<AddDocumentProcedureEntity>(entity);
            }
            else
            {
                await BaseRepository().Update<AddDocumentProcedureEntity>(entity);
            }
        }

        public async Task SaveForms(List<AddDocumentProcedureEntity> entity)
        {
            foreach (var item in entity)
            {
                if (item.Id.IsNullOrZero())
                {
                    await item.Create();
                    await BaseRepository().Insert<AddDocumentProcedureEntity>(item);
                }
                else
                {
                    await item.Modify();
                    await BaseRepository().Update<AddDocumentProcedureEntity>(item);
                }

            }


        }


        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await BaseRepository().Delete<AddDocumentProcedureEntity>(idArr);
        }
        #endregion

    }
}