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
    public class NmrcCategoryRepository : DataRepository, INmrcCategoryRepository
    {
        #region Retrieve data
        public async Task<List<NmrcCategoryEntity>> GetList(NmrcCategoryListParam param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<NmrcCategoryEntity>> GetPageList(NmrcCategoryListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<NmrcCategoryEntity> GetEntityByCategoryId(long Id)
        {
            return await BaseRepository().FindEntity<NmrcCategoryEntity>(x => x.Id == Id);
        }

        public async Task<NmrcCategoryEntity> GetEntity(int id)
        {
            try
            {
                return await BaseRepository().FindEntity<NmrcCategoryEntity>(id);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        #endregion

        #region Submit data
        public async Task SaveForm(NmrcCategoryEntity entity)
        {
            if (entity.Id.IsNullOrZero())
            {
                await entity.Create();
                await BaseRepository().Insert<NmrcCategoryEntity>(entity);
            }
            else
            {
                await BaseRepository().Update<NmrcCategoryEntity>(entity);
            }
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await BaseRepository().Delete<NmrcCategoryEntity>(idArr);
        }
        #endregion

        #region Private method
        private Expression<Func<NmrcCategoryEntity, bool>> ListFilter(NmrcCategoryListParam param)
        {
            var expression = ExtensionLinq.True<NmrcCategoryEntity>();
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