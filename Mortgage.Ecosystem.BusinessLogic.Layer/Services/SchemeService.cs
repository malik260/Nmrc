using Microsoft.EntityFrameworkCore;
using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Operator;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Services
{
    public class SchemeService : ISchemeService
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public SchemeService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;

        }

        #region Retrieve data
        public async Task<TData<List<SchemeSetupEntity>>> GetList(SchemeListParam param)
        {
            TData<List<SchemeSetupEntity>> obj = new TData<List<SchemeSetupEntity>>();
            obj.Data = await _iUnitOfWork.Schemes.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<SchemeSetupEntity>>> GetPageList(SchemeListParam param, Pagination pagination)
        {
            TData<List<SchemeSetupEntity>> obj = new TData<List<SchemeSetupEntity>>();
            obj.Data = await _iUnitOfWork.Schemes.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        //public async Task<TData<List<ZtreeInfo>>> GetZtreeCreditTypeList(CreditTypeListParam param)
        //{
        //    var obj = new TData<List<ZtreeInfo>>();
        //    obj.Data = new List<ZtreeInfo>();
        //    List<CreditTypeEntity> changeEmployerList = await _iUnitOfWork.CreditTypes.GetList(param);
        //    foreach (CreditTypeEntity changeEmployer in changeEmployerList)
        //    {
        //        obj.Data.Add(new ZtreeInfo
        //        {
        //            id = changeEmployer.Id,
        //        });
        //    }
        //    obj.Tag = 1;
        //    return obj;
        //}

        //public async Task<TData<List<ZtreeInfo>>> GetZtreeUserList(CreditTypeListParam param)
        //{
        //    var obj = new TData<List<ZtreeInfo>>();
        //    obj.Data = new List<ZtreeInfo>();
        //    List<CreditTypeEntity> changeEmployerList = await _iUnitOfWork.CreditTypes.GetList(param);
        //    List<UserEntity> userList = await _iUnitOfWork.Users.GetList(null);
        //    foreach (CreditTypeEntity changeEmployer in changeEmployerList)
        //    {
        //        obj.Data.Add(new ZtreeInfo
        //        {
        //            id = changeEmployer.Id,
        //        });
        //        List<long> userIdList = userList.Where(t => t.Company == changeEmployer.Id).Select(t => t.Employee).ToList();
        //        foreach (UserEntity user in userList.Where(t => userIdList.Contains(t.Employee)))
        //        {
        //            obj.Data.Add(new ZtreeInfo
        //            {
        //                id = user.Id,
        //                name = user.RealName
        //            });
        //        }
        //    }
        //    obj.Tag = 1;
        //    return obj;
        //}       

        public async Task<TData<SchemeSetupEntity>> GetEntity(string name)
        {
            TData<SchemeSetupEntity> obj = new TData<SchemeSetupEntity>();
            obj.Data = await _iUnitOfWork.Schemes.GetEntity(name);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<SchemeSetupEntity>> GetEntities(int id)
        {
            TData<SchemeSetupEntity> obj = new TData<SchemeSetupEntity>();

            try
            {
                SchemeSetupEntity entity;

                if (id == 0)
                {
                    // Initialize a new entity for adding
                    entity = new SchemeSetupEntity();
                }
                else
                {
                    // Get entity by ID for editing
                    entity = await _iUnitOfWork.Schemes.GetEntities(id);

                    if (entity == null)
                    {
                        obj.Message = "Entity not found.";
                        obj.Tag = -1;
                        return obj;
                    }

                    // Retrieve and assign the ProductName based on ProductCode
                    var scheme = await _iUnitOfWork.Schemes.GetEntitybyName(entity.SchemeName);
                    //if (productEntity == null)
                    //{
                    //    entity.ProductName = productEntity.Name;
                    //}
                    if (scheme != null)
                    {
                        entity.SchemeName = scheme.SchemeName;
                    }
                    else
                    {
                        entity.SchemeName = "Unknown Product"; // Default value if product entity is not found
                    }
                }

                obj.Data = entity; // Assign the retrieved or new entity to obj.Data
                obj.Tag = 1; // Set tag to indicate success
            }
            catch (Exception ex)
            {
                obj.Message = ex.Message; // Set error message
                obj.Tag = -1; // Set tag to indicate failure
            }

            return obj;
        }


        //public async Task<TData<int>> GetMaxSort()
        //{
        //    TData<int> obj = new TData<int>();
        //    obj.Data = await _iUnitOfWork.CreditTypes.GetMaxSort();
        //    obj.Tag = 1;
        //    return obj;
        //}
        #endregion

        #region Submit data
        public async Task<TData<string>> SaveForm(SchemeSetupEntity entity)
        {
            TData<string> obj = new TData<string>();
            var NameExist = await _iUnitOfWork.Schemes.GetEntitybyName(entity.SchemeName);
            if (NameExist != null)
            {
                obj.Message = "Name already exists!";
                obj.Tag = 0;
                return obj;
            }
            bool isUpdate = entity.Id > 0;
            await _iUnitOfWork.Schemes.SaveForm(entity);
            obj.Data = entity.Id.ParseToString();
            obj.Tag = 1;
            obj.Message = isUpdate ? "Scheme Updated successfully" : "Scheme added successfully";
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData<long> obj = new TData<long>();
            await _iUnitOfWork.Schemes.DeleteForm(ids);

            obj.Tag = 1;
            obj.Message = "Scheme deleted successfully";
            return obj;
        }

        public async Task<TData<string>> UpdateForm(SchemeSetupEntity entity)
        {
            TData<string> obj = new TData<string>();

            // Check if the entity exists in the database
            EmployeeEntity existingEntity = await _iUnitOfWork.Employees.GetEntity(entity.Id);
            if (existingEntity == null)
            {
                obj.Message = "Entity not found";
                obj.Tag = -1;
                return obj;
            }

       


            // Save the changes to the database
            await _iUnitOfWork.Employees.SaveForm(existingEntity);
            await _iUnitOfWork.Schemes.SaveForm(entity);

            obj.Data = existingEntity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }



        #endregion
    }
}
