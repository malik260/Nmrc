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
    public class PropertySubscriptionRepository : DataRepository, IPropertySubscriptionRepository
    {
        #region Retrieve data
        public async Task<List<PropertySubscriptionEntity>> GetList(PropertySubscriptionListParam param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<PropertySubscriptionEntity>> GetPageList(PropertySubscriptionListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<PropertySubscriptionEntity> GetEntity(long id)
        {
            return await BaseRepository().FindEntity<PropertySubscriptionEntity>(id);
        }
         public async Task<PropertySubscriptionEntity> GetSubcribedProperties(string Nhf)
        {
            return await BaseRepository().FindEntity<PropertySubscriptionEntity>(i=> i.Subscriber == Nhf);
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
        public bool ExistCompany(PropertySubscriptionEntity entity)
        {
            var expression = ExtensionLinq.True<PropertySubscriptionEntity>();
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
        public async Task SaveForm(PropertySubscriptionEntity entity)
        {
            try
            {
                if (entity.Id.IsNullOrZero())
                {
                    await entity.Create();
                    await BaseRepository().Insert<PropertySubscriptionEntity>(entity);
                }
                else
                {
                    await entity.Modify();
                    await BaseRepository().Update<PropertySubscriptionEntity>(entity);
                }
            }catch(Exception ex)
            {
                throw;
            }
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await BaseRepository().Delete<PropertySubscriptionEntity>(idArr);
        }
        #endregion

        #region Private method
        private Expression<Func<PropertySubscriptionEntity, bool>> ListFilter(PropertySubscriptionListParam param)
        {
            var expression = ExtensionLinq.True<PropertySubscriptionEntity>();
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