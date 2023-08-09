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
    public class PropertyGalleryRepository : DataRepository, IPropertyGalleryRepository
    {
        #region Retrieve data
        public async Task<List<PropertyGalleryEntity>> GetList(PropertyGalleryListParam param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<PropertyGalleryEntity>> GetPageList(PropertyGalleryListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<PropertyGalleryEntity> GetEntity(long id)
        {
            return await BaseRepository().FindEntity<PropertyGalleryEntity>(id);
        }

        public async Task<int> GetMaxSort()
        {
            object result = await BaseRepository().FindObject("SELECT MAX(Sort) FROM tbl_PropertyGallery");
            int sort = result.ToInt();
            sort++;
            return sort;
        }

        // Whether the company name exists
        // <param name="entity"></param>
        // <returns></returns>
        public bool ExistCompany(PropertyGalleryEntity entity)
        {
            var expression = ExtensionLinq.True<PropertyGalleryEntity>();
            expression = expression.And(t => t.BaseIsDelete == 0);
            if (entity.Id.IsNullOrZero())
            {
                expression = expression.And(t => t.Title == entity.Title);
            }
            else
            {
                expression = expression.And(t => t.Title == entity.Title && t.Id != entity.Id);
            }
            return BaseRepository().IQueryable(expression).Count() > 0 ? true : false;
        }
        #endregion

        #region Submit data
        public async Task SaveForm(PropertyGalleryEntity entity)
        {
            if (entity.Id.IsNullOrZero())
            {
                await entity.Create();
                await BaseRepository().Insert<PropertyGalleryEntity>(entity);
            }
            else
            {
                await entity.Modify();
                await BaseRepository().Update<PropertyGalleryEntity>(entity);
            }
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await BaseRepository().Delete<PropertyGalleryEntity>(idArr);
        }
        #endregion

        #region Private method
        private Expression<Func<PropertyGalleryEntity, bool>> ListFilter(PropertyGalleryListParam param)
        {
            var expression = ExtensionLinq.True<PropertyGalleryEntity>();
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.Title))
                {
                    expression = expression.And(t => t.Title.Contains(param.Title));
                }
            }
            return expression;
        }
        #endregion
    }
}