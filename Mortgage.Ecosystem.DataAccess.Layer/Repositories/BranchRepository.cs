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
    public class BranchRepository : DataRepository, IBranchRepository
    {
        #region Retrieve data
        public async Task<List<BranchEntity>> GetList(BranchListParam param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<BranchEntity>> GetPageList(BranchListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<BranchEntity> GetEntity(long id)
        {
            return await BaseRepository().FindEntity<BranchEntity>(id);
        }

        public bool ExistJob(BranchEntity entity)
        {
            var expression = ExtensionLinq.True<BranchEntity>();
            expression = expression.And(t => t.BaseIsDelete == 0);
            if (entity.Id.IsNullOrZero())
            {
                expression = expression.And(t => t.Company == entity.Company && t.Name == entity.Name);
            }
            else
            {
                expression = expression.And(t => t.Company == entity.Company && t.Name == entity.Name && t.Id != entity.Id);
            }
            return BaseRepository().IQueryable(expression).Count() > 0 ? true : false;
        }
        #endregion

        #region Submit data
        public async Task SaveForm(BranchEntity entity)
        {
            if (entity.Id.IsNullOrZero())
            {
                await entity.Create();
                await BaseRepository().Insert<BranchEntity>(entity);
            }
            else
            {
                await entity.Modify();
                await BaseRepository().Update<BranchEntity>(entity);
            }
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await BaseRepository().Delete<BranchEntity>(idArr);
        }
        #endregion

        #region Private method
        private Expression<Func<BranchEntity, bool>> ListFilter(BranchListParam param)
        {
            var expression = ExtensionLinq.True<BranchEntity>();
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