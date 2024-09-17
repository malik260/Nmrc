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
    public class SchemeLenderRepository : DataRepository, ISchemeLenderRepository
    {
        #region Retrieve data
        public async Task<List<SchemeLenderEntity>> GetList(SchemeLenderListParam param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<SchemeLenderEntity>> GetPageList(SchemeLenderListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<SchemeLenderEntity> GetEntity(int id)
        {
            return await BaseRepository().FindEntity<SchemeLenderEntity>(x => x.SchemeId == id || x.LendersId == id);
        }

        public async Task<SchemeLenderEntity> GetEntities(int id)
        {
            return await BaseRepository().FindEntity<SchemeLenderEntity>(id);
        }
        //public async Task<CreditTypeEntity> GetEntityByProductCode(string code)
        //{
        //    return await BaseRepository().FindEntity<CreditTypeEntity>(x => x.Code == code);
        //}


        public async Task<SchemeLenderEntity> GetEntitybiId(int id)
        {
            return await BaseRepository().FindEntity<SchemeLenderEntity>(x => x.Id == id);
        }


        #endregion

        #region Submit data
        public async Task SaveForm(SchemeLenderEntity entity)
        {
            if (entity.Id.IsNullOrZero())
            {
                await entity.Create();
                await BaseRepository().Insert<SchemeLenderEntity>(entity);
            }
            else
            {
                await BaseRepository().Update<SchemeLenderEntity>(entity);
            }
        }
      

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await BaseRepository().Delete<SchemeLenderEntity>(idArr);
        }
        #endregion

        #region Private method
        private Expression<Func<SchemeLenderEntity, bool>> ListFilter(SchemeLenderListParam param)
        {
            var expression = ExtensionLinq.True<SchemeLenderEntity>();
            if (param != null)
            {
                if (param.SchemeId != 0)
                {
                    expression = expression.And(t => t.Id == param.SchemeId);
                }
            }
            return expression;
        }

        #endregion
    }
}
