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
    public class PropertyRegistrationRepository : DataRepository, IPropertyRegistrationRepository
    {
        #region Retrieve data
        public async Task<List<PropertyRegistrationEntity>> GetList(PropertyRegistrationListParam param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<PropertyRegistrationEntity>> GetPageList(PropertyRegistrationListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<PropertyRegistrationEntity> GetEntity(long id)
        {
            return await BaseRepository().FindEntity<PropertyRegistrationEntity>(id);
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
        public bool ExistCompany(PropertyRegistrationEntity entity)
        {
            var expression = ExtensionLinq.True<PropertyRegistrationEntity>();
            expression = expression.And(t => t.BaseIsDelete == 0);
            if (entity.Id.IsNullOrZero())
            {
                expression = expression.And(t => t.PropertyType == entity.PropertyType);
            }
            else
            {
                expression = expression.And(t => t.PropertyType == entity.PropertyType && t.Id != entity.Id);
            }
            return BaseRepository().IQueryable(expression).Count() > 0 ? true : false;
        }
        #endregion

        #region Submit data
        public async Task SaveForm(PropertyRegistrationEntity entity)
        {
            if (entity.Id.IsNullOrZero())
            {
                await entity.Create();
                await BaseRepository().Insert<PropertyRegistrationEntity>(entity);
            }
            else
            {
                await entity.Modify();
                await BaseRepository().Update<PropertyRegistrationEntity>(entity);
            }
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await BaseRepository().Delete<PropertyRegistrationEntity>(idArr);
        }
        #endregion

        #region Private method
        private Expression<Func<PropertyRegistrationEntity, bool>> ListFilter(PropertyRegistrationListParam param)
        {
            var expression = ExtensionLinq.True<PropertyRegistrationEntity>();
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.PropertyType))
                {
                    expression = expression.And(t => t.PropertyType.Contains(param.PropertyType));
                }
            }
            return expression;
        }
        #endregion
    }
}