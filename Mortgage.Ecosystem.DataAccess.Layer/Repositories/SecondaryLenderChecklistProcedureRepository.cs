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

        public async Task<List<SecondaryLenderChecklistProcedureEntity>> GetList(SecondaryLenderChecklistProcedureEntity param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }


        public async Task<SecondaryLenderChecklistProcedureEntity> GetEntity(string Nhf)
        {
            return await BaseRepository().FindEntity<SecondaryLenderChecklistProcedureEntity>(x => x.EmployeeNhfNumber == Nhf);
        }

        public async Task<SecondaryLenderChecklistProcedureEntity> GetEntityForPmb(long pmbid)
        {
            return await BaseRepository().FindEntity<SecondaryLenderChecklistProcedureEntity>(x => x.PmbId == pmbid);
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
            try
            {
                var db = await BaseRepository().BeginTrans();

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

                await db.CommitTrans();


            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private Expression<Func<SecondaryLenderChecklistProcedureEntity, bool>> ListFilter(SecondaryLenderChecklistProcedureEntity param)
        {
            var expression = ExtensionLinq.True<SecondaryLenderChecklistProcedureEntity>();
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.EmployeeNhfNumber))
                {
                    expression = expression.And(t => t.EmployeeNhfNumber  == param.EmployeeNhfNumber);
                }
                if (param.PmbId != 0)
                {
                    expression = expression.And(t => t.PmbId == param.PmbId);
                }
            }
            return expression;
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await BaseRepository().Delete<SecondaryLenderChecklistProcedureEntity>(idArr);
        }
        #endregion

    }
}