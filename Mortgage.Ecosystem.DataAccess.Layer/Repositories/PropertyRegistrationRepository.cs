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
    public class PropertyRegistrationRepository : DataRepository, IPropertyRegistrationRepository
    {
        #region Retrieve data
        public async Task<List<PropertyRegistrationEntity>> GetList(PropertyRegistrationListParam param, Pagination pagination)
        {
            var expression = ListFilter2(param);
            var list = await BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }


        public async Task<List<PropertyRegistrationEntity>> GetPageList(PropertyRegistrationListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<PropertyRegistrationEntity> GetEntities(long id)
        {
            return await BaseRepository().FindEntity<PropertyRegistrationEntity>(id)
;
        }
        public async Task<PropertyRegistrationEntity> GetEntity(long id)
        {
            return await BaseRepository().FindEntity<PropertyRegistrationEntity>(id);
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
        public bool ExistCompany(PropertyRegistrationEntity entity)
        {
            var expression = ExtensionLinq.True<PropertyRegistrationEntity>();
            expression = expression.And(t => t.BaseIsDelete == 0);
            if (entity.Id.IsNullOrZero())
            {
                expression = expression.And(t => t.PropertyLocation == entity.PropertyLocation);
            }
            else
            {
                expression = expression.And(t => t.PropertyLocation == entity.PropertyLocation && t.Id != entity.Id);
            }
            return BaseRepository().IQueryable(expression).Count() > 0 ? true : false;
        }
        #endregion

        #region Submit data
        public async Task SaveForm(PropertyRegistrationEntity entity)
        {
            
            var message = string.Empty;
            var db = await BaseRepository().BeginTrans();
            try
            {
                if (entity.Id.IsNullOrZero())
                {
                    await entity.Create();
                    await db.Insert(entity);
                }
                else
                {
                    await entity.Modify();
                    await db.Update(entity);
                }

                // Next of kin record
                if (!entity.file.IsNullOrZero())
                {
                    for (int i = 0; i < entity.file.Count(); i++)
                    {
                        using (var stream = new MemoryStream())
                        {
                           

                            entity.file[i].CopyTo(stream);

                            PropertyUploadEntity propertyUploadEntity = new()
                            {
                                ParselId = entity.Id,
                                Pmb = entity.ComapnyNumber,
                                Type = entity.PropertyType,
                                Label = entity.file[i].FileName,
                                Images = entity.file[i].ContentType,
                                Size = entity.file[i].Length,
                                filedata = stream.ToArray(),
                            };
                            await propertyUploadEntity.Create();
                            await db.Insert(propertyUploadEntity);

                        }
                    }

                    
                }
                await db.CommitTrans();
            }
            catch (Exception ex)
            {
                await db.RollbackTrans();
                throw;

            }
        }



        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await BaseRepository().Delete<PropertyRegistrationEntity>(idArr);
        }
        #endregion

        #region Private method
        private Expression<Func<PropertyRegistrationEntity, bool>> ListFilter(PropertyRegistrationListParam param)
        {
            var expression = ExtensionLinq.True<PropertyRegistrationEntity>();
            if (param != null)
            {
                if (param.companyNumber != 0)
                {
                    expression = expression.And(t => t.ComapnyNumber == param.companyNumber);
                }
            }
            return expression;
        }

        private Expression<Func<PropertyRegistrationEntity, bool>> ListFilter2(PropertyRegistrationListParam param)
        {
            var expression = ExtensionLinq.True<PropertyRegistrationEntity>();
            if (param != null)
            {
                if (param.propertyType != "0")
                {
                    expression = expression.And(t => t.PropertyType == param.propertyType);
                }
                if (!string.IsNullOrEmpty(param.propertyLocation))
                {
                    expression = expression.And(t => t.PropertyLocation == param.propertyLocation);
                }
                if (param.latitude != 0)
                {
                    expression = expression.And(t => t.Latitude == param.latitude);
                }
                if (param.longitude != 0)
                {
                    expression = expression.And(t => t.Longitude == param.longitude);
                }
            }
            return expression;
        }


        public string RandomDigits(int length)
        {
            var random = new Random();
            string s = string.Empty;
            for (int i = 0; i < length; i++)
                s = String.Concat(s, random.Next(10).ToString());
            return s;
        }
        #endregion
    }
}