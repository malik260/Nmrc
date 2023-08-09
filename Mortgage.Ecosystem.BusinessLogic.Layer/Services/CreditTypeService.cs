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
    public class CreditTypeService : ICreditTypeService
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public CreditTypeService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;

        }

        #region Retrieve data
        public async Task<TData<List<CreditTypeEntity>>> GetList(CreditTypeListParam param)
        {
            TData<List<CreditTypeEntity>> obj = new TData<List<CreditTypeEntity>>();
            obj.Data = await _iUnitOfWork.CreditTypes.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<CreditTypeEntity>>> GetPageList(CreditTypeListParam param, Pagination pagination)
        {
            TData<List<CreditTypeEntity>> obj = new TData<List<CreditTypeEntity>>();
            obj.Data = await _iUnitOfWork.CreditTypes.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ZtreeInfo>>> GetZtreeCreditTypeList(CreditTypeListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<CreditTypeEntity> changeEmployerList = await _iUnitOfWork.CreditTypes.GetList(param);
            foreach (CreditTypeEntity changeEmployer in changeEmployerList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = changeEmployer.Id,
                });
            }
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ZtreeInfo>>> GetZtreeUserList(CreditTypeListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<CreditTypeEntity> changeEmployerList = await _iUnitOfWork.CreditTypes.GetList(param);
            List<UserEntity> userList = await _iUnitOfWork.Users.GetList(null);
            foreach (CreditTypeEntity changeEmployer in changeEmployerList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = changeEmployer.Id,
                });
                List<long> userIdList = userList.Where(t => t.Company == changeEmployer.Id).Select(t => t.Employee).ToList();
                foreach (UserEntity user in userList.Where(t => userIdList.Contains(t.Employee)))
                {
                    obj.Data.Add(new ZtreeInfo
                    {
                        id = user.Id,
                        name = user.RealName
                    });
                }
            }
            obj.Tag = 1;
            return obj;
        }       

        public async Task<TData<CreditTypeEntity>> GetEntity(long id)
        {
            TData<CreditTypeEntity> obj = new TData<CreditTypeEntity>();
            obj.Data = await _iUnitOfWork.CreditTypes.GetEntity(id);
            obj.Tag = 1;
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
        public async Task<TData<string>> SaveForm(CreditTypeEntity entity)
        {
            TData<string> obj = new TData<string>();
            await _iUnitOfWork.CreditTypes.SaveForm(entity);
            obj.Data = entity.Id.ParseToString();
            obj.Tag = 1;
            obj.Message = "Product added successfully";
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData<long> obj = new TData<long>();
            await _iUnitOfWork.CreditTypes.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<string>> UpdateForm(CreditTypeEntity entity)
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
            await _iUnitOfWork.CreditTypes.SaveForm(entity);

            obj.Data = existingEntity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }



        #endregion
    }
}
