using Mortgage.Ecosystem.BusinessLogic.Layer.Cache;
using Mortgage.Ecosystem.BusinessLogic.Layer.Helpers;
using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
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
using NPOI.HSSF.Record.Chart;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Services
{
    public class CustomerProfileUpdateService : ICustomerProfileUpdateService
    {
        private readonly IUnitOfWork _iUnitOfWork;
        private readonly IEmployeeService _employeeService;
        private readonly INextOfKinService _nextOfKinService;
        private readonly IUserService _userService;



        public CustomerProfileUpdateService(IUnitOfWork iUnitOfWork, IEmployeeService employeeService, INextOfKinService nextOfKinService, IUserService userService)
        {
            _iUnitOfWork = iUnitOfWork;
            _employeeService = employeeService;
            _nextOfKinService = nextOfKinService;
            _userService = userService;
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
            var DB = new ApplicationDbContext();
            var user = await Operator.Instance.Current();
            var employeeDetails = DB.EmployeeEntity.Where(i => i.Id == user.Employee).FirstOrDefault();
            TData<List<CustomerProfileUpdateEntity>> obj = new TData<List<CustomerProfileUpdateEntity>>();
            obj.Data = await _iUnitOfWork.CustomerProfileUpdates.GetPageList(param, pagination);
            obj.Data = obj.Data.Where(customerProfileUpdate => customerProfileUpdate.Employee == employeeDetails.Id).ToList();
            if (obj.Data.Count > 0)
            {
                List<CompanyEntity> companyList = await _iUnitOfWork.Companies.GetList(new CompanyListParam { Ids = obj.Data.Select(p => p.Company).ToList() });
                //List<BranchEntity> branchList = await _iUnitOfWork.Branches.GetList(new BranchListParam { Names = obj.Data.Select(p => p.Branch.ToStr()).ToList() });
                //List<DepartmentEntity> departmentList = await _iUnitOfWork.Departments.GetList(new DepartmentListParam { Ids = obj.Data.Select(p => p.Department).ToList() });
                List<TitleEntity> titleList = await _iUnitOfWork.Titles.GetList(new TitleListParam { Ids = obj.Data.Select(p => p.Title).ToList() });
                List<GenderEntity> genderList = await _iUnitOfWork.Genders.GetList(new GenderListParam { Ids = obj.Data.Select(p => p.Gender).ToList() });
                List<MaritalStatusEntity> maritalStatusList = await _iUnitOfWork.MaritalStatus.GetList(new MaritalStatusListParam { Ids = obj.Data.Select(p => p.MaritalStatus).ToList() });
                List<BankEntity> bankList = await _iUnitOfWork.Banks.GetList(new BankListParam { Codes = obj.Data.Select(p => p.CustomerBank.ToStr()).ToList() });
                List<AccountTypeEntity> accountTypeList = await _iUnitOfWork.AccountTypes.GetList(new AccountTypeListParam { Ids = obj.Data.Select(p => p.AccountType).ToList() });
                List<AlertTypeEntity> alertTypeList = await _iUnitOfWork.AlertTypes.GetList(new AlertTypeListParam { Ids = obj.Data.Select(p => p.AlertType).ToList() });
                List<RelationEntity> relationList = await _iUnitOfWork.Relations.GetList(new RelationListParam { Ids = obj.Data.Select(p => p.KinRelationship).ToList() });
                foreach (CustomerProfileUpdateEntity customerProfileUpdate in obj.Data)
                {
                    customerProfileUpdate.CompanyName = companyList.Where(p => p.Id == customerProfileUpdate.Company).Select(p => p.Name).FirstOrDefault();
                    //customerProfileUpdate.BranchName = branchList.Where(p => p.Id == customerProfileUpdate.Branch && p.Company == employee.Company).Select(p => p.Name).FirstOrDefault();
                    //customerProfileUpdate.DepartmentName = departmentList.Where(p => p.Id == customerProfileUpdate.Department && p.Company == employee.Company && p.Branch == employee.Branch).Select(p => p.Name).FirstOrDefault();
                    customerProfileUpdate.TitleName = titleList.Where(p => p.Id == customerProfileUpdate.Title).Select(p => p.Name).FirstOrDefault();
                    customerProfileUpdate.GenderName = genderList.Where(p => p.Id == customerProfileUpdate.Gender).Select(p => p.Name).FirstOrDefault();
                    customerProfileUpdate.MaritalStatusName = maritalStatusList.Where(p => p.Id == customerProfileUpdate.MaritalStatus).Select(p => p.Name).FirstOrDefault();
                    customerProfileUpdate.BankName = bankList.Where(p => p.Code == customerProfileUpdate.CustomerBank).Select(p => p.Name).FirstOrDefault();
                    //employee.ContributionBranchName = stateList.Where(p => p.Id == employee.ContributionBranch).Select(p => p.Name).FirstOrDefault();
                    customerProfileUpdate.AccountTypeName = accountTypeList.Where(p => p.Id == customerProfileUpdate.AccountType).Select(p => p.Name).FirstOrDefault();
                    customerProfileUpdate.AlertTypeName = alertTypeList.Where(p => p.Id == customerProfileUpdate.AlertType).Select(p => p.Name).FirstOrDefault();
                    customerProfileUpdate.RelationName = relationList.Where(p => p.Id == customerProfileUpdate.KinRelationship).Select(p => p.Name).FirstOrDefault();

                }
            }
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<CustomerProfileUpdateEntity>>> GetApprovalPageList(CustomerProfileUpdateListParam param, Pagination pagination)
        {
            var context = new ApplicationDbContext();
            var user = await Operator.Instance.Current();
            TData<List<CustomerProfileUpdateEntity>> obj = new TData<List<CustomerProfileUpdateEntity>>();

            //    strSql.Append(@"SELECT a.* FROM tbl_CustomerProfileUpdate a
            //                        INNER JOIN tbl_ApprovalSetup b ON b.MenuId = a.BaseProcessMenu
            //                        INNER JOIN tbl_Menu c ON a.BaseProcessMenu = c.Id AND b.MenuId = c.Id
            //                        WHERE b.Authority = " + user.Employee + " AND b.Priority = 1 AND c.ApprovalLevel > 0 AND a.Status = 0");
            //}
            var result = from T1 in context.CustomerProfileUpdateEntity
                         join T2 in context.ApprovalSetupEntity on 563329556086263808 equals T2.MenuId
                         join T3 in context.EmployeeEntity on T1.NHFNumber equals T3.NHFNumber
                         where T2.Authority == user.Employee && T1.Status == 0 && T3.Status == 1
                         select T1;
        obj.Data = await _iUnitOfWork.CustomerProfileUpdates.GetApprovalPageList(param, pagination);
            obj.Data = result.ToList().Distinct().ToList();
        //obj.Data = await _iUnitOfWork.CustomerProfileUpdates.GetApprovalPageList(param, pagination);
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
                foreach (CustomerProfileUpdateEntity employee in obj.Data)
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
        public async Task<TData<CustomerDetailsViewModel>> GetCustomerDetails()
        {
            TData<CustomerDetailsViewModel> obj = new TData<CustomerDetailsViewModel>();
            var user = await Operator.Instance.Current();

            try
            {
                var customerDetails = await _employeeService.GetEntityByNhfNo(user.EmployeeInfo.NHFNumber);
                var companyDetails = await _iUnitOfWork.Companies.GetEntity(user.EmployeeInfo.Company);
                //var gender = await _iUnitOfWork.Genders.GetEntity(user.EmployeeInfo.Gender);
                //var title = await _iUnitOfWork.Titles.GetEntity(user.EmployeeInfo.Title);

                if (customerDetails != null && customerDetails.Data != null)
                {
                    var nok = await _nextOfKinService.GetEntityByEmployee(user.Employee);

                    var custDetails = new CustomerDetailsViewModel
                    {
                        Nhfno = customerDetails.Data.NHFNumber.ToStr(),
                        EmployerNo = companyDetails.Id.ToString(),
                        EmployerName = companyDetails.Name,
                        AccountNum = customerDetails.Data.BankAccountNumber,
                        LastName = customerDetails.Data.LastName,
                        FirstName = customerDetails.Data.FirstName,
                        OtherName = customerDetails.Data.OtherName,
                        MaritalStatus = customerDetails.Data.MaritalStatus,
                        BankName = customerDetails.Data.CustomerBank,
                        EmailAddress = customerDetails.Data.EmailAddress,
                        MobileNo = customerDetails.Data.MobileNumber,
                        ContactAddress = customerDetails.Data.PostalAddress,
                        MonthlyIncome = customerDetails.Data.MonthlySalary,
                        ContributionBranch = customerDetails.Data.ContributionBranch.ToStr(),
                        EmploymentDate = customerDetails.Data.DateOfEmployment.ToDate().ToStr("0"),
                        Nin = customerDetails.Data.NIN,
                        Bvn = customerDetails.Data.BVN,
                        Title = customerDetails.Data.Title.ToStr(),
                        Gender = customerDetails.Data.Gender.ToStr(),
                        AccountType = customerDetails.Data.AccountType.ToStr(),
                        AlertType = customerDetails.Data.AlertType,
                        Dob = customerDetails.Data.DateOfBirth,
                        NOKFirstName = nok?.Data?.FirstName,
                        NOKLastName = nok?.Data?.LastName,
                        NOKPhoneNo = nok?.Data?.MobileNumber,
                        NOKAddress = nok?.Data?.Address,
                        NOKEmailAddress = nok?.Data?.EmailAddress,
                        NOKRelationship = nok?.Data?.Relationship.ToStr(),
                    };

                    // Fetch the list of CustomerProfileUpdateEntity for the current user
                    var paramss = new CustomerProfileUpdateListParam
                    {
                        Company = user.EmployeeInfo.Company,
                        Employee = user.Employee,
                        NHFNumber = user.EmployeeInfo.NHFNumber // Add NHFNumber to params
                    };

                    // Fetch the list and filter it by the current user's NHFNumber
                    var approvalStatusList = (await _iUnitOfWork.CustomerProfileUpdates.GetList(paramss))
                                              .Where(a => a.NHFNumber == user.EmployeeInfo.NHFNumber);

                    // Check if any status is zero
                    if (approvalStatusList.Any(a => a.Status == 0))
                    {
                        obj.Message = "You currently have an ongoing update process in progress.";
                        obj.Tag = 0;
                        return obj;
                    }

                    obj.Data = custDetails;
                    obj.Tag = 1;
                }
                else
                {
                    // Handle the case when customerDetails or customerDetails.Data is null
                    obj.Message = "Customer details not found.";
                    obj.Tag = -1;
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                obj.Message = $"Error: {ex.Message}";
                obj.Tag = -1;
            }

            return obj;
        }


        #endregion

        #region Submit data

        public async Task<TData<string>> SaveForm(CustomerProfileUpdateEntity entity)
        {
            TData<string> obj = new TData<string>();
            var user = await Operator.Instance.Current();
            entity.EmploymentType = EmploymentTypeEnum.Employed.ToInt();
            var customerDetails = await _employeeService.GetEntityByNhf(user.EmployeeInfo.NHFNumber);
            var nok = await _nextOfKinService.GetEntityByEmployee(user.Employee);
            var companyDetails = await _iUnitOfWork.Companies.GetEntity(user.EmployeeInfo.Company);
            try
            {
                entity.CompanyName = companyDetails.Name;
                entity.Employee = customerDetails.Id;
                entity.NHFNumber = customerDetails.NHFNumber;
                entity.Company = customerDetails.Company;
                entity.Gender = customerDetails.Gender;
                entity.BVN = customerDetails.BVN;
                entity.NIN = customerDetails.NIN;
                entity.DateOfBirth = customerDetails.DateOfBirth;
                entity.DateOfEmployment = customerDetails.DateOfEmployment;


                await _iUnitOfWork.CustomerProfileUpdates.SaveForm(entity);
                //await _iUnitOfWork.Employees.SaveForm(customerDetails);
                var message = string.Empty;
                var AuthorityInfo = new ApprovalSetupListParam();
                AuthorityInfo.MenuId = GlobalConstant.CUSTOMER_PROFILE_UPDATE_MENU_ID;
                var approveMenu = await _iUnitOfWork.ApprovalSetups.GetList(AuthorityInfo);
                approveMenu = approveMenu.DistinctBy(x => x.Authority).ToList();
                //employeeListParam.Company = employeeListParam.Id;
                //var newEmployee = _iUnitOfWork.Employees.GetListByCompany(employeeListParam).Result.Where(i => i.EmployerType == 0).FirstOrDefault();
                foreach (var item in approveMenu)
                {
                    var authorityEmail = await _iUnitOfWork.Employees.GetById(item.Authority);

                    MailParameter mailParameter = new()
                    {
                        RegistrationApprover = authorityEmail.FirstName + " " + authorityEmail.LastName,
                        UserEmail = authorityEmail.EmailAddress,
                        UserName = entity.FirstName + " " + entity.LastName,
                        UserCompany = entity.CompanyName,
                    };

                    if (EmailHelper.IsEmployeeUpdateApprovalSent(mailParameter, out message))
                    {

                    }

                }

                obj.Data = entity.Id.ParseToString();
                obj.Tag = 1;
                obj.Message = "Customer Profile Updated Successfully";
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine($"Exception: {ex}");
                obj.Tag = -1;
                obj.Message = "An error occurred while updating the customer profile.";
            }
            return obj;
        }

        public async Task<TData> ApproveForm(CustomerProfileUpdateEntity entity)
        {
            TData<long> obj = new TData<long>();
            var user = await Operator.Instance.Current();
            var entityRecord = await _iUnitOfWork.CustomerProfileUpdates.GetEntity(entity.Id);

            var DB = new ApplicationDbContext();
            var customerDetails = DB.EmployeeEntity.Where(i => i.NHFNumber == entityRecord.NHFNumber).DefaultIfEmpty().FirstOrDefault();
            var nok = DB.NextOfKinEntity.Where(i => i.Employee == customerDetails.Id).DefaultIfEmpty().FirstOrDefault();
            var companyDetails = DB.CompanyEntity.Where(i => i.Id == customerDetails.Company).FirstOrDefault();
            var UserDetails = DB.UserEntity.Where(i => i.Employee == customerDetails.Id).FirstOrDefault();

            customerDetails.FirstName = entityRecord.FirstName;
            customerDetails.ContributionBranch = entityRecord.ContributionBranch;
            customerDetails.LastName = entityRecord.LastName;
            customerDetails.OtherName = entityRecord.OtherName;
            customerDetails.BankAccountNumber = entityRecord.BankAccountNumber;
            customerDetails.EmailAddress = entityRecord.EmailAddress;
            customerDetails.MaritalStatus = entityRecord.MaritalStatus;
            customerDetails.MobileNumber = entityRecord.MobileNumber;
            customerDetails.CustomerBank = entityRecord.CustomerBank;
            customerDetails.PostalAddress = entityRecord.PostalAddress;
            customerDetails.MonthlySalary = entityRecord.MonthlyIncome;
            customerDetails.AccountType = entityRecord.AccountType;
            customerDetails.Title = entityRecord.Title;
            customerDetails.AlertType = entityRecord.AlertType;

            DB.EmployeeEntity.Update(customerDetails);
            DB.SaveChanges();

            nok.FirstName = entityRecord.KinFirstName;
            nok.LastName = entityRecord.KinLastName;
            nok.EmailAddress = entityRecord.KinEmailAddress;
            nok.MobileNumber = entityRecord.KinMobileNumber;
            nok.Address = entityRecord.KinAddress;
            nok.Relationship = entityRecord.KinRelationship;
            DB.NextOfKinEntity.Update(nok);
            DB.SaveChanges();


            entityRecord.Status = 1;
            DB.CustomerProfileUpdateEntity.Update(entityRecord);
            DB.SaveChanges();

            // Update the user record
            var User = DB.UserEntity.Where(i => i.Employee == customerDetails.Id).FirstOrDefault();
            User.UserName = entityRecord.EmailAddress;
            User.RealName = entityRecord.FirstName;

            DB.UserEntity.Update(User);
            DB.SaveChanges();

            var message = string.Empty;
            MailParameter mailParameter = new()
            {
                RealName = customerDetails.FirstName + " " + customerDetails.LastName,
                UserEmail = customerDetails.EmailAddress,
                UserCompany = entity.CompanyName,
            };

            if (EmailHelper.IsApprovedCustomerUpdateEmailSent(mailParameter, out message))
            {

            }

            obj.Message = "Record Approved Successfully";
            obj.Data = entity.Id;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> RejectForm(CustomerProfileUpdateEntity entity, string Remark)
        {
            string message = string.Empty;
            var DB = new ApplicationDbContext();
            ApplicationDbContext db = new ApplicationDbContext();
            TData<long> obj = new TData<long>();
            var entityRecord = await _iUnitOfWork.CustomerProfileUpdates.GetEntity(entity.Id);
            var customerDetails = DB.EmployeeEntity.Where(i => i.NHFNumber == entityRecord.NHFNumber).DefaultIfEmpty().FirstOrDefault();
            await _iUnitOfWork.CustomerProfileUpdates.RejectForm(entityRecord);
            var EmployeeRecord = db.CustomerProfileUpdateEntity.Where(i => i.EmailAddress == entityRecord.EmailAddress).DefaultIfEmpty().FirstOrDefault();

                MailParameter mailParameter = new()
                {
                    RegistrationApprover = "fmbn.gov.ng",
                    //UserName = entityRecord.FirstName + " " + entityRecord.LastName,
                    //UserEmail = user.EmployeeInfo.EmailAddress,
                    UserEmail = entityRecord.EmailAddress,
                    //NhfNumber = Convert.ToString(item.NHFNumber),
                    RealName = entityRecord.FirstName + " " + entityRecord.LastName,
                    UserCompany = "Federal Mortgage Bank of Nigeria",
                    Remark = Remark
                };

                var sendemail = EmailHelper.IsCustomerUpdateRejectionMailSent(mailParameter, out message);

            var userRecord = db.CustomerProfileUpdateEntity.Where(i => i.Id == entityRecord.Id).DefaultIfEmpty().FirstOrDefault();
            userRecord.Status = 2;
            //db.CustomerProfileUpdateEntity.Remove(userRecord);
            //db.CustomerProfileUpdateEntity.Remove(EmployeeRecord);
            db.SaveChanges();
            obj.Message = "Employee Update Disapproved Successfully";
            obj.Data = entity.Id;
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
