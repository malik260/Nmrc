using Microsoft.AspNetCore.Mvc;
using Mortgage.Ecosystem.BusinessLogic.Layer.Cache;
using Mortgage.Ecosystem.BusinessLogic.Layer.Helpers;
using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.BusinessLogic.Layer.Resources;
using Mortgage.Ecosystem.BusinessLogic.Layer.Response;
using Mortgage.Ecosystem.DataAccess.Layer;
using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
using Mortgage.Ecosystem.DataAccess.Layer.Enums;
using Mortgage.Ecosystem.DataAccess.Layer.Helpers;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Operator;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;
using Mortgage.Ecosystem.DataAccess.Layer.Request;
using MySqlX.XDevAPI;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using static Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos.LoanApplicationDTO;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _iUnitOfWork;
        private readonly IApprovalSetupService _approveSetupService;

        public EmployeeService(IUnitOfWork iUnitOfWork, IApprovalSetupService approveSetupService)
        {
            _iUnitOfWork = iUnitOfWork;
            _approveSetupService = approveSetupService;
        }

        #region Retrieve data
        //public async Task<TData<List<EmployeeEntity>>> GetList(EmployeeListParam param)
        //{
        //    TData<List<EmployeeEntity>> obj = new TData<List<EmployeeEntity>>();
        //    obj.Data = await _iUnitOfWork.Employees.GetList(param);
        //    if (obj.Data.Count > 0)
        //    {
        //        foreach (EmployeeEntity employee in obj.Data)
        //        {
        //            employee.FullName = $"{employee.LastName} {employee.FirstName}";
        //        }
        //    }
        //    obj.Total = obj.Data.Count;
        //    obj.Tag = 1;
        //    return obj;
        //}

        public async Task<TData<List<EmployeeEntity>>> GetList(EmployeeListParam param)
        {
            var user = await Operator.Instance.Current();
            TData<List<EmployeeEntity>> obj = new TData<List<EmployeeEntity>>();

            // Retrieve the list of all employees from the database
            obj.Data = _iUnitOfWork.Employees.GetListByCompany(param).Result.Where(i => i.Company == param.Company).ToList();


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

        public async Task<TData<List<EmployeeEntity>>> GetList2(EmployeeListParam param)
        {
            var user = await Operator.Instance.Current();
            TData<List<EmployeeEntity>> obj = new TData<List<EmployeeEntity>>();

            // Retrieve the list of all employees from the database
            obj.Data = _iUnitOfWork.Employees.GetListByCompany(param).Result.Where(i => i.Company == user.Company).ToList();


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
            var loginCompany = await Operator.Instance.Current();
            TData<List<EmployeeEntity>> obj = new TData<List<EmployeeEntity>>();
            param.Company = loginCompany.Company;
            obj.Data = await _iUnitOfWork.Employees.GetPageList(param, pagination);
             if ((obj.Data.Count) > 0 && (!string.IsNullOrEmpty(param.FirstName)))
            {
                obj.Data = obj.Data.Where(i => i.FirstName.Trim().ToLower() == param.FirstName.Trim().ToLower()).DefaultIfEmpty().ToList();
                if (obj.Data[0] == null)
                {
                    obj.Data = new List<EmployeeEntity>();
                    obj.Total = 0;
                    obj.Tag = 1;
                    return obj;
                }
            }
            if ((obj.Data.Count) > 0 && (!string.IsNullOrEmpty(param.LastName)))
            {
                obj.Data = obj.Data.Where(i => i.LastName.Trim().ToLower() == param.LastName.Trim().ToLower()).DefaultIfEmpty().ToList();
                if (obj.Data[0] == null)
                {
                    obj.Data = new List<EmployeeEntity>();
                    obj.Total = 0;
                    obj.Tag = 1;
                    return obj;
                }
            }

            if ((obj.Data.Count) > 0 && (!string.IsNullOrEmpty(param.FirstName)) && (!string.IsNullOrEmpty(param.LastName)))
            {
                obj.Data = obj.Data.Where(i => i.LastName.Trim().ToLower() == param.LastName.Trim().ToLower() && i.FirstName.Trim().ToLower() == param.FirstName.Trim().ToLower()).DefaultIfEmpty().ToList();
                if (obj.Data[0] == null)
                {
                    obj.Data = new List<EmployeeEntity>();
                    obj.Total = 0;
                    obj.Tag = 1;
                    return obj;
                }
            }
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
                List<StateEntity> stateList = await _iUnitOfWork.States.GetList(new StateListParam { Ids = obj.Data.Select(p => p.ContributionBranch).ToList() });
                foreach (EmployeeEntity employee in obj.Data)
                {
                    employee.CompanyName = companyList.Where(p => p.Id == employee.Company).Select(p => p.Name).FirstOrDefault();
                    employee.BranchName = branchList.Where(p => p.Id == employee.Branch && p.Company == employee.Company).Select(p => p.Name).FirstOrDefault();
                    employee.DepartmentName = departmentList.Where(p => p.Id == employee.Department && p.Company == employee.Company && p.Branch == employee.Branch).Select(p => p.Name).FirstOrDefault();
                    employee.TitleName = titleList.Where(p => p.Id == employee.Title).Select(p => p.Name).FirstOrDefault();
                    employee.GenderName = genderList.Where(p => p.Id == employee.Gender).Select(p => p.Name).FirstOrDefault();
                    employee.MaritalStatusName = maritalStatusList.Where(p => p.Id == employee.MaritalStatus).Select(p => p.Name).FirstOrDefault();
                    employee.BankName = bankList.Where(p => p.Code == employee.CustomerBank).Select(p => p.Name).FirstOrDefault();
                    employee.ContributionBranchName = stateList.Where(p => p.Id == employee.ContributionBranch).Select(p => p.Name).FirstOrDefault();
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
            if ((obj.Data.Count) > 0 && (!string.IsNullOrEmpty(param.FirstName)))
            {
                obj.Data = obj.Data.Where(i => i.FirstName.Trim().ToLower() == param.FirstName.Trim().ToLower()).DefaultIfEmpty().ToList();
                if (obj.Data[0] == null)
                {
                    obj.Data = new List<EmployeeEntity>();
                    obj.Total = 0;
                    obj.Tag = 1;
                    return obj;
                }
            }
            if ((obj.Data.Count) > 0 && (!string.IsNullOrEmpty(param.LastName)))
            {
                obj.Data = obj.Data.Where(i => i.LastName.Trim().ToLower() == param.LastName.Trim().ToLower()).DefaultIfEmpty().ToList();
                if (obj.Data[0] == null)
                {
                    obj.Data = new List<EmployeeEntity>();
                    obj.Total = 0;
                    obj.Tag = 1;
                    return obj;
                }
            }

            if ((obj.Data.Count) > 0 && (!string.IsNullOrEmpty(param.FirstName)) && (!string.IsNullOrEmpty(param.LastName)))
            {
                obj.Data = obj.Data.Where(i => i.LastName.Trim().ToLower() == param.LastName.Trim().ToLower() && i.FirstName.Trim().ToLower() == param.FirstName.Trim().ToLower()).DefaultIfEmpty().ToList();
                if (obj.Data[0] == null)
                {
                    obj.Data = new List<EmployeeEntity>();
                    obj.Total = 0;
                    obj.Tag = 1;
                    return obj;
                }
            }

            if (obj.Data.Count > 0)
            {
                List<CompanyEntity> companyList = await _iUnitOfWork.Companies.GetList(new CompanyListParam { Ids = obj.Data.Select(p => p.Company).ToList() });
                List<BranchEntity> branchList = await _iUnitOfWork.Branches.GetList(new BranchListParam { Ids = obj.Data.Select(p => p.Branch).ToList() });
                List<DepartmentEntity> departmentList = await _iUnitOfWork.Departments.GetList(new DepartmentListParam { Ids = obj.Data.Select(p => p.Department).ToList() });
                List<TitleEntity> titleList = await _iUnitOfWork.Titles.GetList(new TitleListParam { Ids = obj.Data.Select(p => p.Title).ToList() });
                List<GenderEntity> genderList = await _iUnitOfWork.Genders.GetList(new GenderListParam { Ids = obj.Data.Select(p => p.Gender).ToList() });
                List<MaritalStatusEntity> maritalStatusList = await _iUnitOfWork.MaritalStatus.GetList(new MaritalStatusListParam { Ids = obj.Data.Select(p => p.MaritalStatus).ToList() });
                List<BankEntity> bankList = await _iUnitOfWork.Banks.GetList(new BankListParam { Codes = obj.Data.Select(p => p.CustomerBank.ToStr()).ToList() });
                List<AccountTypeEntity> accountTypeList = await _iUnitOfWork.AccountTypes.GetList(new AccountTypeListParam { Ids = obj.Data.Select(p => p.AccountType).ToList() }); List<AlertTypeEntity> alertTypeList = await _iUnitOfWork.AlertTypes.GetList(new AlertTypeListParam { Ids = obj.Data.Select(p => p.AlertType).ToList() });
                List<StateEntity> stateList = await _iUnitOfWork.States.GetList(new StateListParam { Ids = obj.Data.Select(p => p.ContributionBranch).ToList() });
                foreach (EmployeeEntity employee in obj.Data)
                {
                    employee.CompanyName = companyList.Where(p => p.Id == employee.Company).Select(p => p.Name).FirstOrDefault();
                    employee.BranchName = branchList.Where(p => p.Id == employee.Branch && p.Company == employee.Company).Select(p => p.Name).FirstOrDefault();
                    employee.DepartmentName = departmentList.Where(p => p.Id == employee.Department && p.Company == employee.Company && p.Branch == employee.Branch).Select(p => p.Name).FirstOrDefault();
                    employee.TitleName = titleList.Where(p => p.Id == employee.Title).Select(p => p.Name).FirstOrDefault();
                    employee.GenderName = genderList.Where(p => p.Id == employee.Gender).Select(p => p.Name).FirstOrDefault();
                    employee.MaritalStatusName = maritalStatusList.Where(p => p.Id == employee.MaritalStatus).Select(p => p.Name).FirstOrDefault();
                    employee.BankName = bankList.Where(p => p.Code == employee.CustomerBank).Select(p => p.Name).FirstOrDefault();
                    employee.ContributionBranchName = stateList.Where(p => p.Id == employee.ContributionBranch).Select(p => p.Name).FirstOrDefault();
                    employee.AccountTypeName = accountTypeList.Where(p => p.Id == employee.AccountType).Select(p => p.Name).FirstOrDefault();
                    employee.AlertTypeName = alertTypeList.Where(p => p.Id == employee.AlertType).Select(p => p.Name).FirstOrDefault();
                    employee.TitleName = employee.TitleName.IsNotNull() ? employee.TitleName : string.Empty;
                    employee.FullName = $"{employee.TitleName} {employee.FirstName} {employee.LastName}";
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
        public async Task<EmployeeEntity> GetEntityByNhf(long nhfNo)
        {
            EmployeeEntity obj = new EmployeeEntity();
            EmployeeEntity employeeEntity = await _iUnitOfWork.Employees.GetEntityByNhfNumber(nhfNo);
            return employeeEntity;
        }

        #endregion

        #region Submit data
        public async Task<TData<string>> SaveForm(EmployeeEntity entity)
        {
            TData<string> obj = new TData<string>();
            EmployeeListParam employeeListParam = new EmployeeListParam();
            entity.EmploymentType = EmploymentTypeEnum.Employed.ToInt();
            entity.CompanyName = _iUnitOfWork.Companies.GetEntity(entity.Company).Result.Name;
            entity.EmployerNhfNumber = _iUnitOfWork.Companies.GetEntity(entity.Company).Result.EmployerNhfNumber;
            if (_iUnitOfWork.Users.CheckUserName(entity.EmailAddress))
            {
                obj.Message = "Email already exists!";
                return obj;
            }

            var emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            if (!Regex.IsMatch(entity.EmailAddress, emailPattern))
            {
                obj.Message = "Please provide a valid Email Address!";
                return obj;
            }

            if (string.IsNullOrEmpty(entity.PostalAddress))
            {
                obj.Message = "Employee address must be provided!";
                return obj;
            }

            if (string.IsNullOrEmpty(entity.EmailAddress))
            {
                obj.Message = "Employee email address must be provided!";
                return obj;
            }

            if (string.IsNullOrEmpty(entity.MobileNumber))
            {
                obj.Message = "Mobile Number must be provided!";
                return obj;
            }
            else if (entity.MobileNumber.Length != 11)
            {
                obj.Message = "Mobile Number must be 11 digits long!";
                return obj;
            }

            if (string.IsNullOrEmpty(entity.DateOfBirth))
            {
                obj.Message = "Date of birth must not be empty!";
                return obj;
            }
            if (entity.NIN.Length != 11)
            {
                obj.Message = "National Identification Number must be 11 digits long!";
                return obj;
            }

            if (entity.DateOfEmployment == null || entity.DateOfEmployment == DateTime.MinValue)
            {
                obj.Message = "Please Provide Employment Date!";
                return obj;
            }

            if (string.IsNullOrEmpty(entity.KinAddress))
            {
                obj.Message = " Next of Kin Address must be provided!";
                return obj;
            }

            if (string.IsNullOrEmpty(entity.KinFirstName))
            {
                obj.Message = " Next of Kin First Name must be provided!";
                return obj;
            }

            if (string.IsNullOrEmpty(entity.KinLastName))
            {
                obj.Message = " Next of Kin Last Name must be provided!";
                return obj;
            }

            if (string.IsNullOrEmpty(entity.KinMobileNumber))
            {
                obj.Message = " Next of Kin Mobile must be provided!";
                return obj;
            }


            if (_iUnitOfWork.Employees.ExistEmployee(entity))
            {
                obj.Message = "Employee email address already exists!";
                return obj;
            }

            if (entity.BVN.IsNotNull() && !ValidationHelper.ValidateBvn(entity.BVN))
            {
                obj.Message = "BVN must be digit and 11 in length!";
                return obj;
            }
            else if (_iUnitOfWork.Employees.ExistEmployeeBVN(entity))
            {
                obj.Message = "Employee BVN already exists!";
                return obj;
            }
            if (_iUnitOfWork.Users.CheckUserName(entity.EmailAddress))
            {
                obj.Message = "Email already exists!";
                return obj;
            }
            var mobileExist = await _iUnitOfWork.Employees.GetEmployeeByMobile(entity.MobileNumber);
            if (mobileExist != null)
            {
                obj.Message = "Mobile Number already exist!";
                return obj;
            }

            var EmailExist = await _iUnitOfWork.Employees.GetEmployeeByEmail(entity?.EmailAddress);
            if (EmailExist != null)
            {
                obj.Message = "Email Address already exist!";
                return obj;
            }

            var BvnExist = await _iUnitOfWork.Employees.GetEmployeeByBVN(entity?.BVN);
            if (BvnExist != null)
            {
                obj.Message = "BVN already exist!";
                return obj;
            }
            var NinExist = await _iUnitOfWork.Employees.GetEmployeeByNIN(entity?.NIN);
            if (NinExist != null)
            {
                obj.Message = "NIN already exist!";
                return obj;
            }

            CusDetailsToCheck cusdet = new CusDetailsToCheck();
            cusdet.BVN = entity.BVN;
            cusdet.EmailAddress = entity.EmailAddress;
            cusdet.MobileNo = entity.MobileNumber;
            var EmployeeInfo = await IntegrateCustomerExis(cusdet);
            if (EmployeeInfo.responseCode != "200")
            {
                obj.Message = EmployeeInfo.message;
                obj.Tag = 0;
                return obj;

            }



            if (entity.BankAccountNumber.IsNotNull() && !ValidationHelper.ValidateAccountNumber(entity.BankAccountNumber))
            {
                obj.Message = "Bank account number must be digit and 10 in length!";
                return obj;
            }
            //else if (_iUnitOfWork.Employees.ExistEmployeeAccountNumber(entity))
            //{
            //    obj.Message = "Employee Account Number already exists!";
            //    return obj;
            //}

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


            entity.Salt = new UserService(_iUnitOfWork).GetPasswordSalt();
            entity.DecryptedPassword = new UserService(_iUnitOfWork).GenerateDefaultPassword();
            entity.Password = EncryptionHelper.Encrypt(entity.DecryptedPassword, entity.Salt);
            entity.BaseProcessMenu = await new DataRepository().GetMenuId(GlobalConstant.EMPLOYEE_MENU_URL);
            entity.BaseCreateTime = DateTime.Now;
            await _iUnitOfWork.Employees.SaveForm(entity);
            var message = string.Empty;
            var AuthorityInfo = new ApprovalSetupListParam();
            AuthorityInfo.MenuId = entity.BaseProcessMenu;
            var approveMenu = await _iUnitOfWork.ApprovalSetups.GetList(AuthorityInfo);
            foreach (var item in approveMenu)
            {
                var authorityEmail = await _iUnitOfWork.Employees.GetById(item.Authority);
                if (authorityEmail != null)
                {
                    MailParameter mailParameter = new()
                    {
                        RegistrationApprover = authorityEmail.FirstName + " " + authorityEmail.LastName,
                        UserEmail = authorityEmail.EmailAddress,
                        UserName = entity.FirstName + " " + entity.LastName,
                        UserCompany = entity.CompanyName,
                    };

                    var sendMail = EmailHelper.IsEmployeeApprovalRegistrationSent(mailParameter, out message);
                }

            }
            var approveCompany = await _iUnitOfWork.ApprovalSetups.GetEntity(entity.Company);
            // Clear the permission data in the cache
            new MenuAuthorizeCache(_iUnitOfWork).Remove();

            obj.Data = entity.Id.ParseToString();
            obj.Message = "Employee Created Successfully";
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

            if (entity.BVN.IsNotNull() && !ValidationHelper.ValidateBvn(entity.BVN))
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
            if (_iUnitOfWork.Users.CheckUserName(entity.EmailAddress))
            {
                obj.Message = "Email already exists!";
                return obj;
            }
            CusDetailsToCheck cusdet = new CusDetailsToCheck();
            cusdet.BVN = entity.BVN;
            cusdet.EmailAddress = entity.EmailAddress;
            cusdet.MobileNo = entity.MobileNumber;
            var EmployeeInfo = await IntegrateCustomerExis(cusdet);
            if (EmployeeInfo.responseCode != "200")
            {
                obj.Message = EmployeeInfo.message;
                obj.Tag = 0;
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
                if (string.IsNullOrEmpty(entity.Company.ToString()))
                {
                    obj.Message = "Company name must be provided!";
                    return obj;
                }
                //    else if (string.IsNullOrEmpty(entity.CoyAddress))
                //    {
                //        obj.Message = "Company address must be provided!";
                //        return obj;
                //    }
                //    else if (string.IsNullOrEmpty(entity.CoyRCNumber))
                //    {
                //        obj.Message = "Company RC-Number must be provided!";
                //        return obj;
                //    }
                //    else if (entity.CoySector < 1)
                //    {
                //        obj.Message = "Company sector must be selected!";
                //        return obj;
                //    }
                //}

                //if (string.IsNullOrEmpty(entity.UserName))
                //{
                //    obj.Message = "Login : User name must be provided!";
                //    return obj;
                //}

                //if (entity.Role.IsNullOrZero())
                //{
                //    obj.Message = "Login : Role must be selected!";
                //    return obj;
            }
            else
            {
                entity.Salt = new UserService(_iUnitOfWork).GetPasswordSalt();
                entity.DecryptedPassword = new UserService(_iUnitOfWork).GenerateDefaultPassword();
                //entity.Password = new UserService(_iUnitOfWork).EncryptUserPassword(entity.DecryptedPassword, entity.Salt);
                entity.Password = EncryptionHelper.Encrypt(entity.DecryptedPassword, entity.Salt);
            }

            entity.NHFNumber = _iUnitOfWork.Employees.GenerateNHFNumber();

            await _iUnitOfWork.Employees.SaveForms(entity);
            obj.Data = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }
        //public async Task<TData<EmployeeEntity>> GetEntityByNhfNo(long nhfNo)
        //{
        //    TData<EmployeeEntity> obj = new TData<EmployeeEntity>();
        //    EmployeeEntity employeeEntity = await _iUnitOfWork.Employees.GetEntityByNhfNumber(nhfNo);
        //    obj.Data = employeeEntity;
        //    obj.Tag = 1;
        //    return obj;
        //}
        public async Task<TData> DeleteForm(string ids)
        {
            TData<long> obj = new TData<long>();
            await _iUnitOfWork.Employees.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }

        [HttpPost]
        private async Task<CustomerCreationResponses> IntegrateEmployeeToCore(NhfEmployeeTemp Employee)
        {
            try
            {
                var _client = new HttpClient();
                var serializeObject = JsonConvert.SerializeObject(Employee);

                var jsonPayload = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(Employee), Encoding.UTF8, ApiResource.ApplicationJson);

                var response = await _client.PostAsync("https://testcorebanking.fmbn.gov.ng:5000/api/v2/Customer/AddEmployees", jsonPayload);


                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<CustomerCreationResponses>(responseContent);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpPost]
        private async Task<CustomerCreationResponses> IntegrateCustomerExis(CusDetailsToCheck Employee)
        {
            try
            {
                var _client = new HttpClient();
                var serializeObject = JsonConvert.SerializeObject(Employee);

                var jsonPayload = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(Employee), Encoding.UTF8, ApiResource.ApplicationJson);

                var response = await _client.PostAsync("https://testcorebanking.fmbn.gov.ng:5000/api/v2/Customer/CheckCustomerExists", jsonPayload);


                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<CustomerCreationResponses>(responseContent);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }



        public async Task<TData> ApproveForm(EmployeeEntity entity)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            TData<long> obj = new TData<long>();
            var user = await Operator.Instance.Current();
            user.DecryptedPassword = EncryptionHelper.Decrypt(user.Password, user.Salt);
            var entityRecord = await _iUnitOfWork.Employees.GetEntity(entity.Id);
            var EmployerRecord = await _iUnitOfWork.Companies.GetEntity(entityRecord.Company);
            var NextofKinDetails = await _iUnitOfWork.NextOfKins.GetEntity(entity.Id);
            var menuRecord = await _iUnitOfWork.Menus.GetEntity(entityRecord.BaseProcessMenu);
            var loginProfile = await _iUnitOfWork.Users.GetEntity(entityRecord.Company, entityRecord.Id);
            loginProfile.DecryptedPassword = EncryptionHelper.Decrypt(loginProfile.Password, loginProfile.Salt);
            var approvalLogListParam = new ApprovalLogListParam()
            {
                Company = user.Company,
                MenuId = menuRecord.Id,
                Record = entity.Id
            };
            var approvalLogRecords = await _iUnitOfWork.ApprovalLogs.GetList(approvalLogListParam);
            menuRecord.ApprovalLogList = approvalLogRecords;
            NhfEmployeeTemp employeeTemp = new NhfEmployeeTemp();
            employeeTemp.Addressline1 = entityRecord.PostalAddress;
            employeeTemp.Addressline2 = entityRecord.PostalAddress;
            employeeTemp.Bankaccountno = entityRecord.BankAccountNumber;
            employeeTemp.Branchcode = "101";
            employeeTemp.Title = entityRecord.Title.ToString();
            employeeTemp.Firstname = entityRecord.FirstName;
            employeeTemp.Surname = entityRecord.LastName;
            employeeTemp.Othername = entityRecord.OtherName;
            employeeTemp.Registrationlocation = "101";
            employeeTemp.Contributionlocation = "101";
            employeeTemp.Bvn = entityRecord.BVN;
            employeeTemp.Employernumber = entityRecord.EmployerNhfNumber;
            employeeTemp.NextofkinFirstname = NextofKinDetails.FirstName;
            employeeTemp.Nextofkinphonenumber = NextofKinDetails.MobileNumber;
            employeeTemp.Nextofkinaddress = NextofKinDetails.Address;
            employeeTemp.Nextofkinsurname = NextofKinDetails.LastName;
            employeeTemp.Nin = entityRecord.NIN;
            employeeTemp.Approved = 1;
            employeeTemp.Dateofbirth = Convert.ToDateTime(entityRecord.DateOfBirth);
            employeeTemp.DateofEmployment = entityRecord.DateOfEmployment;
            employeeTemp.Gender = entityRecord.Gender.ToString();
            employeeTemp.Datecreated = DateTime.Now;
            employeeTemp.Dateapproved = DateTime.Now;
            employeeTemp.Createdby = "fintrak.user";
            employeeTemp.Monthlysalary = entityRecord.MonthlySalary.ToString();
            employeeTemp.AccountNumber = entityRecord.BankAccountNumber;
            employeeTemp.Bankname = entityRecord.BankName;
            employeeTemp.Emailaddress = entityRecord.EmailAddress;
            employeeTemp.Employeremail = EmployerRecord.EmailAddress;
            employeeTemp.Employermobile = EmployerRecord.MobileNumber;
            employeeTemp.Employername = EmployerRecord.Name;
            employeeTemp.Mobilenumber = entityRecord.MobileNumber;
            employeeTemp.Maritalstatus = entityRecord.MaritalStatus.ToString();
            var EmployeeInfo = await IntegrateEmployeeToCore(employeeTemp);
            if (EmployeeInfo.responseCode != "200")
            {
                obj.Message = "Employee Approval Failed: " + EmployeeInfo.message + "on Core Banking";
                obj.Data = entity.Id;
                obj.Tag = 0;
                return obj;

            }
            var EmployeeRecord = db.EmployeeEntity.Where(i => i.EmailAddress == entityRecord.EmailAddress).DefaultIfEmpty().FirstOrDefault();
            EmployeeRecord.NHFNumber = long.Parse(EmployeeInfo.CustomerProfile.NhfNumber);
            EmployeeRecord.EmployeeCode = EmployeeInfo.CustomerProfile.CustomerCode;
            db.SaveChanges();

            entityRecord.NHFNumber = long.Parse(EmployeeInfo.CustomerProfile.NhfNumber);
            entityRecord.EmployeeCode = EmployeeInfo.CustomerProfile.CustomerCode;

            await _iUnitOfWork.Employees.ApproveForm(entityRecord, menuRecord, user, loginProfile);
            // var createatCredit = IndividualExisting(entityRecord);
            var menus = await _iUnitOfWork.Menus.GetEmployeeMenuList();
            foreach (var menu in menus)
            {
                var menuAuth = new MenuAuthorizeEntity();
                menuAuth.AuthorizeId = entity.Id;
                menuAuth.MenuId = menu.Id;
                menuAuth.AuthorizeType = AuthorizeTypeEnum.User.ToInt();
                await _iUnitOfWork.MenuAuthorizes.SaveForm(menuAuth);

            };
            obj.Message = "Employee Approved Successfully";
            obj.Data = entity.Id;
            obj.Tag = 1;
            return obj;
        }


        public async Task<TData> RejectForm(EmployeeEntity entity, string Remark)
        {
            string message = string.Empty;
            ApplicationDbContext db = new ApplicationDbContext();
            TData<long> obj = new TData<long>();
            var user = await Operator.Instance.Current();
            var entityRecord = await _iUnitOfWork.Employees.GetEntity(entity.Id);
            await _iUnitOfWork.Employees.RejectForm(entityRecord);
            var EmployeeRecord = db.EmployeeEntity.Where(i => i.EmailAddress == entityRecord.EmailAddress).DefaultIfEmpty().FirstOrDefault();

            MailParameter mailParameter = new()
            {
                ApproverEmail = "fmbn.gov.ng",
                UserName = entityRecord.FirstName + " " + entityRecord.LastName,
                //UserEmail = user.EmployeeInfo.EmailAddress,
                UserEmail = entityRecord.EmailAddress,
                //NhfNumber = Convert.ToString(item.NHFNumber),
                RealName = entityRecord.FirstName + " " + entityRecord.LastName,
                UserCompany = "Federal Mortgage Bank of Nigeria",
                Remark = Remark
            };

            var sendemail = EmailHelper.IsRegistrationRejectionMailSent(mailParameter, out message);

            var userRecord = db.UserEntity.Where(i => i.UserName == entityRecord.EmailAddress).DefaultIfEmpty().FirstOrDefault();
            userRecord.UserStatus = 2;
            db.UserEntity.Remove(userRecord);
            db.EmployeeEntity.Remove(EmployeeRecord);
            db.SaveChanges();
            obj.Message = "Employee Disapproved Successfully";
            obj.Data = entity.Id;
            obj.Tag = 1;
            return obj;
        }




        [HttpGet]
        public async Task<bool> CustomerExist(string customerCode)
        {
            try
            {
                var _client = new HttpClient();
                var response = await _client.GetAsync(ApiResource.customerExist + customerCode);
                string response1 = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<CustomerExistResponse>(response1);

                if (result.Success == true)
                {
                    // Customer exists
                    return true;
                }

                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<TData> IndividualExisting(EmployeeEntity customercreateRequest)
        {
            TData<string> obj = new TData<string>();
            try
            {
                var NextOfKinDetails = _iUnitOfWork.NextOfKins.GetEntity(customercreateRequest.Id).Result;
                ContactAddress address = new ContactAddress()
                {
                    contactAddress = customercreateRequest.PostalAddress,
                    NearestLandmark = customercreateRequest.PostalAddress,
                    MailingAddress = customercreateRequest.EmailAddress,
                    CityId = 3017,
                    StateId = 1,
                    AddressTypeId = 3,
                    UtilityBillNo = "3456789"


                };
                NextofKin NOK = new NextofKin()
                {
                    firstName = NextOfKinDetails.FirstName,
                    lastName = NextOfKinDetails.LastName,
                    contactAddress = NextOfKinDetails.Address,
                    emailAddress = NextOfKinDetails.EmailAddress,
                    mobilePhoneNo = NextOfKinDetails.MobileNumber,
                    nearestLandmark = "",
                    cityId = 3017,
                    relationship = NextOfKinDetails.Relationship.ToString(),
                    dateOfBirth = DateTime.MinValue,
                    gender = "1",

                };
                ContactPhone phone = new ContactPhone()
                {
                    OfficeLandNo = customercreateRequest.MobileNumber,
                    MobilePhoneNo = customercreateRequest.MobileNumber
                };
                AccountDetails acctdetails = new AccountDetails()
                {
                    AccountStatusName = "Active",
                    ProductAccountName = "NHF",
                    AccountNumber = Convert.ToString(customercreateRequest.NHFNumber),
                    DateOfEmployment = customercreateRequest.DateOfEmployment,
                    MonthlyIncome = customercreateRequest.MonthlySalary.ToString(),
                    OtherBankAccountNumber = customercreateRequest.BankAccountNumber,
                    OtherBankSortCode = "214"
                };
                CreateCustomerRequestDTO customer = new CreateCustomerRequestDTO();
                customer.FirstName = customercreateRequest.FirstName;
                customer.Gender = Convert.ToString(customercreateRequest.Gender);
                customer.LastName = customercreateRequest.LastName;
                customer.MiddleName = customercreateRequest.OtherName;
                customer.Nationality = "Nigerian";
                customer.DateOfBirth = Convert.ToDateTime(customercreateRequest.DateOfBirth);
                customer.EmailAddress = customercreateRequest.EmailAddress;
                customer.CustomerBVN = customercreateRequest.BVN;
                customer.IsBvnValidated = true;
                customer.IsEmailValidated = true;
                customer.IsPhoneValidated = true;
                customer.IsPoliticallyExposed = true;
                customer.CrmsLegalStatusId = 104;
                customer.CrmsRelationshipTypeId = 152;
                customer.TaxNumber = customercreateRequest.NIN;
                customer.Title = customercreateRequest.Title.ToString();
                customer.SubSectorId = int.Parse(customercreateRequest.MobileNumber);
                customer.BranchCode = customercreateRequest.Branch.ToString();
                customer.AccountDetails = acctdetails;
                customer.contactPhone = phone;
                customer.contactAddress = address;

                var client = new HttpClient();
                StringContent content = new StringContent(JsonConvert.SerializeObject(customer), Encoding.UTF8, "application/json");
                var response = await client.PostAsync("https://testcorebanking.fmbn.gov.ng:4438/api/v1/customer/individual-existing", content);
                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<CustomerExistResponse>(apiResponse);

                    obj.Tag = 1;
                    obj.Message = "Employee Created";
                    obj.Data = null;

                }
                else
                {
                    obj.Tag = 0;
                    obj.Data = null;
                }
                return obj;

            }
            catch (Exception)
            {

                throw;
            }





        }

        #endregion
    }
}
