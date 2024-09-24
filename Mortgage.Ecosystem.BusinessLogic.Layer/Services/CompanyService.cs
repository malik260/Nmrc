using Microsoft.AspNetCore.Mvc;
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
using System.Text.RegularExpressions;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly IUnitOfWork _iUnitOfWork;
        private readonly HttpClient _client;

        public CompanyService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
            _client = new HttpClient
            {
                BaseAddress = new Uri(ApiResource.baseAddress)
            };
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ApiResource.ApplicationJson));

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


        #region Retrieve data
        //public async Task<TData<List<CompanyEntity>>> GetList(CompanyListParam param)
        //{
        //    TData<List<CompanyEntity>> obj = new TData<List<CompanyEntity>>();
        //    obj.Data = await _iUnitOfWork.Companies.GetList(param);
        //    obj.Total = obj.Data.Count;
        //    obj.Tag = 1;
        //    return obj;
        //}

        public async Task<TData<CustomerDetailsViewModel>> GetCompanyInfo()
        {
            TData<CustomerDetailsViewModel> obj = new TData<CustomerDetailsViewModel>();
            var user = await Operator.Instance.Current();
            var customerDetails = await _iUnitOfWork.Companies.GetEntity(user.Company);

            var custDetails = new CustomerDetailsViewModel
            {

                companyName = customerDetails.Name,
                companyNumber = customerDetails.Id.ToString(),

            };

            obj.Data = custDetails;
            obj.Tag = 1;
            return obj;
        }


        public async Task<TData<List<CompanyEntity>>> GetList(CompanyListParam param)
        {
            TData<List<CompanyEntity>> obj = new TData<List<CompanyEntity>>();
            var allCompanies = await _iUnitOfWork.Companies.GetList(param);
            // Filter the list to include only approved companies
            var approvedCompanies = allCompanies.Where(company => company.Status == (int)ApprovalEnum.Approved).ToList();
            obj.Data = approvedCompanies;
            obj.Total = approvedCompanies.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<CompanyEntity>>> GetCurrentCompany(CompanyListParam param)
        {
            var user = await Operator.Instance.Current();
            TData<List<CompanyEntity>> obj = new TData<List<CompanyEntity>>();
            var allCompanies = await _iUnitOfWork.Companies.GetList(param);
            // Filter the list to include only approved companies
            //var approvedCompanies = allCompanies.Where(company => company.Status == (int)ApprovalEnum.Approved && company.Id == user.Company).ToList();
            var approvedCompanies = allCompanies.Where(company => company.Id == user.Company).ToList();
            obj.Data = approvedCompanies;
            obj.Total = approvedCompanies.Count;
            obj.Tag = 1;
            return obj;
        }


        public async Task<TData<List<CompanyEntity>>> GetPageList(CompanyListParam param, Pagination pagination)
        {
            TData<List<CompanyEntity>> obj = new TData<List<CompanyEntity>>();
            obj.Data = await _iUnitOfWork.Companies.GetPageList2(param, pagination);
            if ((obj.Data.Count) > 0 && (!string.IsNullOrEmpty(param.Name)))
            {
                obj.Data = obj.Data.Where(i => i.Name.Trim().ToLower() == param.Name.Trim().ToLower()).DefaultIfEmpty().ToList();
                if (obj.Data[0] == null)
                {
                    obj.Data = new List<CompanyEntity>();
                    obj.Total = 0;
                    obj.Tag = 1;
                    return obj;
                }
            }
            if (obj.Data.Count > 0)
            {
                List<SectorEntity> sectorList = await _iUnitOfWork.Sectors.GetList(new SectorListParam { Ids = obj.Data.Select(p => p.Sector).ToList() });
                List<CompanyClassEntity> companyClassList = await _iUnitOfWork.CompanyClasses.GetList(new CompanyClassListParam { Ids = obj.Data.Select(p => p.CompanyClass).ToList() });
                List<AgentTypeEntity> companyTypeList = await _iUnitOfWork.AgentTypes.GetList(new AgentTypeListParam { Ids = obj.Data.Select(p => p.CompanyType).ToList() });
                foreach (CompanyEntity company in obj.Data)
                {
                    company.SectorName = sectorList.Where(p => p.Id == company.Sector).Select(p => p.Name).FirstOrDefault();
                    company.CompanyClassName = companyClassList.Where(p => p.Id == company.CompanyClass).Select(p => p.Name).FirstOrDefault();
                    company.CompanyTypeName = companyTypeList.Where(p => p.Id == company.CompanyType).Select(p => p.Name).FirstOrDefault();
                }
            }
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<CompanyEntity>>> GetApprovalPageList(CompanyListParam param, Pagination pagination)
        {
            TData<List<CompanyEntity>> obj = new TData<List<CompanyEntity>>();
            obj.Data = await _iUnitOfWork.Companies.GetApprovalPageList(param, pagination);
            if ((obj.Data.Count) > 0 && (!string.IsNullOrEmpty(param.Name)))
            {
                obj.Data = obj.Data.Where(i => i.Name.Trim().ToLower() == param.Name.Trim().ToLower()).DefaultIfEmpty().ToList();
                if (obj.Data[0] == null)
                {
                    obj.Data = new List<CompanyEntity>();
                    obj.Total = 0;
                    obj.Tag = 1;
                    return obj;
                }
            }
            if (obj.Data.Count > 0)
            {
                List<SectorEntity> sectorList = await _iUnitOfWork.Sectors.GetList(new SectorListParam { Ids = obj.Data.Select(p => p.Sector).ToList() });
                List<SubSectorEntity> SubSectorList = await _iUnitOfWork.SubSectors.GetList(new SubSectorListParam { Ids = obj.Data.Select(p => p.Subsector).ToList() });
                //List<CompanyEntity> companyList = await _iUnitOfWork.Companies.GetList(new CompanyListParam { Ids = obj.Data.Select(p => p.Name.ToString()).ToList() });
                List<CompanyClassEntity> companyClassList = await _iUnitOfWork.CompanyClasses.GetList(new CompanyClassListParam { Ids = obj.Data.Select(p => p.CompanyClass).ToList() });
                List<CompanyTypeEntity> companyTypeList = await _iUnitOfWork.CompanyTypes.GetList(new CompanyTypeListParam { Ids = obj.Data.Select(p => p.CompanyType).ToList() });
                List<ContributionFrequencyEntity> contributionFrequencyList = await _iUnitOfWork.ContributionFrequencies.GetList(new ContributionFrequencyListParam { Ids = obj.Data.Select(p => p.ContributionFrequency).ToList() });

                foreach (CompanyEntity company in obj.Data)
                {
                    company.SectorName = sectorList.Where(p => p.Id == company.Sector).Select(p => p.Name).FirstOrDefault();
                    company.SubSectorName = SubSectorList.Where(p => p.Id == company.Subsector).Select(p => p.Name).FirstOrDefault();
                    company.CompanyClassName = companyClassList.Where(p => p.Id == company.CompanyClass).Select(p => p.Name).FirstOrDefault();
                    //company.CompanyName = companyList.Where(p => p.Id == company.Name.ToString()).Select(p => p.Name).FirstOrDefault();
                    company.CompanyTypeName = companyTypeList.Where(p => p.Id == company.CompanyType).Select(p => p.Name).FirstOrDefault();
                    company.ContributionFrequencyName = contributionFrequencyList.Where(p => p.Id == company.ContributionFrequency).Select(p => p.Name).FirstOrDefault();
                }
            }
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ZtreeInfo>>> GetZtreeCompanyList(CompanyListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<CompanyEntity> companyList = _iUnitOfWork.Companies.GetList(param).Result.Where(i => i.Status == 1).ToList();
            foreach (CompanyEntity company in companyList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = company.Id,
                    name = company.Name
                });
            }
            obj.Tag = 1;
            return obj;
        }
        public async Task<TData<List<ZtreeInfo>>> GetZtreeCompanyList2(CompanyListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<CompanyEntity> companyList = _iUnitOfWork.Companies.GetList(param).Result.Where(i => i.Status == 1 && i.CompanyType == 1).ToList();

            foreach (CompanyEntity company in companyList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = company.Id,
                    name = company.Name
                });
            }
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ZtreeInfo>>> GetZtreeUserList(CompanyListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<CompanyEntity> companyList = await _iUnitOfWork.Companies.GetList(param);
            List<UserEntity> userList = await _iUnitOfWork.Users.GetList(null);
            foreach (CompanyEntity company in companyList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = company.Id,
                    name = company.Name
                });
                List<long> userIdList = userList.Where(t => t.Company == company.Id).Select(t => t.Employee).ToList();
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

        public async Task<TData<CompanyEntity>> GetEntity(long id)
        {
            TData<CompanyEntity> obj = new TData<CompanyEntity>();
            obj.Data = await _iUnitOfWork.Companies.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }

        #endregion

        #region Submit data
        public async Task<TData<string>> SaveForm(CompanyEntity entity)
        {
            TData<string> obj = new TData<string>();
            if (_iUnitOfWork.Companies.ExistCompany(entity))
            {
                obj.Message = "Company name already exists!";
                return obj;
            }

            await _iUnitOfWork.Companies.SaveForm(entity);
            obj.Data = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<string>> SaveForms(CompanyEntity entity)
        {
            TData<string> obj = new TData<string>();

            if (string.IsNullOrEmpty(entity.Name))
            {
                obj.Message = "Company name must be provided!";
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
            var GetCompanyName1 = await _iUnitOfWork.Companies.GetByName(entity.Name);
            if (GetCompanyName1 != null)
            {
                obj.Message = "Company Name already exist!";
                return obj;
            }

            var GetCompanyName2 = await _iUnitOfWork.Pmbs.GetEntitybyName(entity.Name);
            if (GetCompanyName2 != null)
            {
                obj.Message = "Company Name already exist!";
                return obj;
            }

            var GetCompanyName3 = await _iUnitOfWork.SecondaryLenders.GetEntitybyName(entity.Name);
            if (GetCompanyName3 != null)
            {
                obj.Message = "Company Name already exist!";
                return obj;
            }



            var mobileExist = await _iUnitOfWork.Employees.GetEmployeeByMobile(entity.MobileNumber);
            if (mobileExist != null)
            {
                obj.Message = "Mobile Number already exist!";
                return obj;
            }

            var EmailExist = await _iUnitOfWork.Employees.GetEmployeeByEmail(entity.EmailAddress);
            if (EmailExist != null)
            {
                obj.Message = "Email Address already exist!";
                return obj;
            }


            if (string.IsNullOrEmpty(entity.EmailAddress))
            {
                obj.Message = "Email Address must be provided!";
                return obj;
            }

            var emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            if (!Regex.IsMatch(entity.EmailAddress, emailPattern))
            {
                obj.Message = "Please provide a valid Email Address!";
                return obj;
            }

            if (entity.DateOfIncorporation == null || entity.DateOfIncorporation == DateTime.MinValue)
            {
                obj.Message = "Please Provide Date Of Incorporation!";
                return obj;
            }

            if (_iUnitOfWork.Companies.ExistCompany(entity))
            {
                obj.Message = "Company name already exists!";
                return obj;
            }

            CusDetailsToCheck cusdet = new CusDetailsToCheck();
            cusdet.EmailAddress = entity.EmailAddress;
            cusdet.MobileNo = entity.MobileNumber;
            var EmployeeInfo = await IntegrateCustomerExis(cusdet);
            if (EmployeeInfo.responseCode != "200")
            {
                obj.Message = EmployeeInfo.message;
                obj.Tag = 0;
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
                entity.Password = EncryptionHelper.Encrypt(entity.DecryptedPassword, entity.Salt);
            }

            if (string.IsNullOrEmpty(entity.Address))
            {
                obj.Message = "Company address must be provided!";
                return obj;
            }

            if (string.IsNullOrEmpty(entity.AgentType))
            {
                entity.AgentType = GlobalConstant.SIX.ToString();
            }

            if (int.Parse(entity.AgentType.ToStr()) > GlobalConstant.ZERO && int.Parse(entity.AgentType.ToStr()) == GlobalConstant.ONE)
            {
                if (entity.Sector < GlobalConstant.ONE)
                {
                    obj.Message = "Company sector must be selected!";
                    return obj;
                }
            }

            if (string.IsNullOrEmpty(entity.RCNumber))
            {
                obj.Message = "Company RC-Number must be provided!";
                return obj;
            }
            else if (_iUnitOfWork.Companies.ExistRCNumber(entity))
            {
                obj.Message = "Company RC-Number already exists!";
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

            if (string.IsNullOrEmpty(entity.IndLastName))
            {
                obj.Message = "Login : Last name must be provided!";
                return obj;
            }
            

            
            var currentMenu = await new DataRepository().GetMenuId(GlobalConstant.COMPANY_MENU_URL);
            entity.BaseProcessMenu = currentMenu;
            entity.EmployerNhfNumber = _iUnitOfWork.Employees.GenerateNHFNumber().ToString();
            entity.CompanyType = entity.AgentType.ToInt();
            await _iUnitOfWork.Companies.SaveForms(entity);
            var message = string.Empty;
            var AuthorityInfo = new ApprovalSetupListParam();
            AuthorityInfo.MenuId = entity.BaseProcessMenu;
            var approveMenu = await _iUnitOfWork.ApprovalSetups.GetList(AuthorityInfo);
            //employeeListParam.Company = employeeListParam.Id;
            //var newEmployee = _iUnitOfWork.Employees.GetListByCompany(employeeListParam).Result.Where(i => i.EmployerType == 0).FirstOrDefault();
            foreach (var item in approveMenu)
            {
                var authorityEmail = await _iUnitOfWork.Employees.GetById(item.Authority);
                if (authorityEmail != null)
                {
                    var authorityCompany = await _iUnitOfWork.Companies.GetById(authorityEmail.Company);

                    MailParameter mailParameter = new()
                    {
                        RegistrationApprover = authorityEmail.FirstName + " " + authorityEmail.LastName,
                        UserEmail = authorityEmail.EmailAddress,
                        UserCompany = authorityCompany.Name,
                        NewCompany = entity.Name
                    };

                    var sendEmail = EmailHelper.IsCompanyApprovalRegistrationSent(mailParameter, out message);


                }



            }
            obj.Message = "Registration Successful";
            obj.Data = entity.Employee.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData<long> obj = new TData<long>();
            await _iUnitOfWork.Companies.DeleteForm(ids);
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




        public async Task<TData> ApproveForm(CompanyEntity entity)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            TData<long> obj = new TData<long>();
            var user = await Operator.Instance.Current();
            //var entityRecord = db.CompanyEntity.Where(i => i.Id == entity.Id).DefaultIfEmpty().FirstOrDefault();
            var entityRecord = await _iUnitOfWork.Companies.GetEntity(entity.Id);
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
            NhfemployerVM employer = new NhfemployerVM();
            employer.Employername = entityRecord.Name;
            employer.Emailaddress = entityRecord.EmailAddress;
            employer.Mobilenumber = entityRecord.MobileNumber;
            employer.Rcnumber = entityRecord.RCNumber;
            employer.Addressline1 = entityRecord.Address;
            employer.Addressline2 = entityRecord.Address;
            employer.Contactperson = entityRecord.ContactPerson;
            employer.Contactpersondesignation = entityRecord.ContactPersonDesignation;
            employer.Contributionlocation = "101";
            employer.Datecreated = DateTime.Now;
            employer.Economicsector = entityRecord.Sector.ToString();
            employer.Postaladdress = entityRecord.Address;
            employer.Telephonenumber = entityRecord.MobileNumber;
            employer.Batchrefr = "2";
            var EmployerInfo = await IntegrateEmployerToCore(employer);
            if (EmployerInfo.responseCode != "200")
            {
                obj.Message = "Employer Approval Failed: " + EmployerInfo.message + "on Core Banking";
                obj.Data = entity.Id;
                obj.Tag = 0;
                return obj;

            }

            var EmployerRecord = db.CompanyEntity.Where(i => i.EmailAddress == employer.Emailaddress).DefaultIfEmpty().FirstOrDefault();
            EmployerRecord.EmployerNhfNumber = EmployerInfo.CustomerProfile.NhfNumber;
            EmployerRecord.EmployerCode = EmployerInfo.CustomerProfile.CustomerCode;
            db.SaveChanges();

            entityRecord.EmployerNhfNumber = EmployerInfo.CustomerProfile.NhfNumber;
            entityRecord.EmployerCode = EmployerInfo.CustomerProfile.CustomerCode;
            await _iUnitOfWork.Companies.ApproveForm(entityRecord, menuRecord, user);


            var menus = await _iUnitOfWork.Menus.GetEmployerMenuList();
            var menutoremove = db.MenuEntity.Where(x => x.Id == 563493675972169728).DefaultIfEmpty().FirstOrDefault();
            menus.Remove(menutoremove);
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
            employeeRecord.NHFNumber = long.Parse(EmployerInfo.CustomerProfile.NhfNumber);
            employeeRecord.EmployeeCode = EmployerInfo.CustomerProfile.CustomerCode;
            employeeRecord.UserType = 1;
            db.SaveChanges();
            obj.Message = "Employer Approved successfully";
            obj.Data = entity.Id;
            obj.Tag = 1;
            return obj;
        }



        public async Task<TData> RejectForm(CompanyEntity entity, string Remark)
        {
            string message = string.Empty;
            ApplicationDbContext db = new ApplicationDbContext();
            TData<long> obj = new TData<long>();
            var user = await Operator.Instance.Current();
            //var entityRecord = db.CompanyEntity.Where(i => i.Id == entity.Id).DefaultIfEmpty().FirstOrDefault();
            var entityRecord = await _iUnitOfWork.Companies.GetEntity(entity.Id);
            var loginProfile = await _iUnitOfWork.Users.GetEntityByCompany(entity.Id);
            await _iUnitOfWork.Companies.DisApproveForm(entityRecord, user);

            var CompanyRecord = db.CompanyEntity.Where(i => i.EmailAddress == entityRecord.EmailAddress).DefaultIfEmpty().FirstOrDefault();

            MailParameter mailParameter = new()
            {
                UserName = CompanyRecord.CompanyName,
                //UserEmail = CompanyRecord.EmailAddress,
                UserEmail = CompanyRecord.EmailAddress,
                //NhfNumber = Convert.ToString(item.NHFNumber),
                RealName = CompanyRecord.CompanyName,
                UserCompany = "Federal Mortgage Bank of Nigeria",
                Remark = Remark
            };

            var sendemail = EmailHelper.IsRegistrationRejectionMailSent(mailParameter, out message);


            //CompanyRecord.Status = 2;

            var employeeRecord = db.EmployeeEntity.Where(i => i.EmailAddress == entityRecord.EmailAddress).DefaultIfEmpty().FirstOrDefault();
            //employeeRecord.Status = 2;

            var userRecord = db.UserEntity.Where(i => i.UserName == entityRecord.EmailAddress).DefaultIfEmpty().FirstOrDefault();
            //userRecord.UserStatus = 2;
            db.EmployeeEntity.Remove(employeeRecord);
            db.UserEntity.Remove(userRecord);
            db.CompanyEntity.Remove(CompanyRecord);
            db.SaveChanges();
            obj.Message = "Employer Disapproved successfully";
            obj.Data = entity.Id;
            obj.Tag = 1;
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

                if (result.Status == true)
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
