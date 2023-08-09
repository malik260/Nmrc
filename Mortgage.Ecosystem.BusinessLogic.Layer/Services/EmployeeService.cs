using Mortgage.Ecosystem.BusinessLogic.Layer.Cache;
using Mortgage.Ecosystem.BusinessLogic.Layer.Helpers;
using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
using Mortgage.Ecosystem.DataAccess.Layer.Enums;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public EmployeeService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        #region Retrieve data
        public async Task<TData<List<EmployeeEntity>>> GetList(EmployeeListParam param)
        {
            TData<List<EmployeeEntity>> obj = new TData<List<EmployeeEntity>>();
            obj.Data = await _iUnitOfWork.Employees.GetList(param);
            if (obj.Data.Count > 0)
            {
                foreach (EmployeeEntity employee in obj.Data)
                {
                    employee.FullName = $"{employee.LastName} {employee.FirstName}";
                }
            }
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<EmployeeEntity>>> GetPageList(EmployeeListParam param, Pagination pagination)
        {
            TData<List<EmployeeEntity>> obj = new TData<List<EmployeeEntity>>();
            obj.Data = await _iUnitOfWork.Employees.GetPageList(param, pagination);
            if (obj.Data.Count > 0)
            {
                List<CompanyEntity> companyList = await _iUnitOfWork.Companies.GetList(new CompanyListParam { Ids = obj.Data.Select(p => p.Company).ToList() });
                List<BranchEntity> branchList = await _iUnitOfWork.Branches.GetList(new BranchListParam { Ids = obj.Data.Select(p => p.Branch).ToList() });
                List<DepartmentEntity> departmentList = await _iUnitOfWork.Departments.GetList(new DepartmentListParam { Ids = obj.Data.Select(p => p.Department).ToList() });
                List<TitleEntity> titleList = await _iUnitOfWork.Titles.GetList(new TitleListParam { Ids = obj.Data.Select(p => p.Title).ToList() });
                List<GenderEntity> genderList = await _iUnitOfWork.Genders.GetList(new GenderListParam { Ids = obj.Data.Select(p => p.Gender).ToList() });
                List<MaritalStatusEntity> maritalStatusList = await _iUnitOfWork.MaritalStatus.GetList(new MaritalStatusListParam { Ids = obj.Data.Select(p => p.MaritalStatus).ToList() });
                List<BankEntity> bankList = await _iUnitOfWork.Banks.GetList(new BankListParam { Codes = obj.Data.Select(p => p.CustomerBank.ToStr()).ToList() });
                List<AccountTypeEntity> accountTypeList = await _iUnitOfWork.AccountTypes.GetList(new AccountTypeListParam { Ids = obj.Data.Select(p => p.AccountType).ToList() });
                List<AlertTypeEntity> alertTypeList = await _iUnitOfWork.AlertTypes.GetList(new AlertTypeListParam { Ids = obj.Data.Select(p => p.AlertType).ToList() });
                foreach (EmployeeEntity employee in obj.Data)
                {
                    employee.CompanyName = companyList.Where(p => p.Id == employee.Company).Select(p => p.Name).FirstOrDefault();
                    employee.BranchName = branchList.Where(p => p.Id == employee.Branch && p.Company == employee.Company).Select(p => p.Name).FirstOrDefault();
                    employee.DepartmentName = departmentList.Where(p => p.Id == employee.Department && p.Company == employee.Company && p.Branch == employee.Branch).Select(p => p.Name).FirstOrDefault();
                    employee.TitleName = titleList.Where(p => p.Id == employee.Title).Select(p => p.Name).FirstOrDefault();
                    employee.GenderName = genderList.Where(p => p.Id == employee.Gender).Select(p => p.Name).FirstOrDefault();
                    employee.MaritalStatusName = maritalStatusList.Where(p => p.Id == employee.MaritalStatus).Select(p => p.Name).FirstOrDefault();
                    employee.BankName = bankList.Where(p => p.Code == employee.CustomerBank).Select(p => p.Name).FirstOrDefault();
                    employee.AccountTypeName = accountTypeList.Where(p => p.Id == employee.AccountType).Select(p => p.Name).FirstOrDefault();
                    employee.AlertTypeName = alertTypeList.Where(p => p.Id == employee.AlertType).Select(p => p.Name).FirstOrDefault();
                }
            }
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<EmployeeEntity>>> GetApprovalPageList(EmployeeListParam param, Pagination pagination)
        {
            TData<List<EmployeeEntity>> obj = new TData<List<EmployeeEntity>>();
            obj.Data = await _iUnitOfWork.Employees.GetApprovalPageList(param, pagination);
            if (obj.Data.Count > 0)
            {
                List<CompanyEntity> companyList = await _iUnitOfWork.Companies.GetList(new CompanyListParam { Ids = obj.Data.Select(p => p.Company).ToList() });
                List<BranchEntity> branchList = await _iUnitOfWork.Branches.GetList(new BranchListParam { Ids = obj.Data.Select(p => p.Branch).ToList() });
                List<DepartmentEntity> departmentList = await _iUnitOfWork.Departments.GetList(new DepartmentListParam { Ids = obj.Data.Select(p => p.Department).ToList() });
                List<TitleEntity> titleList = await _iUnitOfWork.Titles.GetList(new TitleListParam { Ids = obj.Data.Select(p => p.Title).ToList() });
                List<GenderEntity> genderList = await _iUnitOfWork.Genders.GetList(new GenderListParam { Ids = obj.Data.Select(p => p.Gender).ToList() });
                List<MaritalStatusEntity> maritalStatusList = await _iUnitOfWork.MaritalStatus.GetList(new MaritalStatusListParam { Ids = obj.Data.Select(p => p.MaritalStatus).ToList() });
                List<BankEntity> bankList = await _iUnitOfWork.Banks.GetList(new BankListParam { Codes = obj.Data.Select(p => p.CustomerBank.ToStr()).ToList() });
                List<AccountTypeEntity> accountTypeList = await _iUnitOfWork.AccountTypes.GetList(new AccountTypeListParam { Ids = obj.Data.Select(p => p.AccountType).ToList() });
                List<AlertTypeEntity> alertTypeList = await _iUnitOfWork.AlertTypes.GetList(new AlertTypeListParam { Ids = obj.Data.Select(p => p.AlertType).ToList() });
                foreach (EmployeeEntity employee in obj.Data)
                {
                    employee.CompanyName = companyList.Where(p => p.Id == employee.Company).Select(p => p.Name).FirstOrDefault();
                    employee.BranchName = branchList.Where(p => p.Id == employee.Branch && p.Company == employee.Company).Select(p => p.Name).FirstOrDefault();
                    employee.DepartmentName = departmentList.Where(p => p.Id == employee.Department && p.Company == employee.Company && p.Branch == employee.Branch).Select(p => p.Name).FirstOrDefault();
                    employee.TitleName = titleList.Where(p => p.Id == employee.Title).Select(p => p.Name).FirstOrDefault();
                    employee.GenderName = genderList.Where(p => p.Id == employee.Gender).Select(p => p.Name).FirstOrDefault();
                    employee.MaritalStatusName = maritalStatusList.Where(p => p.Id == employee.MaritalStatus).Select(p => p.Name).FirstOrDefault();
                    employee.BankName = bankList.Where(p => p.Code == employee.CustomerBank).Select(p => p.Name).FirstOrDefault();
                    employee.AccountTypeName = accountTypeList.Where(p => p.Id == employee.AccountType).Select(p => p.Name).FirstOrDefault();
                    employee.AlertTypeName = alertTypeList.Where(p => p.Id == employee.AlertType).Select(p => p.Name).FirstOrDefault();
                }
            }
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ZtreeInfo>>> GetZtreeEmployeeList(EmployeeListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<EmployeeEntity> employeeList = await _iUnitOfWork.Employees.GetList(param);
            foreach (EmployeeEntity employee in employeeList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = employee.Id,
                    name = $"{employee.LastName} {employee.FirstName}"
                });
            }
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<EmployeeEntity>> GetEntity(long id)
        {
            TData<EmployeeEntity> obj = new TData<EmployeeEntity>();
            EmployeeEntity employeeEntity = await _iUnitOfWork.Employees.GetEntity(id);
            List<MenuAuthorizeEntity> menuAuthorizeList = await _iUnitOfWork.MenuAuthorizes.GetList(new MenuAuthorizeEntity
            {
                AuthorizeId = id,
                AuthorizeType = AuthorizeTypeEnum.User.ToInt()
            });
            // Get the permissions corresponding to the user
            employeeEntity.MenuIds = string.Join(",", menuAuthorizeList.Select(p => p.MenuId));

            obj.Data = employeeEntity;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<EmployeeEntity>> GetEntityByNhfNo(long nhfNo)
        {
            TData<EmployeeEntity> obj = new TData<EmployeeEntity>();
            EmployeeEntity employeeEntity = await _iUnitOfWork.Employees.GetEntityByNhfNumber(nhfNo);
            obj.Data = employeeEntity;
            obj.Tag = 1;
            return obj;
        }

        #endregion

        #region Submit data
        public async Task<TData<string>> SaveForm(EmployeeEntity entity)
        {
            TData<string> obj = new TData<string>();
            entity.EmploymentType = EmploymentTypeEnum.Employed.ToInt();
            entity.CompanyName = _iUnitOfWork.Companies.GetEntity(entity.Company).Result.Name;

            if (_iUnitOfWork.Employees.ExistEmployee(entity))
            {
                obj.Message = "Employee email address already exists!";
                return obj;
            }

            if (string.IsNullOrEmpty(entity.BVN))
            {
                obj.Message = "BVN must not be empty!";
                return obj;
            }
            else if (!ValidationHelper.ValidateBvn(entity.BVN))
            {
                obj.Message = "BVN must be digit and 11 in length!";
                return obj;
            }
            else if (_iUnitOfWork.Employees.ExistEmployeeBVN(entity))
            {
                obj.Message = "Employee BVN already exists!";
                return obj;
            }

            if (string.IsNullOrEmpty(entity.FirstName))
            {
                obj.Message = "First name must be provided!";
                return obj;
            }

            if (string.IsNullOrEmpty(entity.LastName))
            {
                obj.Message = "Last name must be provided!";
                return obj;
            }

            if (string.IsNullOrEmpty(entity.DateOfBirth))
            {
                obj.Message = "Date of birth must not be empty!";
                return obj;
            }

            if (!string.IsNullOrEmpty(entity.MenuIds))
            {
                entity.Salt = new UserService(_iUnitOfWork).GetPasswordSalt();
                entity.DecryptedPassword = new UserService(_iUnitOfWork).GenerateDefaultPassword();
                entity.Password = new UserService(_iUnitOfWork).EncryptUserPassword(entity.DecryptedPassword, entity.Salt);
            }

            entity.NHFNumber = _iUnitOfWork.Employees.GenerateNHFNumber();

            await _iUnitOfWork.Employees.SaveForm(entity);

            // Clear the permission data in the cache
            new MenuAuthorizeCache(_iUnitOfWork).Remove();

            obj.Data = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<string>> SaveForms(EmployeeEntity entity)
        {
            TData<string> obj = new TData<string>();

            if (_iUnitOfWork.Employees.ExistEmployee(entity))
            {
                obj.Message = "Employee email address already exists!";
                return obj;
            }

            if (string.IsNullOrEmpty(entity.BVN))
            {
                obj.Message = "BVN must not be empty!";
                return obj;
            }
            else if (!ValidationHelper.ValidateBvn(entity.BVN))
            {
                obj.Message = "BVN must be digit and 11 in length!";
                return obj;
            }
            else if (_iUnitOfWork.Employees.ExistEmployeeBVN(entity))
            {
                obj.Message = "Employee BVN already exists!";
                return obj;
            }

            if (string.IsNullOrEmpty(entity.FirstName))
            {
                obj.Message = "First name must be provided!";
                return obj;
            }

            if (string.IsNullOrEmpty(entity.LastName))
            {
                obj.Message = "Last name must be provided!";
                return obj;
            }

            if (string.IsNullOrEmpty(entity.DateOfBirth))
            {
                obj.Message = "Date of birth must not be empty!";
                return obj;
            }

            if (entity.EmploymentType < 1)
            {
                obj.Message = "Employment type must be selected!";
                return obj;
            }
            else if (entity.EmploymentType == EmploymentTypeEnum.Employed.ToInt())
            {
                if (string.IsNullOrEmpty(entity.CoyName))
                {
                    obj.Message = "Company name must be provided!";
                    return obj;
                }
                else if (string.IsNullOrEmpty(entity.CoyAddress))
                {
                    obj.Message = "Company address must be provided!";
                    return obj;
                }
                else if (string.IsNullOrEmpty(entity.CoyRCNumber))
                {
                    obj.Message = "Company RC-Number must be provided!";
                    return obj;
                }
                else if (entity.CoySector < 1)
                {
                    obj.Message = "Company sector must be selected!";
                    return obj;
                }
            }

            if (string.IsNullOrEmpty(entity.UserName))
            {
                obj.Message = "Login : User name must be provided!";
                return obj;
            }

            if (entity.Role.IsNullOrZero())
            {
                obj.Message = "Login : Role must be selected!";
                return obj;
            }
            else
            {
                entity.Salt = new UserService(_iUnitOfWork).GetPasswordSalt();
                entity.DecryptedPassword = new UserService(_iUnitOfWork).GenerateDefaultPassword();
                entity.Password = new UserService(_iUnitOfWork).EncryptUserPassword(entity.DecryptedPassword, entity.Salt);
            }

            entity.NHFNumber = _iUnitOfWork.Employees.GenerateNHFNumber();

            await _iUnitOfWork.Employees.SaveForms(entity);
            obj.Data = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData<long> obj = new TData<long>();
            await _iUnitOfWork.Employees.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion        
    }
}
