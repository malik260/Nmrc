using Microsoft.EntityFrameworkCore;
using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer;
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
    public class RefundService : IRefundService
    {
        private readonly IUnitOfWork _iUnitOfWork;
        private readonly ApplicationDbContext _context;
        private readonly IEmployeeService _employeeService;
        private readonly IContributionService _contributionService;
        private readonly IFinanceCounterpartyTransactionService _financeCounterpartyTransactionService;


        public RefundService(IUnitOfWork iUnitOfWork, ApplicationDbContext context, IEmployeeService employeeService, IContributionService contributionService, IFinanceCounterpartyTransactionService financeCounterpartyTransactionService)
        {
            _iUnitOfWork = iUnitOfWork;
            _context = context;
            _employeeService = employeeService;
            _contributionService = contributionService;
            _financeCounterpartyTransactionService = financeCounterpartyTransactionService;


        }

        #region Retrieve data

        public async Task<TData<List<RefundEntity>>> GetList(RefundListParam param)
        {
            TData<List<RefundEntity>> obj = new TData<List<RefundEntity>>();
            obj.Data = await _iUnitOfWork.Refunds.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<RefundEntity>>> GetPageList(RefundListParam param, Pagination pagination)
        {
            TData<List<RefundEntity>> obj = new TData<List<RefundEntity>>();
            obj.Data = await _iUnitOfWork.Refunds.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ZtreeInfo>>> GetZtreeRefundList(RefundListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<RefundEntity> refundList = await _iUnitOfWork.Refunds.GetList(param);
            foreach (RefundEntity refund in refundList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = refund.Id,
                    name = refund.Name
                });
            }
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ZtreeInfo>>> GetZtreeUserList(RefundListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<RefundEntity> refundList = await _iUnitOfWork.Refunds.GetList(param);
            List<UserEntity> userList = await _iUnitOfWork.Users.GetList(null);
            foreach (RefundEntity refund in refundList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = refund.Id,
                    name = refund.Name
                });
                List<long> userIdList = userList.Where(t => t.Company == refund.Id).Select(t => t.Employee).ToList();
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

        public async Task<TData<RefundEntity>> GetEntity(long id)
        {
            TData<RefundEntity> obj = new TData<RefundEntity>();
            obj.Data = await _iUnitOfWork.Refunds.GetEntity(id);
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
            var noOfRefund = await _iUnitOfWork.Refunds.GetRefundList(refundParam);
            if (noOfRefund.Count() > 0)
            {
                obj.Message = "You currently have an ongoing refund process in progress.";
                obj.Tag = 0;
                return obj;
            }
            //else
            //{
            //    obj.Message = "Proceed to application";
            //    obj.Tag = 1;
            //    return obj;
            //}
            int age = 0;
            int yearsofservice = 0;
            DateTime? EmployementDate = DateTime.Now;
            var amount = 0.00m;
            //var con = _context.FinanceCounterpartyTransactionEntity.Where(i => i.Ref == employeeNumber && i.Approved == 1 && i.TransactionType == "70").Select(x => x.CreditAmount).ToList().Sum();
            var x1 = new FinanceCounterpartyTransactionListParam()
            {
                Ref = user.EmployeeInfo.NHFNumber.ToStr(),
                Approved = 1
            };
            var xx = _financeCounterpartyTransactionService.GetList(x1);
            var totalContribution = xx.Result.Data.Where(i => i.TransactionType == "70").Select(x => x.CreditAmount).ToList().Sum();
            var loanDisbursed = xx.Result.Data.Where(i => i.TransactionType == "70").Select(x => x.DebitAmount).ToList().Sum();
            var totalLoanRepaid = xx.Result.Data.Where(i => i.TransactionType == "71").Select(x => x.CreditAmount).ToList().Sum();
            var loanBalance = loanDisbursed - totalLoanRepaid;
            amount = Convert.ToDecimal(totalContribution - loanBalance);
            var dateOfBirth = customerDetails.Data.DateOfBirth.ToDate();
            DateTime dateOfService = Convert.ToDateTime(customerDetails.Data.DateOfEmployment);
            age = DateTime.Now.Year - dateOfBirth.Year;
            if (DateTime.Now.DayOfYear < dateOfBirth.DayOfYear)
                age = age - 1;
            if (customerDetails.Data.DateOfEmployment != null)
            {
                yearsofservice = DateTime.Now.Year - dateOfService.Year;
                if (DateTime.Now.DayOfYear < dateOfService.DayOfYear)
                    yearsofservice = yearsofservice - 1;
            }

            //var BankName = _context.BankEntity.Where(i => i.Code == customerDetails.Data.CustomerBank).DefaultIfEmpty().FirstOrDefault();
            var custDetails = new CustomerDetailsViewModel
            {
                Branchcode = customerDetails.Data.Branch.ToString(),

                AccountNum = customerDetails.Data.BankAccountNumber,
                Name = customerDetails.Data.LastName + " " + customerDetails.Data.FirstName,
                CustNo = customerDetails.Data.StaffNumber,
                //Dob = Convert.ToDateTime(customerDetails.Data.DateOfBirth),
                //Dob = customerDetails.Data.DateOfBirth.ToDate(),
                Dob = customerDetails.Data.DateOfBirth.ToDate().ToStr("0"),
                EmployerNo = user.CompanyInfo.RCNumber,
                EmploymentDate = customerDetails.Data.DateOfEmployment.ToDate().ToStr("0"),
                Bvn = customerDetails.Data.BVN,
                Nin = customerDetails.Data.NIN,
                MobileNo = customerDetails.Data.MobileNumber,
                ContactAddress = customerDetails.Data.PostalAddress,
                EmployerName = user.CompanyInfo.Name,
                Nhfno = customerDetails.Data.NHFNumber.ToStr(),
                BankAccountNumber = customerDetails.Data.BankAccountNumber,
                Age = age,
                YearsOfService = yearsofservice,
                Amount = Convert.ToString(amount),

                BankName = customerDetails.Data.CustomerBank,
                MonthlyIncome = customerDetails.Data.MonthlySalary
                //BankCode = customerDetails.Data.
            };

            obj.Data = custDetails;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<int>> GetMaxSort()
        {
            TData<int> obj = new TData<int>();
            obj.Data = await _iUnitOfWork.Refunds.GetMaxSort();
            obj.Tag = 1;
            return obj;
        }

        #endregion Retrieve data

        #region Submit data

        public async Task<TData<string>> SaveForm(RefundEntity entity)
        {
            TData<string> obj = new TData<string>();
            var user = await Operator.Instance.Current();
            var customerDetails = await _employeeService.GetEntityByNhfNo(user.EmployeeInfo.NHFNumber);
            entity.NhfNumber = customerDetails.Data.NHFNumber.ToStr();
            entity.BVN = customerDetails.Data.BVN;
            entity.NIN = customerDetails.Data.NIN;
            entity.EmployerNumber = customerDetails.Data.EmployerNo;
            entity.MobileNumber = customerDetails.Data.MobileNumber;
            entity.ContactAddress = customerDetails.Data.PostalAddress;
            entity.CustomerNumber = customerDetails.Data.StaffNumber;
            entity.EmployerName = user.CompanyInfo.Name.ToStr();
            entity.Name = customerDetails.Data.LastName + " " + customerDetails.Data.FirstName;
            entity.EmployerNumber = user.CompanyInfo.RCNumber;

            await _iUnitOfWork.Refunds.SaveForm(entity);
            obj.Data = entity.Id.ParseToString();
            obj.Tag = 1;
            obj.Message = "Refund Application Successful";
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData<long> obj = new TData<long>();
            await _iUnitOfWork.Refunds.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }

        #endregion Submit data
    }
}