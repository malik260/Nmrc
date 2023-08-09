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
    public class LoanRepaymentService : ILoanRepaymentService
    {
        private readonly IUnitOfWork _iUnitOfWork;
        private readonly IPaymentIntegrationService paymentIntegrationService;

        public LoanRepaymentService(IUnitOfWork iUnitOfWork, IPaymentIntegrationService paymentIntegrationService)
        {
            _iUnitOfWork = iUnitOfWork;
            this.paymentIntegrationService = paymentIntegrationService;
        }


        #region Retrieve data
        public async Task<TData<List<LoanRepaymentEntity>>> GetList(LoanRepaymentListParam param)
        {
            TData<List<LoanRepaymentEntity>> obj = new TData<List<LoanRepaymentEntity>>();
            obj.Data = await _iUnitOfWork.LoanRepayments.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<LoanRepaymentEntity>>> GetPageList(LoanRepaymentListParam param, Pagination pagination)
        {
            TData<List<LoanRepaymentEntity>> obj = new TData<List<LoanRepaymentEntity>>();
            obj.Data = await _iUnitOfWork.LoanRepayments.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ZtreeInfo>>> GetZtreeLoanRepaymentList(LoanRepaymentListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<LoanRepaymentEntity> loanRepaymentList = await _iUnitOfWork.LoanRepayments.GetList(param);
            foreach (LoanRepaymentEntity loanRepayment in loanRepaymentList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = loanRepayment.Id,
                    name = loanRepayment.Firstname
                });
            }
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ZtreeInfo>>> GetZtreeUserList(LoanRepaymentListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<LoanRepaymentEntity> loanRepaymentList = await _iUnitOfWork.LoanRepayments.GetList(param);
            List<UserEntity> userList = await _iUnitOfWork.Users.GetList(null);
            foreach (LoanRepaymentEntity loanRepayment in loanRepaymentList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = loanRepayment.Id,
                    name = loanRepayment.Firstname
                });
                List<long> userIdList = userList.Where(t => t.Company == loanRepayment.Id).Select(t => t.Employee).ToList();
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

        public async Task<TData<LoanRepaymentEntity>> GetEntity(long id)
        {
            TData<LoanRepaymentEntity> obj = new TData<LoanRepaymentEntity>();
            obj.Data = await _iUnitOfWork.LoanRepayments.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<int>> GetMaxSort()
        {
            TData<int> obj = new TData<int>();
            obj.Data = await _iUnitOfWork.LoanRepayments.GetMaxSort();
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region Submit data
        public async Task<TData<string>> SaveForm(LoanRepaymentEntity entity)
        {
            TData<string> obj = new TData<string>();
            await _iUnitOfWork.LoanRepayments.SaveForm(entity);
            obj.Data = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData<long> obj = new TData<long>();
            await _iUnitOfWork.LoanRepayments.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion


        public async Task<TData<RemitaPaymentDetailsEntity>> SingleLoanRepayment(LoanRepaymentDto entity)
        {
            OperatorInfo loggedUserinfo = new OperatorInfo();
            var UserCpt = await _iUnitOfWork.FinanceCounterpartyTransactions.GetEntity(loggedUserinfo.Employee);
            var debitAmount = UserCpt.DebitAmount;
            var creditAmount = UserCpt.CreditAmount;
            var loanBalance = debitAmount - creditAmount;
            TData<RemitaPaymentDetailsEntity> obj = new TData<RemitaPaymentDetailsEntity>();

            if (entity.Amount > loanBalance)
            {
                obj.Message = "Repayment Amount cannot be greater than Loan balance";
                obj.Tag = 0;
                return obj;
            }
            var employeeinfo = await _iUnitOfWork.Employees.GetEntity(loggedUserinfo.Id);
            RemitaPaymentDTO PaymentDetails = new RemitaPaymentDTO();
            PaymentDetails.amount = entity.Amount;
            PaymentDetails.description = entity.Narration;
            PaymentDetails.payerEmail = employeeinfo.EmailAddress.ToStr();
            PaymentDetails.payerPhone = employeeinfo.MobileNumber.ToStr();
            PaymentDetails.payerName = employeeinfo.FirstName + " " + employeeinfo.LastName;

            var Rrrgenerator = await paymentIntegrationService.GenerateRRR(PaymentDetails);
            if (Rrrgenerator.Data == null)
            {
                obj.Message = "Coult not generate RRR";
                obj.Tag = 0;
                return obj;
            }
            RemitaPaymentDetailsEntity remitaPaymentDetailsEntity = new RemitaPaymentDetailsEntity();

            remitaPaymentDetailsEntity.TransactionId = Rrrgenerator.Data.TransactionId;
            remitaPaymentDetailsEntity.TransactionDate = DateTime.Now;
            remitaPaymentDetailsEntity.Status = 0;
            remitaPaymentDetailsEntity.Rrr = Rrrgenerator.Data.RRR;
            remitaPaymentDetailsEntity.Amount = PaymentDetails.amount.ToStr();
            remitaPaymentDetailsEntity.EmployeeNumber = employeeinfo.Id.ToString();


            LoanRepaymentEntity loanRepaymentEntity = new LoanRepaymentEntity();
            loanRepaymentEntity.Amount = entity.Amount;
            loanRepaymentEntity.PaymentStatus = "0";
            loanRepaymentEntity.EmployeeNumber = employeeinfo.Id.ToStr();
            loanRepaymentEntity.EmployerNumber = "";
            loanRepaymentEntity.Narration = entity.Narration;
            loanRepaymentEntity.Transactionid = Rrrgenerator.Data.RRR;
            loanRepaymentEntity.Amount = entity.Amount;
            loanRepaymentEntity.Month = entity.Month;
            loanRepaymentEntity.Year = entity.Year;
            loanRepaymentEntity.Firstname = employeeinfo.FirstName.ToStr();
            loanRepaymentEntity.Lastname = employeeinfo.LastName.ToStr();
            loanRepaymentEntity.MiddleName = employeeinfo.OtherName.ToStr();
            loanRepaymentEntity.Valuedate = DateTime.Now;
            loanRepaymentEntity.PaymentStatus = entity.Paymentoption.ToString();
            loanRepaymentEntity.Repaymentdate = DateTime.Now;

            FinanceCounterpartyTransactionEntity CPT = new FinanceCounterpartyTransactionEntity();
            CPT.Ref = employeeinfo.Id.ToStr();
            CPT.DebitAmount = 0;
            CPT.CreditAmount = entity.Amount;
            CPT.TransactionDate = DateTime.Now;
            CPT.TransactionType = "71";
            CPT.TransactionId = int.Parse(Rrrgenerator.Data.RRR);
            CPT.Approved = 0;
            CPT.Branch = employeeinfo.Branch.ToStr();
            CPT.Description = entity.Narration;
            CPT.OldAccountNo = employeeinfo.BankAccountNumber.ToStr();

            FinanceTransactionEntity FttCredit = new FinanceTransactionEntity();
            FttCredit.CreditAmt = entity.Amount;
            FttCredit.TransactionDate = DateTime.Now;
            FttCredit.TransactionType = 71;
            FttCredit.Approved = 0;
            FttCredit.ApprovedBy = string.Empty;
            FttCredit.DebitAmt = 0;
            FttCredit.Description = entity.Narration; ;
            FttCredit.DestinationBranch = employeeinfo.Branch.ToString();
            FttCredit.ValueDate = DateTime.Now;
            FttCredit.Ref = employeeinfo.Id.ToString();
            FttCredit.TransactionId = Rrrgenerator.Data.RRR;

            FinanceTransactionEntity FttDebit = new FinanceTransactionEntity();
            FttDebit.CreditAmt = 0;
            FttDebit.TransactionDate = DateTime.Now;
            FttDebit.TransactionType = 71;
            FttDebit.Approved = 0;
            FttDebit.ApprovedBy = string.Empty;
            FttDebit.DebitAmt = entity.Amount;
            FttDebit.Description = entity.Narration; ;
            FttDebit.DestinationBranch = employeeinfo.Branch.ToString();
            FttDebit.ValueDate = DateTime.Now;
            FttDebit.Ref = employeeinfo.Id.ToString();
            FttDebit.TransactionId = Rrrgenerator.Data.RRR;

            await _iUnitOfWork.RemitaPaymentDetails.SaveForm(remitaPaymentDetailsEntity);
            await _iUnitOfWork.LoanRepayments.SaveForm(loanRepaymentEntity);
            await _iUnitOfWork.FinanceCounterpartyTransactions.SaveForm(CPT);
            await _iUnitOfWork.FinanceTransactions.SaveForm(FttCredit);
            await _iUnitOfWork.FinanceTransactions.SaveForm(FttDebit);

            obj.Message = "RRR Generataed succesfully";
            obj.Data = remitaPaymentDetailsEntity;
            obj.Tag = 0;
            return obj;
        }

    }
}
