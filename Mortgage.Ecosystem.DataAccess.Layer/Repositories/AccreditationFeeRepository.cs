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
    public class AccreditationFeeRepository : DataRepository, IAccreditationFeeRepository
    {
        #region Retrieve data
        public async Task<List<AccreditationFeeEntity>> GetList(AccreditationFeeListParam param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<AccreditationFeeEntity>> GetPageList(AccreditationFeeListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<AccreditationFeeEntity> GetEntity(long id)
        {
            return await BaseRepository().FindEntity<AccreditationFeeEntity>(id);
        }

        public async Task<EmployeeEntity> GetEmployeeEntity(long id)
        {
            return await BaseRepository().FindEntity<EmployeeEntity>(id);
        }

        public async Task<int> GetMaxSort()
        {
            object result = await BaseRepository().FindObject("SELECT MAX(Sort) FROM tbl_AccredidationFee");
            int sort = result.ToInt();
            sort++;
            return sort;
        }

        // Whether the company name exists
        // <param name="entity"></param>
        // <returns></returns>
        public bool ExistCompany(AccreditationFeeEntity entity)
        {
            var expression = ExtensionLinq.True<AccreditationFeeEntity>();
            expression = expression.And(t => t.BaseIsDelete == 0);
            if (entity.Id.IsNullOrZero())
            {
                expression = expression.And(t => t.AgenName == entity.AgenName);
            }
            else
            {
                expression = expression.And(t => t.AgenName == entity.AgenName && t.Id != entity.Id);
            }
            return BaseRepository().IQueryable(expression).Count() > 0 ? true : false;
        }
        #endregion

        #region Submit data
        public async Task SaveForm(AccreditationFeeEntity entity)
        {
            if (entity.Id.IsNullOrZero())
            {
                await entity.Create();
                await BaseRepository().Insert<AccreditationFeeEntity>(entity);
            }
            else
            {
                await entity.Modify();
                await BaseRepository().Update<AccreditationFeeEntity>(entity);
            }
        }



        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await BaseRepository().Delete<AccreditationFeeEntity>(idArr);
        }
        #endregion

        #region Private method
        private Expression<Func<AccreditationFeeEntity, bool>> ListFilter(AccreditationFeeListParam param)
        {
            var expression = ExtensionLinq.True<AccreditationFeeEntity>();
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.AgentName))
                {
                    expression = expression.And(t => t.MobileNumber.Contains(param.AgentName));
                }
            }
            return expression;
        }
        #endregion
    }
}