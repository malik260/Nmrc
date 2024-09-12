using Microsoft.AspNetCore.Mvc;
using Mortgage.Ecosystem.BusinessLogic.Layer.Cache;
using Mortgage.Ecosystem.BusinessLogic.Layer.Helpers;
using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.BusinessLogic.Layer.Resources;
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
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Services
{
    public class SecondaryLenderService : ISecondaryLenderService
    {
        private readonly IUnitOfWork _iUnitOfWork;
        private readonly HttpClient _client;

        public SecondaryLenderService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
            _client = new HttpClient
            {
                BaseAddress = new Uri(ApiResource.baseAddress)
            };
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ApiResource.ApplicationJson));

        }

        #region Retrieve data


        public async Task<TData<List<SecondaryLenderEntity>>> GetList(SecondaryLenderListParam param)
        {
            TData<List<SecondaryLenderEntity>> obj = new TData<List<SecondaryLenderEntity>>();
            var allSecondaryLenders = await _iUnitOfWork.SecondaryLenders.GetList(param);
            // Filter the list to include only approved companies
            var approvedSecondaryLenders = allSecondaryLenders.Where(secondaryLender => secondaryLender.Status == (int)ApprovalEnum.Approved).ToList();
            obj.Data = approvedSecondaryLenders;
            obj.Total = approvedSecondaryLenders.Count;
            obj.Tag = 1;
            return obj;
        }


        public async Task<TData<List<SecondaryLenderEntity>>> GetPageList(SecondaryLenderListParam param, Pagination pagination)
        {
            TData<List<SecondaryLenderEntity>> obj = new TData<List<SecondaryLenderEntity>>();
            obj.Data = await _iUnitOfWork.SecondaryLenders.GetPageList(param, pagination);
            if (obj.Data.Count > 0)
            {
                List<SectorEntity> sectorList = await _iUnitOfWork.Sectors.GetList(new SectorListParam { Ids = obj.Data.Select(p => p.Sector).ToList() });
                List<SubSectorEntity> subSectorList = await _iUnitOfWork.SubSectors.GetList(new SubSectorListParam { Ids = obj.Data.Select(p => p.Sector).ToList() });
                List<ContributionFrequencyEntity> contributionFrequencyList = await _iUnitOfWork.ContributionFrequencies.GetList(new ContributionFrequencyListParam { Ids = obj.Data.Select(p => p.Sector).ToList() });
                //List<CompanyClassEntity> companyClassList = await _iUnitOfWork.CompanyClasses.GetList(new CompanyClassListParam { Ids = obj.Data.Select(p => p.CompanyClass).ToList() });
                //List<CompanyTypeEntity> companyTypeList = await _iUnitOfWork.CompanyTypes.GetList(new CompanyTypeListParam { Ids = obj.Data.Select(p => p.CompanyType).ToList() });
                foreach (SecondaryLenderEntity company in obj.Data)
                {
                    company.SectorName = sectorList.Where(p => p.Id == company.Sector).Select(p => p.Name).FirstOrDefault();
                    company.SubSectorName = subSectorList.Where(p => p.Id == company.Subsector).Select(p => p.Name).FirstOrDefault();
                    company.ContributionFrequencyName = contributionFrequencyList.Where(p => p.Id == company.ContributionFrequency).Select(p => p.Name).FirstOrDefault();
                    //company.CompanyClassName = companyClassList.Where(p => p.Id == company.CompanyClass).Select(p => p.Name).FirstOrDefault();
                    //company.CompanyTypeName = companyTypeList.Where(p => p.Id == company.CompanyType).Select(p => p.Name).FirstOrDefault();
                }
            }
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }


        public async Task<TData<List<ZtreeInfo>>> GetZtreeSecondaryLenderList(SecondaryLenderListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<SecondaryLenderEntity> secondaryLenderList = await _iUnitOfWork.SecondaryLenders.GetList(param);
            foreach (SecondaryLenderEntity secondaryLender in secondaryLenderList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = secondaryLender.Id,
                    name = secondaryLender.Name
                });
            }
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<SecondaryLenderEntity>>> GetApprovalPageList(SecondaryLenderListParam param, Pagination pagination)
        {
            TData<List<SecondaryLenderEntity>> obj = new TData<List<SecondaryLenderEntity>>();
            obj.Data = await _iUnitOfWork.SecondaryLenders.GetApprovalPageList(param, pagination);
            if ((obj.Data.Count) > 0 && (!string.IsNullOrEmpty(param.Name)))
            {
                obj.Data = obj.Data.Where(i => i.Name.Trim().ToLower() == param.Name.Trim().ToLower()).DefaultIfEmpty().ToList();
                if (obj.Data[0] == null)
                {
                    obj.Data = new List<SecondaryLenderEntity>();
                    obj.Total = 0;
                    obj.Tag = 1;
                    return obj;
                }
            }
            if (obj.Data.Count > 0)
            {
                List<SectorEntity> sectorList = await _iUnitOfWork.Sectors.GetList(new SectorListParam { Ids = obj.Data.Select(p => p.Sector).ToList() });
                List<SubSectorEntity> subSectorList = await _iUnitOfWork.SubSectors.GetList(new SubSectorListParam { Ids = obj.Data.Select(p => p.Sector).ToList() });
                List<ContributionFrequencyEntity> contributionFrequencyList = await _iUnitOfWork.ContributionFrequencies.GetList(new ContributionFrequencyListParam { Ids = obj.Data.Select(p => p.Sector).ToList() });
                foreach (SecondaryLenderEntity company in obj.Data)
                {
                    company.SectorName = sectorList.Where(p => p.Id == company.Sector).Select(p => p.Name).FirstOrDefault();
                    company.SubSectorName = subSectorList.Where(p => p.Id == company.Subsector).Select(p => p.Name).FirstOrDefault();
                    company.ContributionFrequencyName = contributionFrequencyList.Where(p => p.Id == company.ContributionFrequency).Select(p => p.Name).FirstOrDefault();
                }
            }
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }



        public async Task<TData<List<ZtreeInfo>>> GetZtreeUserList(SecondaryLenderListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<SecondaryLenderEntity> secondaryLenderList = await _iUnitOfWork.SecondaryLenders.GetList(param);
            List<UserEntity> userList = await _iUnitOfWork.Users.GetList(null);
            foreach (SecondaryLenderEntity secondaryLender in secondaryLenderList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = secondaryLender.Id,
                    name = secondaryLender.Name
                });
                List<long> userIdList = userList.Where(t => t.Company == secondaryLender.Id).Select(t => t.Employee).ToList();
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

        public async Task<TData<SecondaryLenderEntity>> GetEntity(long id)
        {
            TData<SecondaryLenderEntity> obj = new TData<SecondaryLenderEntity>();
            obj.Data = await _iUnitOfWork.SecondaryLenders.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }

        #endregion

        #region Submit data


        public async Task<TData<string>> SaveForms(SecondaryLenderEntity entity)
        {
            TData<string> obj = new TData<string>();

            if (string.IsNullOrEmpty(entity.Name))
            {
                obj.Message = "Secondary lender name must be provided!";
                return obj;
            }

            if (_iUnitOfWork.SecondaryLenders.ExistSecondaryLender(entity))
            {
                obj.Message = "Secondary lender name already exists!";
                return obj;
            }

            if (_iUnitOfWork.Users.CheckUserName(entity.EmailAddress))
            {
                obj.Message = "Email already exists!";
                return obj;
            }
            else
            {
                entity.Salt = new UserService(_iUnitOfWork).GetPasswordSalt();
                entity.DecryptedPassword = new UserService(_iUnitOfWork).GenerateDefaultPassword();
                //entity.Password = new UserService(_iUnitOfWork).EncryptUserPassword(entity.DecryptedPassword, entity.Salt);
                entity.Password = EncryptionHelper.Encrypt(entity.DecryptedPassword, entity.Salt);
            }

            if (string.IsNullOrEmpty(entity.Address))
            {
                obj.Message = "Company address must be provided!";
                return obj;
            }



            if (string.IsNullOrEmpty(entity.RCNumber))
            {
                obj.Message = "Company RC-Number must be provided!";
                return obj;
            }
            else if (_iUnitOfWork.SecondaryLenders.ExistRCNumber(entity))
            {
                obj.Message = "Company RC-Number already exists!";
                return obj;
            }

            if (_iUnitOfWork.Users.CheckUserName(entity.EmailAddress))
            {
                obj.Message = "Email already exists!";
                return obj;
            }

            if (string.IsNullOrEmpty(entity.UserName))
            {
                obj.Message = "Login : User name must be provided!";
                return obj;
            }

            if (string.IsNullOrEmpty(entity.IndFirstName))
            {
                obj.Message = "Login : First name must be provided!";
                return obj;
            }

            var GetCompanyName1 = await _iUnitOfWork.Companies.GetByName(entity.Name);
            if (GetCompanyName1 != null)
            {
                obj.Message = "Company Name already exist!";
                return obj;
            }

            var GetCompanyName2 = await _iUnitOfWork.SecondaryLenders.GetEntitybyName(entity.Name);
            if (GetCompanyName2 != null)
            {
                obj.Message = "Company Name already exist!";
                return obj;
            }


            //if (string.IsNullOrEmpty(entity.IndLastName))
            //{
            //    obj.Message = "Login : Last name must be provided!";
            //    return obj;
            //}

            if (entity.Role.IsNullOrZero())
            {
                obj.Message = "Login : Role must be selected!";
                return obj;
            }
            var user = await Operator.Instance.Current();
            //entity.NHFNumber = _iUnitOfWork.Employees.GenerateNHFNumber();
            var menu = _iUnitOfWork.Menus.GetEntitybyUrl("SecondaryLender/SecondaryLenderIndex").Result;
            entity.BaseProcessMenu = menu.Id;
            //entity.PmbNhfNumber = _iUnitOfWork.Employees.GenerateNHFNumber().ToString();
            var message = string.Empty;
            var AuthorityInfo = new ApprovalSetupListParam();
            AuthorityInfo.MenuId = entity.BaseProcessMenu;
            var approveMenu = await _iUnitOfWork.ApprovalSetups.GetList(AuthorityInfo);
            //employeeListParam.Company = employeeListParam.Id;
            //var newEmployee = _iUnitOfWork.Employees.GetListByCompany(employeeListParam).Result.Where(i => i.EmployerType == 0).FirstOrDefault();
            foreach (var item in approveMenu)
            {
                var authorityEmail = await _iUnitOfWork.Employees.GetById(item.Authority);

                MailParameter mailParameter = new()
                {
                    RegistrationApprover = authorityEmail.FirstName + " " + authorityEmail.LastName,
                    UserEmail = authorityEmail.EmailAddress,
                    UserCompany = authorityEmail.CompanyName,
                    NewCompany = entity.Name
                };

                if (EmailHelper.IsCompanyApprovalRegistrationSent(mailParameter, out message))
                {

                }

            }
            await _iUnitOfWork.SecondaryLenders.SaveForms(entity);
            obj.Data = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData<long> obj = new TData<long>();
            await _iUnitOfWork.SecondaryLenders.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }

        [HttpPost]
        private async Task<CustomerCreationResponses> IntegrateEmployerToCore(NhfemployerVM Employer)
        {
            try
            {
                var _client = new HttpClient();
                var serializeObject = JsonConvert.SerializeObject(Employer);

                var jsonPayload = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(Employer), Encoding.UTF8, ApiResource.ApplicationJson);

                var response = await _client.PostAsync("https://testcorebanking.fmbn.gov.ng:5000/api/v2/Customer/AddEmployers", jsonPayload);


                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<CustomerCreationResponses>(responseContent);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }



        public async Task<TData> ApproveForm(SecondaryLenderEntity entity)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            TData<long> obj = new TData<long>();
            var user = await Operator.Instance.Current();
            var entityRecord = await _iUnitOfWork.SecondaryLenders.GetEntity(entity.Id);
            var companyRecord = await _iUnitOfWork.Companies.GetEntity(entity.Id);
            var menuRecord = await _iUnitOfWork.Menus.GetEntity(entityRecord.BaseProcessMenu);
            var loginProfile = await _iUnitOfWork.Users.GetEntityByCompany(entity.Id);
            loginProfile.DecryptedPassword = EncryptionHelper.Decrypt(loginProfile.Password, loginProfile.Salt);
            user.DecryptedPassword = loginProfile.DecryptedPassword;
            user.UserName = loginProfile.UserName;

            var approvalLogListParam = new ApprovalLogListParam()
            {
                Company = user.Company,
                MenuId = menuRecord.Id,
                //Authority = user.Employee,
                Record = entity.Id
            };
            var approvalLogRecords = await _iUnitOfWork.ApprovalLogs.GetList(approvalLogListParam);
            menuRecord.ApprovalLogList = approvalLogRecords;
            
            await _iUnitOfWork.SecondaryLenders.ApproveForm(entityRecord, menuRecord, user);
            var menus = await _iUnitOfWork.Menus.GetSecondaryLenderMenuList();
            foreach (var menu in menus)
            {
                var menuAuth = new MenuAuthorizeEntity();
                menuAuth.AuthorizeId = entity.Id;
                menuAuth.MenuId = menu.Id;
                menuAuth.AuthorizeType = AuthorizeTypeEnum.User.ToInt();
                await _iUnitOfWork.MenuAuthorizes.SaveForm(menuAuth);

            };
            var employeeRecord = db.EmployeeEntity.Where(i => i.EmailAddress == entityRecord.EmailAddress).DefaultIfEmpty().FirstOrDefault();
            employeeRecord.Status = 1;
            employeeRecord.NHFNumber = long.Parse(entityRecord.NHFNumber);
            employeeRecord.EmployeeCode = entityRecord.SecondaryLenderCode;

            var CompanyRecord = db.CompanyEntity.Where(i => i.Id == entityRecord.Id).DefaultIfEmpty().FirstOrDefault();
            employeeRecord.Status = 1;
            db.SaveChanges();
            obj.Message = "Lender Approved Successfully";
            obj.Data = entity.Id;
            obj.Tag = 1;
            return obj;
        }



        public async Task<TData> DisApproveForm(SecondaryLenderEntity entity, string Remark)
        {
            string message = string.Empty;
            ApplicationDbContext db = new ApplicationDbContext();
            TData<long> obj = new TData<long>();
            var user = await Operator.Instance.Current();
            var entityRecord = await _iUnitOfWork.SecondaryLenders.GetEntity(entity.Id);
            var loginProfile = await _iUnitOfWork.Users.GetEntityByCompany(entity.Id);
            await _iUnitOfWork.SecondaryLenders.DisApproveForm(entityRecord);

            var SecondaryLenderRecord = db.SecondaryLenderEntity.Where(i => i.EmailAddress == entityRecord.EmailAddress).DefaultIfEmpty().FirstOrDefault();
            MailParameter mailParameter = new()
            {
                UserName = SecondaryLenderRecord.Name,
                //UserEmail = user.EmployeeInfo.EmailAddress,
                UserEmail = SecondaryLenderRecord.EmailAddress,
                //NhfNumber = Convert.ToString(item.NHFNumber),
                RealName = SecondaryLenderRecord.Name,
                UserCompany = "Federal Mortgage Bank of Nigeria",
                Remark = Remark
            };

            var sendemail = EmailHelper.IsRegistrationRejectionMailSent(mailParameter, out message);


            var employeeRecord = db.EmployeeEntity.Where(i => i.EmailAddress == entityRecord.EmailAddress).DefaultIfEmpty().FirstOrDefault();

            var CompanyRecord = db.CompanyEntity.Where(i => i.EmailAddress == entityRecord.EmailAddress).DefaultIfEmpty().FirstOrDefault();

            var userRecord = db.UserEntity.Where(i => i.UserName == entityRecord.EmailAddress).DefaultIfEmpty().FirstOrDefault();

            db.EmployeeEntity.Remove(employeeRecord);
            db.CompanyEntity.Remove(CompanyRecord);
            db.UserEntity.Remove(userRecord);
            db.SecondaryLenderEntity.Remove(SecondaryLenderRecord);
            db.SaveChanges();
            obj.Message = "Lender DisApproved Successfully";
            ///obj.Data = entity.Id;
            obj.Tag = 1;
            return obj;
        }



        public async Task<TData<string>> SaveNewEmployee(EmployeeEntity entity)
        {
            TData<string> obj = new TData<string>();
            entity.EmploymentType = EmploymentTypeEnum.Employed.ToInt();
            entity.CompanyName = _iUnitOfWork.Companies.GetEntity(entity.Company).Result.Name;
            //entity.DateOfEmployment = DateTime.Now.ToDate();
            entity.DateOfEmployment = DateTime.Now.Date;
            var checkusername = await _iUnitOfWork.Users.GetEntity(entity.EmailAddress);

            if (checkusername != null)
            {
                obj.Message = "Employee email address already exists!";
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
            if (EmailExist != null)
            {
                obj.Message = "BVN already exist!";
                return obj;
            }
            //if (entity.BVN.IsNotNull() && !ValidationHelper.ValidateBvn(entity.BVN))
            //{
            //    obj.Message = "BVN must be digit and 11 in length!";
            //    return obj;
            //}
            //else if (_iUnitOfWork.Employees.ExistEmployeeBVN(entity))
            //{
            //    obj.Message = "Employee BVN already exists!";
            //    return obj;
            //}

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

            if (entity.DateOfEmployment == null || entity.DateOfEmployment == DateTime.MinValue)
            {
                obj.Message = "Please Provide Date Of Employment!";
                return obj;
            }
            //if (!string.IsNullOrEmpty(entity.MenuIds) || string.IsNullOrEmpty(entity.MenuIds))
            //{
            entity.Salt = new UserService(_iUnitOfWork).GetPasswordSalt();
            entity.DecryptedPassword = new UserService(_iUnitOfWork).GenerateDefaultPassword();
            //entity.Password = new UserService(_iUnitOfWork).EncryptUserPassword(entity.DecryptedPassword, entity.Salt);
            entity.Password = EncryptionHelper.Encrypt(entity.DecryptedPassword, entity.Salt);
            //}

            entity.NHFNumber = _iUnitOfWork.Employees.GenerateNHFNumber();
            entity.EmployerType = 3;
            entity.BaseProcessMenu = 563322288309538816;
            await _iUnitOfWork.SecondaryLenders.SaveNewEmployee(entity);
            // Clear the permission data in the cache
            new MenuAuthorizeCache(_iUnitOfWork).Remove();

            obj.Data = entity.Id.ParseToString();
            obj.Tag = 1;
            obj.Message = "Employee Added Successfully";
            return obj;
        }




        [HttpGet]
        public async Task<bool> CustomerExist(string customerCode)
        {
            try
            {
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


        public async Task<TData<List<EmployeeEntity>>> GetSecondaryLenderEmployee(EmployeeListParam param)
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
                    employee.CompanyName = _iUnitOfWork.SecondaryLenders.GetEntity(employee.Company).Result.Name;
                }
            }

            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }



        [HttpPost]
        public async Task<bool> UpdateCustomer(CustomerUpdateRequestDTO customerUpdateRequestDTO)
        {
            try
            {
                var jsonParameters = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(customerUpdateRequestDTO), Encoding.UTF8, ApiResource.ApplicationJson);
                var response = await _client.PostAsync(ApiResource.updateCustomer, jsonParameters);
                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<CustomerExistResponse>(responseContent);

                if (response.IsSuccessStatusCode && result.Success == true)
                {
                    // Customer update was successful
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


        [HttpPost]
        public async Task<bool> IndividualExiting(CreateCustomerRequestDTO createCustomerRequestDTO)
        {
            try
            {
                var jsonParameters = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(createCustomerRequestDTO), Encoding.UTF8, ApiResource.ApplicationJson);
                var response = await _client.PostAsync(ApiResource.individualExisting, jsonParameters);
                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<CustomerExistResponse>(responseContent);

                if (response.IsSuccessStatusCode && result.Success == true)
                {
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

        #endregion
    }
}
