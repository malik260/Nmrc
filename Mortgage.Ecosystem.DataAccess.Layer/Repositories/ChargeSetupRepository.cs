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
    public class ChargeSetupRepository : DataRepository, IChargeSetupRepository
    {
        #region Retrieve data
        public async Task<List<ChargeSetupEntity>> GetList(ChargeSetupListParam param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<ChargeSetupEntity>> GetPageList(ChargeSetupListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<ChargeSetupEntity> GetEntity(long id)
        {
            return await BaseRepository().FindEntity<ChargeSetupEntity>(id);
        }


        public async Task<int> GetMaxSort()
        {
            object result = await BaseRepository().FindObject("SELECT MAX(Sort) FROM tbl_ChargeSetup");
            int sort = result.ToInt();
            sort++;
            return sort;
        }

        // Whether the company name exists
        // <param name="entity"></param>
        // <returns></returns>
        public bool ExistCompany(ChargeSetupEntity entity)
        {
            var expression = ExtensionLinq.True<ChargeSetupEntity>();
            expression = expression.And(t => t.BaseIsDelete == 0);
            if (entity.Id.IsNullOrZero())
            {
                expression = expression.And(t => t.Id == entity.Id);
            }
            else
            {
                expression = expression.And(t => t.Id == entity.Id && t.Id != entity.Id);
            }
            return BaseRepository().IQueryable(expression).Count() > 0 ? true : false;
        }
        #endregion

        #region Submit data
        public async Task SaveForm(ChargeSetupEntity entity)
        {
            if (entity.Id.IsNullOrZero())
            {
                await entity.Create();
                await BaseRepository().Insert<ChargeSetupEntity>(entity);
            }
            else
            {
                await entity.Modify();
                await BaseRepository().Update<ChargeSetupEntity>(entity);
            }
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await BaseRepository().Delete<ChargeSetupEntity>(idArr);
        }
        #endregion

        #region Private method
        private Expression<Func<ChargeSetupEntity, bool>> ListFilter(ChargeSetupListParam param)
        {
            var expression = ExtensionLinq.True<ChargeSetupEntity>();
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.Name))
                {
                    expression = expression.And(t => t.ReferenceNumber.Contains(param.Name));
                }
            }
            return expression;
        }
        #endregion
    }
}