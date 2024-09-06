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
    public class SchemeRepository : DataRepository, ISchemeRepository
    {
        #region Retrieve data
        public async Task<List<SchemeSetupEntity>> GetList(SchemeListParam param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<SchemeSetupEntity>> GetPageList(SchemeListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<SchemeSetupEntity> GetEntity(string name)
        {
            return await BaseRepository().FindEntity<SchemeSetupEntity>(x => x.SchemeName == name);
        }

        public async Task<SchemeSetupEntity> GetEntities(int id)
        {
            return await BaseRepository().FindEntity<SchemeSetupEntity>(id);
        }
        //public async Task<CreditTypeEntity> GetEntityByProductCode(string code)
        //{
        //    return await BaseRepository().FindEntity<CreditTypeEntity>(x => x.Code == code);
        //}

        public async Task<SchemeSetupEntity> GetEntitybyName(string name)
        {
            return await BaseRepository().FindEntity<SchemeSetupEntity>(x => x.SchemeName.Contains(name));
        }
        public async Task<SchemeSetupEntity> GetEntitybiId(int id)
        {
            return await BaseRepository().FindEntity<SchemeSetupEntity>(x => x.Id == id);
        }


        #endregion

        #region Submit data
        public async Task SaveForm(SchemeSetupEntity entity)
        {
            var db = await BaseRepository().BeginTrans();
            try
            {
                if (entity.Id.IsNullOrZero())
                {
                    await entity.Create();
                    await BaseRepository().Insert<SchemeSetupEntity>(entity);
                }
                else
                {
                    await BaseRepository().Update<SchemeSetupEntity>(entity);
                }
                foreach (var item in entity.LendersId)
                {
                    var schemeLender = new SchemeLenderEntity();
                    schemeLender.LendersId = item;
                    schemeLender.SchemeId = entity.Id;

                    await schemeLender.Create();

                    await db.Insert(schemeLender);
                }

                await db.CommitTrans();
            }
            catch (Exception ex)
            {
                await db.RollbackTrans();
                throw;
            }
        }
            public bool ExistSchemeName(SchemeSetupEntity entity)
        {
            var expression = ExtensionLinq.True<SchemeSetupEntity>();
            expression = expression.And(t => t.BaseIsDelete == 0);
            if (entity.Id.IsNullOrZero())
            {
                expression = expression.And(t => t.SchemeName == entity.SchemeName);
            }
            else
            {
                expression = expression.And(t => t.SchemeName == entity.SchemeName && t.Id != entity.Id);
            }
            return BaseRepository().IQueryable(expression).Count() > 0 ? true : false;
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await BaseRepository().Delete<SchemeSetupEntity>(idArr);
        }
        #endregion

        #region Private method
        private Expression<Func<SchemeSetupEntity, bool>> ListFilter(SchemeListParam param)
        {
            var expression = ExtensionLinq.True<SchemeSetupEntity>();
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.SchemeName))
                {
                    expression = expression.And(second: t => t.SchemeName.Contains(param.SchemeName));
                }
            }
            return expression;
        }
        #endregion
    }
}
