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
    public class FeedBackFormRepository : DataRepository, IFeedBackFormRepository
    {
        #region Retrieve data
        public async Task<List<FeedBackFormEntity>> GetList(FeedBackFormListParam param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<FeedBackFormEntity>> GetPageList(FeedBackFormListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<FeedBackFormEntity> GetEntity(long id)
        {
            return await BaseRepository().FindEntity<FeedBackFormEntity>(id);
        }

        public async Task<int> GetMaxSort()
        {
            object result = await BaseRepository().FindObject("SELECT MAX(Sort) FROM tbl_Conpany");
            int sort = result.ToInt();
            sort++;
            return sort;
        }

        // Whether the company name exists
        // <param name="entity"></param>
        // <returns></returns>
        public bool ExistCompany(FeedBackFormEntity entity)
        {
            var expression = ExtensionLinq.True<FeedBackFormEntity>();
            expression = expression.And(t => t.BaseIsDelete == 0);
            if (entity.Id.IsNullOrZero())
            {
                expression = expression.And(t => t.Name == entity.Name);
            }
            else
            {
                expression = expression.And(t => t.Name == entity.Name && t.Id != entity.Id);
            }
            return BaseRepository().IQueryable(expression).Count() > 0 ? true : false;
        }
        #endregion

        #region Submit data
        public async Task SaveForm(FeedBackFormEntity entity)
        {
            var db = await BaseRepository().BeginTrans();
            try
            {
                if (entity.Id.IsNullOrZero())
                {
                    await entity.Create();
                    await BaseRepository().Insert<FeedBackFormEntity>(entity);
                }
                else
                {
                    await entity.Modify();
                    await BaseRepository().Update<FeedBackFormEntity>(entity);
                }
                await db.CommitTrans();
            }
            catch (Exception)
            {
                await db.RollbackTrans();
                throw;
            }
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await BaseRepository().Delete<FeedBackFormEntity>(idArr);
        }
        #endregion

        #region Private method
        private Expression<Func<FeedBackFormEntity, bool>> ListFilter(FeedBackFormListParam param)
        {
            var expression = ExtensionLinq.True<FeedBackFormEntity>();
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.Name))
                {
                    expression = expression.And(t => t.Name.Contains(param.Name));
                }
            }
            return expression;
        }
        #endregion
    }
}