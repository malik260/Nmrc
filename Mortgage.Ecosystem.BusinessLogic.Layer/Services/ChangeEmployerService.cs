using Microsoft.EntityFrameworkCore;
using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Operator;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Services
{
    public class ChangeEmployerService : IChangeEmployerService
    {
        private readonly IUnitOfWork _iUnitOfWork;
        private readonly IEmployeeService _employeeService;

        public ChangeEmployerService(IUnitOfWork iUnitOfWork, IEmployeeService employeeService)
        {
            _iUnitOfWork = iUnitOfWork;
            _employeeService = employeeService;
        }

        #region Retrieve data
        public async Task<TData<List<ChangeEmployerEntity>>> GetList(ChangeEmployerListParam param)
        {
            TData<List<ChangeEmployerEntity>> obj = new TData<List<ChangeEmployerEntity>>();
            obj.Data = await _iUnitOfWork.ChangeEmployers.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        //public async Task<TData<List<ChangeEmployerEntity>>> GetPageList(ChangeEmployerListParam param, Pagination pagination)
        //{
        //    TData<List<ChangeEmployerEntity>> obj = new TData<List<ChangeEmployerEntity>>();
        //    obj.Data = await _iUnitOfWork.ChangeEmployers.GetPageList(param, pagination);
        //    if (obj.Data.Count > 0)
        //    {
        //        List<CompanyEntity> companyList = await _iUnitOfWork.Companies.GetList(new CompanyListParam { Ids = obj.Data.Select(p => p.Company).ToList() });
        //        foreach (ChangeEmployerEntity changeEmployer in obj.Data)
        //        {
        //            changeEmployer.CompanyName = companyList.Where(p => p.Id == changeEmployer.Company).Select(p => p.Name).FirstOrDefault();
        //        }
        //    }
        //    obj.Total = pagination.TotalCount;
        //    obj.Tag = 1;
        //    return obj;
        //}

        public async Task<TData<List<ChangeEmployerEntity>>> GetPageList(ChangeEmployerListParam param, Pagination pagination)
        {
            var DB = new ApplicationDbContext();
            var user = await Operator.Instance.Current();
            var employeeDetails = DB.EmployeeEntity.Where(i => i.Id == user.Employee).FirstOrDefault();

            TData<List<ChangeEmployerEntity>> obj = new TData<List<ChangeEmployerEntity>>();
            obj.Data = await _iUnitOfWork.ChangeEmployers.GetPageList(param, pagination);
            obj.Data = obj.Data.Where(changeEmployer => changeEmployer.NhfNumber == employeeDetails.NHFNumber.ToString()).ToList();

            if (obj.Data.Count > 0)
            {
                List<CompanyEntity> companyList = await _iUnitOfWork.Companies.GetList(new CompanyListParam { Ids = obj.Data.Select(p => p.Company).ToList() });
                foreach (ChangeEmployerEntity changeEmployer in obj.Data)
                {
                    changeEmployer.CompanyName = companyList.Where(p => p.Id == changeEmployer.Company).Select(p => p.Name).FirstOrDefault();
                }
            }
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }


        public async Task<TData<List<ZtreeInfo>>> GetZtreeChangeEmployerList(ChangeEmployerListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<ChangeEmployerEntity> changeEmployerList = await _iUnitOfWork.ChangeEmployers.GetList(param);
            foreach (ChangeEmployerEntity changeEmployer in changeEmployerList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = changeEmployer.Id,
                });
            }
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ZtreeInfo>>> GetZtreeUserList(ChangeEmployerListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<ChangeEmployerEntity> changeEmployerList = await _iUnitOfWork.ChangeEmployers.GetList(param);
            List<UserEntity> userList = await _iUnitOfWork.Users.GetList(null);
            foreach (ChangeEmployerEntity changeEmployer in changeEmployerList)
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

        public async Task<TData<ChangeEmployerEntity>> GetEntity(long id)
        {
            TData<ChangeEmployerEntity> obj = new TData<ChangeEmployerEntity>();
            obj.Data = await _iUnitOfWork.ChangeEmployers.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<int>> GetMaxSort()
        {
            TData<int> obj = new TData<int>();
            obj.Data = await _iUnitOfWork.ChangeEmployers.GetMaxSort();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<CustomerDetailsViewModel>> GetCompanyName()
        {
            TData<CustomerDetailsViewModel> obj = new TData<CustomerDetailsViewModel>();
            var user = await Operator.Instance.Current();
            var companyDetails = await _iUnitOfWork.Companies.GetEntity(user.EmployeeInfo.Company);
            var customerDetails = await _employeeService.GetEntityByNhfNo(user.EmployeeInfo.NHFNumber);

            var custDetails = new CustomerDetailsViewModel
            {

                EmployerName = companyDetails.Name,
                EmployerNo = companyDetails.Id.ToString(),
                Nhfno = customerDetails.Data.NHFNumber.ToStr(),
            };

            obj.Data = custDetails;
            obj.Tag = 1;
            return obj;
        }


        #endregion

        #region Submit data
        public async Task<TData<string>> SaveForm(ChangeEmployerEntity entity)
        {
            TData<string> obj = new TData<string>();

           
            try
            {
                var DB = new ApplicationDbContext();
                var user = await Operator.Instance.Current();
                var customerDetails = await _employeeService.GetEntity(user.Employee);
                var newCompanyDetails = DB.CompanyEntity.Where(i => i.Id == entity.Company).FirstOrDefault();

                //entity.CompanyName = newCompanyDetails.Name;
                entity.NewEmployer = newCompanyDetails.Name;

                if (customerDetails == null)
                {
                    obj.Tag = 0;
                    obj.Message = "Customer details not found.";
                    return obj;
                }

                if (entity.OldEmployer == entity.NewEmployer)
                {
                    obj.Message = "Please select a different employer than your current company";
                    return obj;
                }

                // Update the customerDetails object

                // Save the ChangeEmployerEntity
                await _iUnitOfWork.ChangeEmployers.SaveForm(entity);
               
                // Now update the Company of the EmployeeEntity
                var employeeDetails = DB.EmployeeEntity.Where(i => i.Id == user.Employee).FirstOrDefault();
                employeeDetails.Company = entity.Company;
                employeeDetails.CompanyName = newCompanyDetails.Name;

                // Now update the Company of the NextOfKin
                var nok = DB.NextOfKinEntity.Where(i => i.Employee == user.Employee).FirstOrDefault();
                nok.Company = entity.Company;
                DB.SaveChanges();

                var User = DB.UserEntity.Where(i => i.Employee == user.Employee).FirstOrDefault();
                User.Company = entity.Company;
                //DB.UserEntity.Update(User);
                DB.SaveChanges();

                obj.Data = entity.Id.ParseToString();
                obj.Tag = 1;
                obj.Message = "Employer Updated Successfully";
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine($"Exception: {ex}");
                obj.Tag = -1;
                obj.Message = "An error occurred while updating the change employer.";
            }
            return obj;
        }


        public async Task<TData> DeleteForm(string ids)
        {
            TData<long> obj = new TData<long>();
            await _iUnitOfWork.ChangeEmployers.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<string>> UpdateForm(ChangeEmployerEntity entity)
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
            //existingEntity.EmployerNo = entity.CurrentEmployerNo;
            //existingEntity.Employer = entity.CurrentEmployer;           


            // Save the changes to the database
            await _iUnitOfWork.Employees.SaveForm(existingEntity);
            await _iUnitOfWork.ChangeEmployers.SaveForm(entity);

            obj.Data = existingEntity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }



        #endregion
    }
}
