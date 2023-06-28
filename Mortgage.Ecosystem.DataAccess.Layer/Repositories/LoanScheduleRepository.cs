﻿using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
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
    public class LoanScheduleRepository : DataRepository, ILoanScheduleRepository
    {
        #region Retrieve data
        public async Task<List<LoanScheduleEntity>> GetList(LoanScheduleListParam param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<LoanScheduleEntity>> GetPageList(LoanScheduleListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<LoanScheduleEntity> GetEntity(long id)
        {
            return await BaseRepository().FindEntity<LoanScheduleEntity>(id);
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
        public bool ExistCompany(LoanScheduleEntity entity)
        {
            var expression = ExtensionLinq.True<LoanScheduleEntity>();
            expression = expression.And(t => t.BaseIsDelete == 0);
            if (entity.Id.IsNullOrZero())
            {
                expression = expression.And(t => t.Customer == entity.Customer);
            }
            else
            {
                expression = expression.And(t => t.Customer == entity.Customer && t.Id != entity.Id);
            }
            return BaseRepository().IQueryable(expression).Count() > 0 ? true : false;
        }
        #endregion

        #region Submit data
        public async Task SaveForm(LoanScheduleEntity entity)
        {
            if (entity.Id.IsNullOrZero())
            {
                await entity.Create();
                await BaseRepository().Insert<LoanScheduleEntity>(entity);
            }
            else
            {
                await entity.Modify();
                await BaseRepository().Update<LoanScheduleEntity>(entity);
            }
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await BaseRepository().Delete<LoanScheduleEntity>(idArr);
        }
        #endregion

        #region Private method
        private Expression<Func<LoanScheduleEntity, bool>> ListFilter(LoanScheduleListParam param)
        {
            var expression = ExtensionLinq.True<LoanScheduleEntity>();
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.Customer))
                {
                    expression = expression.And(t => t.Customer.Contains(param.Customer));
                }
            }
            return expression;
        }
        #endregion
    }
}