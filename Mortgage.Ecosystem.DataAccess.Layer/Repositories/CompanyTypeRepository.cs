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
    public class CompanyTypeRepository : DataRepository, ICompanyTypeRepository
    {
        #region Retrieve data
        public async Task<List<CompanyTypeEntity>> GetList(CompanyTypeListParam param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<CompanyTypeEntity>> GetPageList(CompanyTypeListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<CompanyTypeEntity> GetEntity(long id)
        {
            return await BaseRepository().FindEntity<CompanyTypeEntity>(id);
        }
        #endregion

        #region Submit data
        public async Task SaveForm(CompanyTypeEntity entity)
        {
            if (entity.Id.IsNullOrZero())
            {
                await entity.Create();
                await BaseRepository().Insert<CompanyTypeEntity>(entity);
            }
            else
            {
                await BaseRepository().Update<CompanyTypeEntity>(entity);
            }
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await BaseRepository().Delete<CompanyTypeEntity>(idArr);
        }
        #endregion

        #region Private method
        private Expression<Func<CompanyTypeEntity, bool>> ListFilter(CompanyTypeListParam param)
        {
            var expression = ExtensionLinq.True<CompanyTypeEntity>();
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