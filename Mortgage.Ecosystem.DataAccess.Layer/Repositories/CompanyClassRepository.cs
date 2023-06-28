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
    public class CompanyClassRepository : DataRepository, ICompanyClassRepository
    {
        #region Retrieve data
        public async Task<List<CompanyClassEntity>> GetList(CompanyClassListParam param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<CompanyClassEntity>> GetPageList(CompanyClassListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<CompanyClassEntity> GetEntity(long id)
        {
            return await BaseRepository().FindEntity<CompanyClassEntity>(id);
        }
        #endregion

        #region Submit data
        public async Task SaveForm(CompanyClassEntity entity)
        {
            if (entity.Id.IsNullOrZero())
            {
                await entity.Create();
                await BaseRepository().Insert<CompanyClassEntity>(entity);
            }
            else
            {
                await BaseRepository().Update<CompanyClassEntity>(entity);
            }
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await BaseRepository().Delete<CompanyClassEntity>(idArr);
        }
        #endregion

        #region Private method
        private Expression<Func<CompanyClassEntity, bool>> ListFilter(CompanyClassListParam param)
        {
            var expression = ExtensionLinq.True<CompanyClassEntity>();
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