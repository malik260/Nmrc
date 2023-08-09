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
    public class DesignationRepository : DataRepository, IDesignationRepository
    {
        #region Retrieve data
        public async Task<List<DesignationEntity>> GetList(DesignationListParam param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<DesignationEntity>> GetPageList(DesignationListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<DesignationEntity> GetEntity(long id)
        {
            return await BaseRepository().FindEntity<DesignationEntity>(id);
        }

        public async Task<int> GetMaxSort()
        {
            object result = await BaseRepository().FindObject("SELECT MAX(Sort) FROM st_Designation");
            int sort = result.ParseToInt();
            sort++;
            return sort;
        }

        public bool ExistDesignationName(DesignationEntity entity)
        {
            var expression = ExtensionLinq.True<DesignationEntity>();
            expression = expression.And(t => t.BaseIsDelete == 0);
            if (entity.Id.IsNullOrZero())
            {
                expression = expression.And(t => t.Company == entity.Company && t.Name == entity.Name);
            }
            else
            {
                expression = expression.And(t => t.Company == entity.Company && t.Name == entity.Name && t.Id != entity.Id);
            }
            return BaseRepository().IQueryable(expression).Count() > 0 ? true : false;
        }
        #endregion

        #region Submit data
        public async Task SaveForm(DesignationEntity entity)
        {
            if (entity.Id.IsNullOrZero())
            {
                await entity.Create();
                await BaseRepository().Insert<DesignationEntity>(entity);
            }
            else
            {
                await entity.Modify();
                await BaseRepository().Update(entity);
            }
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await BaseRepository().Delete<DesignationEntity>(idArr);
        }
        #endregion

        #region Private method
        private Expression<Func<DesignationEntity, bool>> ListFilter(DesignationListParam param)
        {
            var expression = ExtensionLinq.True<DesignationEntity>();
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.Company.ToString()) && !string.IsNullOrEmpty(param.Name))
                {
                    expression = expression.And(t => t.Company == param.Company && t.Name.Contains(param.Name));
                }
                if (!string.IsNullOrEmpty(param.Ids))
                {
                    long[] positionIdArr = TextHelper.SplitToArray<long>(param.Ids, ',');
                    expression = expression.And(t => positionIdArr.Contains(t.Id));
                }
            }
            return expression;
        }
        #endregion
    }
}