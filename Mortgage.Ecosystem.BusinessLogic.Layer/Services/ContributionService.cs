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
    public class ContributionService : IContributionService
    {
        private readonly IUnitOfWork _iUnitOfWork;
        private readonly IPaymentIntegrationService paymentIntegrationService;
        public ContributionService(IUnitOfWork iUnitOfWork, IPaymentIntegrationService _ipaymentintegrationservice)
        {
            _iUnitOfWork = iUnitOfWork;
            paymentIntegrationService = _ipaymentintegrationservice;
        }

        #region Retrieve data
        public async Task<TData<List<ContributionEntity>>> GetList(ContributionParam param)
        {
            TData<List<ContributionEntity>> obj = new TData<List<ContributionEntity>>();
            obj.Data = await _iUnitOfWork.Contributions.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ContributionEntity>>> GetPageList(ContributionParam param, Pagination pagination)
        {
            TData<List<ContributionEntity>> obj = new TData<List<ContributionEntity>>();
            obj.Data = await _iUnitOfWork.Contributions.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ZtreeInfo>>> GetZtreeSingleContributionList(ContributionParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<ContributionEntity> contributionList = await _iUnitOfWork.Contributions.GetList(param);
            foreach (ContributionEntity contribution in contributionList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = contribution.Id,
                    name = contribution.Name
                });
            }
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ZtreeInfo>>> GetZtreeUserList(ContributionParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<ContributionEntity> contributionList = await _iUnitOfWork.Contributions.GetList(param);
            List<UserEntity> userList = await _iUnitOfWork.Users.GetList(null);
            foreach (ContributionEntity contribution in contributionList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = contribution.Id,
                    name = contribution.Name
                });
                List<long> userIdList = userList.Where(t => t.Company == contribution.Id).Select(t => t.Employee).ToList();
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

        public async Task<TData<ContributionEntity>> GetEntity(long id)
        {
            TData<ContributionEntity> obj = new TData<ContributionEntity>();
            obj.Data = await _iUnitOfWork.Contributions.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<int>> GetMaxSort()
        {
            TData<int> obj = new TData<int>();
            obj.Data = await _iUnitOfWork.Contributions.GetMaxSort();
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region Submit data
        public async Task<TData<string>> SaveForm(ContributionEntity entity)
        {
            TData<string> obj = new TData<string>();
            await _iUnitOfWork.Contributions.SaveForm(entity);
            obj.Data = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData<long> obj = new TData<long>();
            await _iUnitOfWork.Contributions.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion

        public async Task<TData<RemitaPaymentDetailsEntity>> SingleContribution(ContributionEntity entity)
        {
            string message = "";
            var employeedetails = await _iUnitOfWork.Employees.GetEntity(entity.Id);
            RemitaPaymentDTO PaymentDetails = new RemitaPaymentDTO();
            PaymentDetails.amount = entity.contributionAmount;
            PaymentDetails.description = entity.naration;
            PaymentDetails.payerEmail = entity.Email;
            PaymentDetails.payerPhone = entity.phoneNumber;
            PaymentDetails.payerName = employeedetails.FirstName + " " + employeedetails.LastName;

            var Rrrgenerator = await paymentIntegrationService.GenerateRRR(PaymentDetails);
            if (Rrrgenerator.Data == null)
            {
                message = "Failed to Generate RRR";
            }
            RemitaPaymentDetailsEntity remitaPaymentDetailsEntity = new RemitaPaymentDetailsEntity();
            remitaPaymentDetailsEntity.TransactionId = Rrrgenerator.Data.TransactionId;
            remitaPaymentDetailsEntity.TransactionDate = DateTime.Now;
            remitaPaymentDetailsEntity.Status = 0;
            remitaPaymentDetailsEntity.Rrr = Rrrgenerator.Data.RRR;
            remitaPaymentDetailsEntity.Amount = PaymentDetails.amount.ToStr();
            remitaPaymentDetailsEntity.EmployeeNumber = entity.employeeNumber;

            FinanceCounterpartyTransactionEntity CPT = new FinanceCounterpartyTransactionEntity();
            CPT.Ref = entity.employeeNumber;
            CPT.Approved = 0;
            CPT.Branch = employeedetails.Branch.ToStr();
            CPT.TransactionType = "70";
            CPT.PostDate = DateTime.Now;
            CPT.TransactionDate = DateTime.Now;
            CPT.Description = entity.naration;
            CPT.CreditAmount = entity.contributionAmount;

            FinanceTransactionEntity FTT = new FinanceTransactionEntity();
            FTT.DebitAmt = 0;
            FTT.Approved = 0;
            FTT.CreditAmt = entity.contributionAmount;
            FTT.DestinationBranch = employeedetails.Branch.ToStr();
            FTT.TransactionDate = DateTime.Now;
            FTT.TransactionType = 70;
            FTT.ValueDate = DateTime.Now;
            FTT.Ref = entity.employeeNumber;
            FTT.SourceBranch = employeedetails.Branch.ToStr();

            await _iUnitOfWork.RemitaPaymentDetails.SaveForm(remitaPaymentDetailsEntity);
            await _iUnitOfWork.FinanceCounterpartyTransactions.SaveForm(CPT);
            await _iUnitOfWork.FinanceTransactions.SaveForm(FTT);

            TData<RemitaPaymentDetailsEntity> obj = new TData<RemitaPaymentDetailsEntity>();
            obj.Data = remitaPaymentDetailsEntity;
            obj.Tag = 1;
            return obj;

        }

    }

}
