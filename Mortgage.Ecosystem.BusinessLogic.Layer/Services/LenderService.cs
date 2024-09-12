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
    public class LenderService : ILenderService
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public LenderService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;

        }

        #region Retrieve data
        public async Task<TData<List<LenderSetupEntity>>> GetList(LenderListParam param)
        {
            TData<List<LenderSetupEntity>> obj = new TData<List<LenderSetupEntity>>();
            obj.Data = await _iUnitOfWork.Lenders.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<LenderSetupEntity>>> GetPageList(LenderListParam param, Pagination pagination)
        {
            TData<List<LenderSetupEntity>> obj = new TData<List<LenderSetupEntity>>();
            obj.Data = await _iUnitOfWork.Lenders.GetPageList(param, pagination);
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

        public async Task<TData<LenderSetupEntity>> GetEntity(long id)
        {
            TData<LenderSetupEntity> obj = new TData<LenderSetupEntity>();
            obj.Data = await _iUnitOfWork.Lenders.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<LenderSetupEntity>> GetEntities(int id)
        {
            TData<LenderSetupEntity> obj = new TData<LenderSetupEntity>();

            try
            {
                LenderSetupEntity entity;

                if (id == 0)
                {
                    // Initialize a new entity for adding
                    entity = new LenderSetupEntity();
                }
                else
                {
                    // Get entity by ID for editing
                    entity = await _iUnitOfWork.Lenders.GetEntities(id);

                    if (entity == null)
                    {
                        obj.Message = "Entity not found.";
                        obj.Tag = -1;
                        return obj;
                    }

                    // Retrieve and assign the ProductName based on ProductCode
                    var lender = await _iUnitOfWork.Lenders.GetEntity(entity.Lender);
                    //if (productEntity == null)
                    //{
                    //    entity.ProductName = productEntity.Name;
                    //}
                    if (lender != null)
                    {
                        entity.Lender = lender.Lender;
                    }
                    else
                    {
                        entity.Lender = 0; // Default value if product entity is not found
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
        public async Task<TData<string>> SaveForm(LenderSetupEntity entity)
        {
            TData<string> obj = new TData<string>();
            var ProductExist = await _iUnitOfWork.Lenders.GetEntity(entity.Lender);
            if (ProductExist != null)
            {
                obj.Message = "Lender already exists!";
                obj.Tag = 0;
                return obj;
            }
<<<<<<< HEAD

=======
>>>>>>> 3a8b23030f309960c839d5b12636bddcf5122dd1
            bool isUpdate = entity.Id > 0;
            await _iUnitOfWork.Lenders.SaveForm(entity);
            obj.Data = entity.Id.ParseToString();
            obj.Tag = 1;
            obj.Message = isUpdate ? "Lender Updated successfully" : "Lender added successfully";
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData<long> obj = new TData<long>();
            await _iUnitOfWork.Lenders.DeleteForm(ids);
            obj.Message = "Lenders deleted successfully";
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<string>> UpdateForm(LenderSetupEntity entity)
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

            // Update the existing entity with the new data
            //existingEntity.EmployerNo = entity.Name;
            //existingEntity.Employer = entity.Name;


            // Save the changes to the database
            await _iUnitOfWork.Employees.SaveForm(existingEntity);
            await _iUnitOfWork.Lenders.SaveForm(entity);

            obj.Data = existingEntity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }



        #endregion
    }
}
