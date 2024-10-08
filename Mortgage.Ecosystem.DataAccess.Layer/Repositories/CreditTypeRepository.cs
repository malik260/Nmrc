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
    public class CreditTypeRepository : DataRepository, ICreditTypeRepository
    {
        #region Retrieve data
        public async Task<List<CreditTypeEntity>> GetList(CreditTypeListParam param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<CreditTypeEntity>> GetPageList(CreditTypeListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<CreditTypeEntity> GetEntity(string code)
        {
            return await BaseRepository().FindEntity<CreditTypeEntity>(x => x.Code == code || x.Name == code || x.ProductId == Convert.ToInt32(code));
        }

        public async Task<CreditTypeEntity> GetEntityByProductCode(string code)
        {
            return await BaseRepository().FindEntity<CreditTypeEntity>(x => x.Code == code);
        }

        public async Task<CreditTypeEntity> GetEntitybyName(string name)
        {
            return await BaseRepository().FindEntity<CreditTypeEntity>(x =>  x.Name.Contains(name));
        }
        public async Task<CreditTypeEntity> GetEntitybiId(int id)
        {
            return await BaseRepository().FindEntity<CreditTypeEntity>(x => x.Id == id);
        }
        
       
        #endregion

        #region Submit data
        public async Task SaveForm(CreditTypeEntity entity)
        {
            if (entity.Id.IsNullOrZero())
            {
                await entity.Create();
                await BaseRepository().Insert<CreditTypeEntity>(entity);
            }
            else
            {
                await BaseRepository().Update<CreditTypeEntity>(entity);
            }
        }

        public bool ExistCode(CreditTypeEntity entity)
        {
            var expression = ExtensionLinq.True<CreditTypeEntity>();
            expression = expression.And(t => t.BaseIsDelete == 0);
            if (entity.Id.IsNullOrZero())
            {
                expression = expression.And(t => t.Code == entity.Code);
            }
            else
            {
                expression = expression.And(t => t.Code == entity.Code && t.Id != entity.Id);
            }
            return BaseRepository().IQueryable(expression).Count() > 0 ? true : false;
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await BaseRepository().Delete<CreditTypeEntity>(idArr);
        }
        #endregion

        #region Private method
        private Expression<Func<CreditTypeEntity, bool>> ListFilter(CreditTypeListParam param)
        {
            var expression = ExtensionLinq.True<CreditTypeEntity>();
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
