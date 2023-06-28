using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
using Mortgage.Ecosystem.DataAccess.Layer.Extensions;
using Mortgage.Ecosystem.DataAccess.Layer.Helpers;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories;
using Mortgage.Ecosystem.DataAccess.Layer.Models;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Operator;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;
using System.Linq.Expressions;

namespace Mortgage.Ecosystem.DataAccess.Layer.Repositories
{
    public class CustomerProfileUpdateRepository : DataRepository, ICustomerProfileUpdateRepository
    {
        #region Retrieve data
        public async Task<List<CustomerProfileUpdateEntity>> GetList(CustomerProfileUpdateListParam param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<CustomerProfileUpdateEntity>> GetPageList(CustomerProfileUpdateListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<CustomerProfileUpdateEntity> GetEntity(long id)
        {
            return await BaseRepository().FindEntity<CustomerProfileUpdateEntity>(id);
        }

        public async Task<EmployeeEntity> GetEmployeeEntity(long id)
        {
            return await BaseRepository().FindEntity<EmployeeEntity>(id);
        }

        public async Task<int> GetMaxSort()
        {
            object result = await BaseRepository().FindObject("SELECT MAX(Sort) FROM tbl_CustomerProfileUpdate");
            int sort = result.ToInt();
            sort++;
            return sort;
        }

        // Whether the company name exists
        // <param name="entity"></param>
        // <returns></returns>
        public bool ExistCompany(CustomerProfileUpdateEntity entity)
        {
            var expression = ExtensionLinq.True<CustomerProfileUpdateEntity>();
            expression = expression.And(t => t.BaseIsDelete == 0);
            if (entity.Id.IsNullOrZero())
            {
                expression = expression.And(t => t.MobileNumber == entity.MobileNumber);
            }
            else
            {
                expression = expression.And(t => t.MobileNumber == entity.MobileNumber && t.Id != entity.Id);
            }
            return BaseRepository().IQueryable(expression).Count() > 0 ? true : false;
        }
        #endregion

        #region Submit data
        public async Task SaveForm(CustomerProfileUpdateEntity entity)
        {
            if (entity.Id.IsNullOrZero())
            {
                await entity.Create();
                await BaseRepository().Insert<CustomerProfileUpdateEntity>(entity);
            }
            else
            {
                await entity.Modify();
                await BaseRepository().Update<CustomerProfileUpdateEntity>(entity);
            }
        }



        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await BaseRepository().Delete<CustomerProfileUpdateEntity>(idArr);
        }
        #endregion

        #region Private method
        private Expression<Func<CustomerProfileUpdateEntity, bool>> ListFilter(CustomerProfileUpdateListParam param)
        {
            var expression = ExtensionLinq.True<CustomerProfileUpdateEntity>();
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.Name))
                {
                    expression = expression.And(t => t.MobileNumber.Contains(param.Name));
                }
            }
            return expression;
        }
        #endregion
    }
}