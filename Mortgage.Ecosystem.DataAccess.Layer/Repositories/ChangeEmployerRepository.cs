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
    public class ChangeEmployerRepository : DataRepository, IChangeEmployerRepository
    {
        #region Retrieve data
        public async Task<List<ChangeEmployerEntity>> GetList(ChangeEmployerListParam param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<ChangeEmployerEntity>> GetPageList(ChangeEmployerListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<ChangeEmployerEntity> GetEntity(long id)
        {
            return await BaseRepository().FindEntity<ChangeEmployerEntity>(id);
        }

        public async Task<int> GetMaxSort()
        {
            object result = await BaseRepository().FindObject("SELECT MAX(Sort) FROM tbl_ChangeEmployer");
            int sort = result.ToInt();
            sort++;
            return sort;
        }

        // Whether the company name exists
        // <param name="entity"></param>
        // <returns></returns>
        public bool ExistCompany(ChangeEmployerEntity entity)
        {
            var expression = ExtensionLinq.True<ChangeEmployerEntity>();
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
        public async Task SaveForm(ChangeEmployerEntity entity)
        {
            if (entity.Id.IsNullOrZero())
            {
                await entity.Create();
                await BaseRepository().Insert<ChangeEmployerEntity>(entity);
            }
            else
            {
                await entity.Modify();
                await BaseRepository().Update<ChangeEmployerEntity>(entity);
            }
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await BaseRepository().Delete<ChangeEmployerEntity>(idArr);
        }
        #endregion

        #region Private method
        private Expression<Func<ChangeEmployerEntity, bool>> ListFilter(ChangeEmployerListParam param)
        {
            var expression = ExtensionLinq.True<ChangeEmployerEntity>();
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.Name))
                {
                    expression = expression.And(t => t.NhfNumber.Contains(param.Name));
                }
            }
            return expression;
        }
        #endregion
    }
}