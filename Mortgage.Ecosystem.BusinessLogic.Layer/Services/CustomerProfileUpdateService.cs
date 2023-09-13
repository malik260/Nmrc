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
    public class CustomerProfileUpdateService : ICustomerProfileUpdateService
    {
        private readonly IUnitOfWork _iUnitOfWork;
        private readonly IEmployeeService _employeeService;


        public CustomerProfileUpdateService(IUnitOfWork iUnitOfWork, IEmployeeService employeeService)
        {
            _iUnitOfWork = iUnitOfWork;
            _employeeService = employeeService;
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

        public async Task<TData<CustomerDetailsViewModel>> GetCustomerDetails()
        {
            TData<CustomerDetailsViewModel> obj = new TData<CustomerDetailsViewModel>();
            var user = await Operator.Instance.Current();
            var customerDetails = await _employeeService.GetEntityByNhfNo(user.EmployeeInfo.NHFNumber);
            var refundParam = new RefundParam
            {
                Name = customerDetails.Data.FirstName,
                NhfNumber = customerDetails.Data.NHFNumber.ToStr()

            };
            //var noOfRefund = await _iUnitOfWork.Refunds.GetRefundList(refundParam);
            //if (noOfRefund.Count() > 0)
            //{
            //    obj.Message = "You currently have an ongoing refund process in progress.";
            //    obj.Tag = 0;
            //    return obj;
            //}
            ////else
            ////{
            ////    obj.Message = "Proceed to application";
            ////    obj.Tag = 1;
            ////    return obj;
            ////}
            //int age = 0;
            //int yearsofservice = 0;
            //DateTime? EmployementDate = DateTime.Now;
            //var amount = 0.00m;
            ////var con = _context.FinanceCounterpartyTransactionEntity.Where(i => i.Ref == employeeNumber && i.Approved == 1 && i.TransactionType == "70").Select(x => x.CreditAmount).ToList().Sum();
            //var x1 = new FinanceCounterpartyTransactionListParam()
            //{
            //    Ref = user.EmployeeInfo.NHFNumber.ToStr(),
            //    Approved = 1
            //};
            //var xx = _financeCounterpartyTransactionService.GetList(x1);
            //var totalContribution = xx.Result.Data.Where(i => i.TransactionType == "70").Select(x => x.CreditAmount).ToList().Sum();
            //var loanDisbursed = xx.Result.Data.Where(i => i.TransactionType == "70").Select(x => x.DebitAmount).ToList().Sum();
            //var totalLoanRepaid = xx.Result.Data.Where(i => i.TransactionType == "71").Select(x => x.CreditAmount).ToList().Sum();
            //var loanBalance = loanDisbursed - totalLoanRepaid;
            //amount = Convert.ToDecimal(totalContribution - loanBalance);
            //var dateOfBirth = customerDetails.Data.DateOfBirth.ToDate();
            //DateTime dateOfService = Convert.ToDateTime(customerDetails.Data.DateOfEmployment);
            //age = DateTime.Now.Year - dateOfBirth.Year;
            //if (DateTime.Now.DayOfYear < dateOfBirth.DayOfYear)
            //    age = age - 1;
            //if (customerDetails.Data.DateOfEmployment != null)
            //{
            //    yearsofservice = DateTime.Now.Year - dateOfService.Year;
            //    if (DateTime.Now.DayOfYear < dateOfService.DayOfYear)
            //        yearsofservice = yearsofservice - 1;
            //}

            //var BankName = _context.BankEntity.Where(i => i.Code == customerDetails.Data.CustomerBank).DefaultIfEmpty().FirstOrDefault();
            var custDetails = new CustomerDetailsViewModel
            {
                AccountNum = customerDetails.Data.BankAccountNumber,
                Name = customerDetails.Data.LastName + " " + customerDetails.Data.FirstName,
                MaritalStatus = customerDetails.Data.MaritalStatus,
                BankName = customerDetails.Data.CustomerBank,
                MobileNo = customerDetails.Data.MobileNumber,
                ContactAddress = customerDetails.Data.PostalAddress,
                MonthlyIncome = customerDetails.Data.MonthlySalary,
                NOKName = customerDetails.Data.NOKName,
                NOKPhoneNo = customerDetails.Data.NOKNumber,
                NOKAddress = customerDetails.Data.NOKAddress,
                NOKRelationship = customerDetails.Data.Relationship,
                //Branchcode = customerDetails.Data.Branch.ToString(),
                //CustNo = customerDetails.Data.StaffNumber,
                //Dob = Convert.ToDateTime(customerDetails.Data.DateOfBirth),
                //Dob = customerDetails.Data.DateOfBirth.ToDate(),
                //Dob = customerDetails.Data.DateOfBirth.ToDate().ToStr("0"),
                //EmployerNo = user.CompanyInfo.RCNumber,
                //EmploymentDate = customerDetails.Data.DateOfEmployment.ToDate().ToStr("0"),
                //Bvn = customerDetails.Data.BVN,
                //Nin = customerDetails.Data.NIN,


                //EmployerName = user.CompanyInfo.Name,
                //Nhfno = customerDetails.Data.NHFNumber.ToStr(),
                //BankAccountNumber = customerDetails.Data.BankAccountNumber,
                //Age = age,
                //YearsOfService = yearsofservice,
                //Amount = Convert.ToString(amount),


                //BankCode = customerDetails.Data.
            };

            obj.Data = custDetails;
            obj.Tag = 1;
            return obj;
        }

        #endregion

        #region Submit data

        public async Task<TData<string>> SaveForm(CustomerProfileUpdateEntity entity)
        {
            TData<string> obj = new TData<string>();
            try
            {
                var user = await Operator.Instance.Current();
                var customerDetails = await _employeeService.GetEntityByNhf(user.EmployeeInfo.NHFNumber);

                if (customerDetails == null)
                {
                    obj.Tag = 0;
                    obj.Message = "Customer details not found.";
                    return obj;
                }

                entity.NHFNumber = customerDetails.NHFNumber.ToStr();
                customerDetails.BankAccountNumber = entity.NewBankAccountNumber;

                if (!string.IsNullOrEmpty(entity.FullName))
                {
                    var fullNameParts = entity.FullName.Split(" ");
                    if (fullNameParts.Length > 0)
                    {
                        customerDetails.FirstName = fullNameParts[0];
                        if (fullNameParts.Length > 1)
                        {
                            customerDetails.LastName = string.Join(" ", fullNameParts.Skip(1));
                        }
                        else
                        {
                            customerDetails.LastName = string.Empty;
                        }
                    }
                }

                customerDetails.MaritalStatus = entity.NewMaritalStatus;
                customerDetails.MobileNumber = entity.NewMobileNumber;
                customerDetails.BankName = entity.NewCustomerBank;
                customerDetails.PostalAddress = entity.NewAddress;
                customerDetails.MonthlySalary = entity.NewMonthlyIncome;
                customerDetails.NOKName = entity.NewNOKName;
                customerDetails.NOKNumber = entity.NewNOKNumber;
                customerDetails.NOKAddress = entity.NewNOKAddress;
                customerDetails.Relationship = entity.NewRelationship;

                await _iUnitOfWork.Employees.SaveForm(customerDetails);
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

        //public async Task<TData<string>> SaveForm(CustomerProfileUpdateEntity entity)
        //{
        //    TData<string> obj = new TData<string>();
        //    var user = await Operator.Instance.Current();
        //    var customerDetails = await _employeeService.GetEntityByNhf(user.EmployeeInfo.NHFNumber);
        //    entity.NHFNumber = customerDetails.NHFNumber.ToStr();
        //    customerDetails.BankAccountNumber = entity.NewBankAccountNumber;
        //    // customerDetails.Data.LastName + " " + customerDetails.Data.FirstName = entity.FullName
        //    entity.FullName = customerDetails.LastName + " " + customerDetails.FirstName;
        //    customerDetails.MaritalStatus = entity.NewMaritalStatus;
        //    customerDetails.MobileNumber = entity.NewMobileNumber;
        //    customerDetails.BankName = entity.NewCustomerBank;
        //    customerDetails.PostalAddress = entity.NewAddress;
        //    customerDetails.MonthlySalary = entity.NewMonthlyIncome;
        //    customerDetails.NOKName = entity.NewNOKName;
        //    customerDetails.NOKNumber = entity.NewNOKNumber;
        //    customerDetails.NOKAddress = entity.NewNOKAddress;
        //    customerDetails.Relationship = entity.NewRelationship;
        //    await _iUnitOfWork.Employees.SaveForm(customerDetails);
        //    obj.Data = entity.Id.ParseToString();
        //    obj.Tag = 1;
        //    obj.Message = "Customer Profile Updated Successfully";
        //    return obj;
        //}

        //public async Task<TData<string>> UpdateCustomerProfile(CustomerProfileUpdateEntity entity)
        //{
        //    TData<string> obj = new TData<string>();

        //    // Check if the entity exists in the database
        //    EmployeeEntity existingEntity = await _iUnitOfWork.Employees.GetById(entity.Id);
        //    if (existingEntity == null)
        //    {
        //        obj.Message = "Entity not found";
        //        obj.Tag = -1;
        //        return obj;
        //    }

        //    // Update the existing entity with the new data
        //    string[] fullName = entity.FullName.Split(' ');

        //    existingEntity.MobileNumber = entity.MobileNumber;
        //    existingEntity.PostalAddress = entity.Address;
        //    existingEntity.MaritalStatus = entity.MaritalStatus;
        //    existingEntity.BankAccountNumber = entity.BankAccountNumber;
        //    existingEntity.CustomerBank = entity.CustomerBank;
        //    existingEntity.MonthlySalary = entity.MonthlyIncome;
        //    existingEntity.NOKName = entity.NOKName;
        //    existingEntity.NOKNumber = entity.NOKNumber;
        //    existingEntity.LastName = fullName[0];
        //    existingEntity.FirstName = fullName[1];
        //    existingEntity.OtherName = fullName[2];
        //    existingEntity.NOKAddress = entity.NOKAddress;
        //    existingEntity.Relationship = entity.Relationship;
        //    // Save the changes to the database
        //    await _iUnitOfWork.Employees.SaveForm(existingEntity);
        //    await _iUnitOfWork.CustomerProfileUpdates.SaveForm(entity);

        //    obj.Data = existingEntity.Id.ParseToString();
        //    obj.Tag = 1;
        //    return obj;
        //}
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
