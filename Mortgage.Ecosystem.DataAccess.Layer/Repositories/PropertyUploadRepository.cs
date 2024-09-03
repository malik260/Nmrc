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
    public class PropertyUploadRepository : DataRepository, IPropertyUploadRepository
    {
        #region Retrieve data
        public async Task<List<PropertyUploadEntity>> GetList(long id)
        {
            var expression = ListFilter2(id);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<PropertyUploadEntity>> GetList2(PropertyUploadListParam param)
        {
            var expression = ListFilter3(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<PropertyUploadEntity>> GetPageList(PropertyUploadListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

       

        public async Task<PropertyUploadEntity> GetEntity(long id)
        {
            return await BaseRepository().FindEntity<PropertyUploadEntity>(id);
        }
        #endregion

        #region Submit data
        public async Task SaveForm(PropertyUploadEntity entity)
        {
            if (entity.Id.IsNullOrZero())
            {
                await entity.Create();
                await BaseRepository().Insert<PropertyUploadEntity>(entity);
            }
            else
            {
                await BaseRepository().Update<PropertyUploadEntity>(entity);
            }
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await BaseRepository().Delete<PropertyUploadEntity>(idArr);
        }
        #endregion

        #region Private method
        private Expression<Func<PropertyUploadEntity, bool>> ListFilter(PropertyUploadListParam param)
        {
            var expression = ExtensionLinq.True<PropertyUploadEntity>();
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.Pmb.ToString()) && (!string.IsNullOrEmpty(param.Images) || !string.IsNullOrEmpty(param.Type)))
                {
                    expression = expression.And(second: t => t.Pmb == param.Pmb && t.Type.Contains(param.Type) && t.Images.Contains(param.Images));
                }
            }
            return expression;
        }



        private Expression<Func<PropertyUploadEntity, bool>> ListFilter3(PropertyUploadListParam param)
        {
            var expression = ExtensionLinq.True<PropertyUploadEntity>();
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.Pmb.ToString()) && (!string.IsNullOrEmpty(param.Images) || !string.IsNullOrEmpty(param.Type)))
                {
                    expression = expression.And(second: t => t.Pmb == param.Pmb && t.Type.Contains(param.Type) && t.Images.Contains(param.Images));
                }
            }
            return expression;
        }




        private Expression<Func<PropertyUploadEntity, bool>> ListFilter2(long id)
        {
            var expression = ExtensionLinq.True<PropertyUploadEntity>();
            if (id != 0)
            {

                expression = expression.And(second: t => t.ParselId == id);
            }
            return expression;
        }
        #endregion
    }
}