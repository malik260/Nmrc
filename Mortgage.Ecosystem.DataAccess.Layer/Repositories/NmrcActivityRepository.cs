using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
using Mortgage.Ecosystem.DataAccess.Layer.Extensions;
using Mortgage.Ecosystem.DataAccess.Layer.Helpers;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Operator;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Mortgage.Ecosystem.DataAccess.Layer.Repositories
{
    public class NmrcActivityRepository: DataRepository, INmrcActivityRepository
    {
        #region Retrieve data
        public async Task<List<RefinancingEntity>> GetList(RefinancingEntity param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }
          public async Task<List<RefinancingEntity>> GetListByBatch(RefinancingEntity param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<RefinancingEntity>> GetPageList(RefinancingEntity param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

      
        public async Task<RefinancingEntity> GetEntity(long lenderid)
        {
            try
            {
                return await BaseRepository().FindEntity<RefinancingEntity>(x=> x.LenderID == lenderid);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        #endregion

        #region Submit data
        public async Task SaveForm(RefinancingEntity entity)
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
            await BaseRepository().Delete<NmrcCategoryEntity>(idArr);
        }
        #endregion

        #region Private method
        private Expression<Func<RefinancingEntity, bool>> ListFilter(RefinancingEntity param)
        {
            var expression = ExtensionLinq.True<RefinancingEntity>();
            if (param != null)
            {
                if (param.LenderID != 0)
                {
                    expression = expression.And(second: t => t.LenderID == param.LenderID);
                }
                if (!string.IsNullOrEmpty(param.RefinanceNumber))
                {
                    expression = expression.And(second: t => t.RefinanceNumber == param.RefinanceNumber);
                }
            }
            return expression;
        }
        #endregion


    }
}
