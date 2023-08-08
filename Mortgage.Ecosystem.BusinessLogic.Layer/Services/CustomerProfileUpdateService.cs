using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Services
{
    public class CustomerProfileUpdateService : ICustomerProfileUpdateService
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public CustomerProfileUpdateService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        #region Retrieve data
        public async Task<TData<List<CustomerProfileUpdateEntity>>> GetList(CustomerProfileUpdateListParam param)
        {
            TData<List<CustomerProfileUpdateEntity>> obj = new TData<List<CustomerProfileUpdateEntity>>();
            obj.Data = await _iUnitOfWork.CustomerProfileUpdates.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<CustomerProfileUpdateEntity>>> GetPageList(CustomerProfileUpdateListParam param, Pagination pagination)
        {
            TData<List<CustomerProfileUpdateEntity>> obj = new TData<List<CustomerProfileUpdateEntity>>();
            obj.Data = await _iUnitOfWork.CustomerProfileUpdates.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ZtreeInfo>>> GetZtreeCustomerProfileUpdateList(CustomerProfileUpdateListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<CustomerProfileUpdateEntity> customerProfileUpdateList = await _iUnitOfWork.CustomerProfileUpdates.GetList(param);
            foreach (CustomerProfileUpdateEntity customerProfileUpdate in customerProfileUpdateList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = customerProfileUpdate.Id,
                    name = customerProfileUpdate.MobileNumber
                });
            }
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ZtreeInfo>>> GetZtreeUserList(CustomerProfileUpdateListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<CustomerProfileUpdateEntity> customerProfileUpdateList = await _iUnitOfWork.CustomerProfileUpdates.GetList(param);
            List<UserEntity> userList = await _iUnitOfWork.Users.GetList(null);
            foreach (CustomerProfileUpdateEntity customerProfileUpdate in customerProfileUpdateList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = customerProfileUpdate.Id,
                    name = customerProfileUpdate.MobileNumber
                });
                List<long> userIdList = userList.Where(t => t.Company == customerProfileUpdate.Id).Select(t => t.Employee).ToList();
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

        public async Task<TData<CustomerProfileUpdateEntity>> GetEntity(long id)
        {
            TData<CustomerProfileUpdateEntity> obj = new TData<CustomerProfileUpdateEntity>();
            obj.Data = await _iUnitOfWork.CustomerProfileUpdates.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<int>> GetMaxSort()
        {
            TData<int> obj = new TData<int>();
            obj.Data = await _iUnitOfWork.CustomerProfileUpdates.GetMaxSort();
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region Submit data
        public async Task<TData<string>> SaveForm(CustomerProfileUpdateEntity entity)
        {
            TData<string> obj = new TData<string>();
            await _iUnitOfWork.CustomerProfileUpdates.SaveForm(entity);
            obj.Data = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<string>> UpdateCustomerProfile(CustomerProfileUpdateEntity entity)
        {
            TData<string> obj = new TData<string>();

            // Check if the entity exists in the database
            EmployeeEntity existingEntity = await _iUnitOfWork.Employees.GetById(entity.Id);
            if (existingEntity == null)
            {
                obj.Message = "Entity not found";
                obj.Tag = -1;
                return obj;
            }

            // Update the existing entity with the new data
            string[] fullName = entity.FullName.Split(' ');

            existingEntity.MobileNumber = entity.MobileNumber;
            existingEntity.PostalAddress = entity.Address;
            existingEntity.MaritalStatus = entity.MaritalStatus;
            existingEntity.BankAccountNumber = entity.BankAccountNumber;
            existingEntity.CustomerBank = entity.CustomerBank;
            existingEntity.MonthlySalary = entity.MonthlyIncome;
            //existingEntity.NOKName = entity.NOKName;
            //existingEntity.NOKNumber = entity.NOKNumber;
            //existingEntity.LastName = fullName[0];
            //existingEntity.FirstName = fullName[1];
            //existingEntity.OtherName = fullName[2];
            //existingEntity.NOKAddress = entity.NOKAddress;
            //existingEntity.Relationship= entity.Relationship;
            // Save the changes to the database
            await _iUnitOfWork.Employees.SaveForm(existingEntity);
            await _iUnitOfWork.CustomerProfileUpdates.SaveForm(entity);

            obj.Data = existingEntity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }
        public async Task<TData> DeleteForm(string ids)
        {
            TData<long> obj = new TData<long>();
            await _iUnitOfWork.CustomerProfileUpdates.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion
    }
}
