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
    public class CreditTypeRepository : DataRepository, ICreditTypeRepository
    {
        #region Retrieve data
        public async Task<List<CreditTypeEntity>> GetList(CreditTypeListParam param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }



        public async Task<List<CreditTypeEntity>> GetPageList(CreditTypeListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<CreditTypeEntity> GetEntity(string code)
        {
            return await BaseRepository().FindEntity<CreditTypeEntity>(x => x.Code == code);
        }
        #endregion

        #region Submit data

        public async Task SaveForm(CreditTypeEntity entity)
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
            await BaseRepository().Delete<CreditTypeEntity>(idArr);
        }
        #endregion

        #region Private method
        private Expression<Func<CreditTypeEntity, bool>> ListFilter(CreditTypeListParam param)
        {
            var expression = ExtensionLinq.True<CreditTypeEntity>();
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.Name))
                {
                    expression = expression.And(second: t => t.Name.Contains(param.Name));
                }
            }
            return expression;
        }
        #endregion
    }
}