using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
using Mortgage.Ecosystem.DataAccess.Layer.Extensions;
using Mortgage.Ecosystem.DataAccess.Layer.Helpers;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Operator;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using System.Linq.Expressions;

namespace Mortgage.Ecosystem.DataAccess.Layer.Repositories
{
    public class DepartmentRepository : DataRepository, IDepartmentRepository
    {
        #region Get data
        public async Task<List<DepartmentEntity>> GetList(DepartmentListParam param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.OrderBy(p => p.Sort).ToList();
        }

        public async Task<DepartmentEntity> GetEntity(long id)
        {
            return await BaseRepository().FindEntity<DepartmentEntity>(id);
        }

        public async Task<int> GetMaxSort()
        {
            object result = await BaseRepository().FindObject("SELECT MAX(Sort) FROM tbl_Department");
            int sort = result.ToInt();
            sort++;
            return sort;
        }

        // Whether the department name exists
        // <param name="entity"></param>
        // <returns></returns>
        public bool ExistDepartmentName(DepartmentEntity entity)
        {
            var expression = ExtensionLinq.True<DepartmentEntity>();
            expression = expression.And(t => t.BaseIsDelete == 0);
            if (entity.Id.IsNullOrZero())
            {
                expression = expression.And(t => t.Name == entity.Name && t.Branch == entity.Branch);
            }
            else
            {
                expression = expression.And(t => t.Name == entity.Name && t.Branch == entity.Branch && t.Id != entity.Id);
            }
            return BaseRepository().IQueryable(expression).Count() > 0 ? true : false;
        }

        // Whether there is a sub-department
        // <param name="id"></param>
        // <returns></returns>
        public bool ExistChildrenDepartment(long branch)
        {
            var expression = ExtensionLinq.True<DepartmentEntity>();
            expression = expression.And(t => t.Branch == branch);
            return BaseRepository().IQueryable(expression).Count() > 0 ? true : false;
        }
        #endregion

        #region Submit data
        public async Task SaveForm(DepartmentEntity entity)
        {
            if (entity.Id.IsNullOrZero())
            {
                await entity.Create();
                await BaseRepository().Insert(entity);
            }
            else
            {
                await entity.Modify();
                await BaseRepository().Update(entity);
            }
        }

        public async Task DeleteForm(string ids)
        {
            var db = await BaseRepository().BeginTrans();
            try
            {
                long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
                await db.Delete<DepartmentEntity>(idArr);
                await db.CommitTrans();
            }
            catch
            {
                await db.RollbackTrans();
                throw;
            }
        }
        #endregion

        #region private method
        private Expression<Func<DepartmentEntity, bool>> ListFilter(DepartmentListParam param)
        {
            var expression = ExtensionLinq.True<DepartmentEntity>();
            if (param != null)
            {
                if (!param.Name.IsNull())
                {
                    expression = expression.And(t => t.Name.Contains(param.Name));
                }
            }
            return expression;
        }
        #endregion
    }
}