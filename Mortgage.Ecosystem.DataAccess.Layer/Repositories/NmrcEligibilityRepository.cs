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
    public class NmrcEligibilityRepository : DataRepository, INmrcEligibilityRepository
    {
        #region Retrieve data
        public async Task<List<NmrcEligibilityEntity>> GetList(NmrcEligibilityListParam param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<NmrcEligibilityEntity>> GetListbyCategory(string category)
        {
            var expression = ListFilterbyItem(category);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<NmrcEligibilityEntity>> GetPageList(NmrcEligibilityListParam param, Pagination pagination)
        {
            var expression = ListFilters(param);
            var list = await BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

      

        public async Task<NmrcEligibilityEntity> GetEntities(int id)
        {
            return await BaseRepository().FindEntity<NmrcEligibilityEntity>(id)
;
        }

        public async Task<NmrcEligibilityEntity> GetEntitiesbyCategoryid(int id)
        {
            return await BaseRepository().FindEntity<NmrcEligibilityEntity>(x => x.Category == id)
;
        }

        public async Task<NmrcEligibilityEntity> GetEntitybyName(string item)
        {
            return await BaseRepository().FindEntity<NmrcEligibilityEntity>(x => x.Item.Contains(item));
        }
        public async Task<NmrcEligibilityEntity> GetEntity(int id)
        {
            return await BaseRepository().FindEntity<NmrcEligibilityEntity>(id)
;
        }
        #endregion

        #region Submit data
        public async Task SaveForm(NmrcEligibilityEntity entity)
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
            await BaseRepository().Delete<NmrcEligibilityEntity>(idArr);
        }
        #endregion

        #region Private method
        private Expression<Func<NmrcEligibilityEntity, bool>> ListFilters(NmrcEligibilityListParam param)
        {
            var expression = ExtensionLinq.True<NmrcEligibilityEntity>();
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.CategoryName))
                {
                    expression = expression.And(second: t => t.CategoryName.Contains(param.CategoryName));
                }
            }
            return expression;
        }
        private Expression<Func<NmrcEligibilityEntity, bool>> ListFilter(NmrcEligibilityListParam param)
        {
            var expression = ExtensionLinq.True<NmrcEligibilityEntity>();
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.Item))
                {
                    expression = expression.And(t => t.Item.Contains(param.Item));
                }
            }
            return expression;
        }


        private Expression<Func<NmrcEligibilityEntity, bool>> ListFilterbyItem(string item)
        {
            var expression = ExtensionLinq.True<NmrcEligibilityEntity>();
            if (item != null)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    expression = expression.And(t => t.Item == item);
                }


            }
            return expression;
        }
        #endregion
    }
}