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
    public class LenderRepository : DataRepository, ILenderRepository
    {
        #region Retrieve data
        public async Task<List<LenderSetupEntity>> GetList(LenderListParam param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

       

        public async Task<List<LenderSetupEntity>> GetPageList(LenderListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<LenderSetupEntity> GetEntity(long Id)
        {
            return await BaseRepository().FindEntity<LenderSetupEntity>(x => x.LenderCategory == Id);
        }

       
        public async Task<LenderSetupEntity> GetEntities(int id)
        {
            return await BaseRepository().FindEntity<LenderSetupEntity>(id);
        }

        public async Task<CreditTypeEntity> GetEntitybiId(int id)
        {
            return await BaseRepository().FindEntity<CreditTypeEntity>(x => x.Id == id);
        }


        #endregion

        #region Submit data
        public async Task SaveForm(LenderSetupEntity entity)
        {
            var db = await BaseRepository().BeginTrans();
            try
            {
                if (entity.Id.IsNullOrZero())
                {
                    await entity.Create();
                    await BaseRepository().Insert<LenderSetupEntity>(entity);
                }
                else
                {
                    await BaseRepository().Update<LenderSetupEntity>(entity);
                }

                await db.CommitTrans();
            }
            catch (Exception ex)
            {
                await db.RollbackTrans();
                throw;
            }
        }



        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await BaseRepository().Delete<LenderSetupEntity>(idArr);
        }
        #endregion

        #region Private method
        private Expression<Func<LenderSetupEntity, bool>> ListFilter(LenderListParam param)
        {
            var expression = ExtensionLinq.True<LenderSetupEntity>();
            if (param != null)
            {
                if (param.LenderId != 0)
                {
                    expression = expression.And(second: t => t.LenderCategory == param.LenderId);
                }
            }
            return expression;
        }
       
        #endregion
    }
}
