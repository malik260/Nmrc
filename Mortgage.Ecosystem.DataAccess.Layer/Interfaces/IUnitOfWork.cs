using Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces
{
    public interface IUnitOfWork
    {
        IAccountTypeRepository AccountTypes { get; }
        IAccreditationFeeRepository AccreditationFees { get; }

        IAgentTypeRepository AgentTypes { get; }
        IAlertTypeRepository AlertTypes { get; }
        IAllNHFSubscriberRepository AllNHFSubscribers { get; }
        IApprovalLogRepository ApprovalLogs { get; }
        IApprovalSetupRepository ApprovalSetups { get; }
        IApproveAgentsRepository ApproveAgents { get; }
        IAutoJobLogRepository AutoJobLogs { get; }
        IAutoJobRepository AutoJobs { get; }
        IApproveEmployerAggregatorRepository ApproveEmployerAggregators { get; }
        IBankRepository Banks { get; }
        IBranchRepository Branches { get; }
        IChangeEmployerRepository ChangeEmployers { get; }
        IChangePasswordRepository ChangePasswords { get; }
        IChargeSetupRepository ChargeSetups { get; }

        ICompanyRepository Companies { get; }
        ICompanyClassRepository CompanyClasses { get; }
        ICompanyTypeRepository CompanyTypes { get; }
        IContributionFrequencyRepository ContributionFrequencies { get; }
        IContributionRepository Contributions { get; }
        IContributionRefundPostingRepository ContributionRefundPostings { get; }
        IContributionHistoryRepository ContributionHistories { get; }
        ICreditAssessmentFactorIndexRepository CreditAssessmentFactorIndexes { get; }
        ICreditAssessmentIndexRepository CreditAssessmentIndexes { get; }
        ICreditAssessmentIndexTitleRepository CreditAssessmentIndexTitles { get; }
        ICreditAssessmentRiskFactorRepository CreditAssessmentRiskFactors { get; }
        ICreditScoreRepository CreditScores { get; }
        ICreditTypeRepository CreditTypes { get; }
        ICustomerProfileUpdateRepository CustomerProfileUpdates { get; }
        IDepartmentRepository Departments { get; }
        IDesignationRepository Designations { get; }
        IDiasporaUserRepository DiasporaUsers { get; }
        IEmployeeRepository Employees { get; }
        IETicketRepository ETickets { get; }
        IFeedBackFormRepository FeedBackForms { get; }
        IFinanceCounterpartyTransactionRepository FinanceCounterpartyTransactions { get; }
        IFinanceTransactionRepository FinanceTransactions { get; }
        IGenderRepository Genders { get; }
        IInternetBankingUsersRepository InternetBankingUsers { get; }
        ILoanInitiationRepository LoanInitiations { get; }
        ILoanRepaymentRepository LoanRepayments { get; }
        ILoanScheduleRepository LoanSchedules { get; }
        ILogLoginRepository LogLogins { get; }
        ILogOperateRepository LogOperates { get; }
        IMaritalStatusRepository MaritalStatus { get; }
        IMenuAuthorizeRepository MenuAuthorizes { get; }
        IMenuRepository Menus { get; }
        INationalityRepository Nationalities { get; }
        INextOfKinRepository NextOfKins { get; }
        INHFCustomerRequestRepository NHFCustomerRequests { get; }
        INHFRegUsersRepository NHFRegUsers { get; }
        IRefundRepository Refunds { get; }
        IRefundConditionRepository RefundConditions { get; }
        IRefundProfilingRepository RefundProfilings { get; }
        IRiskAssessmentSetupRepository RiskAssessmentSetups { get; }

        IPaymentHistoryRepository PaymentHistories { get; }
        IPropertySubscriptionRepository PropertySubscriptions { get; }
        IPropertyRegistrationRepository PropertyRegistrations { get; }
        IPropertyGalleryRepository PropertyGalleries { get; }
        IRemitaPaymentDetailsRepository RemitaPaymentDetails { get; }
        IRelationRepository Relations { get; }
        IRoleRepository Roles { get; }
        IStateRepository States { get; }
        IStatementOfAccountRepository StatementOfAccounts { get; }
        ISectorRepository Sectors { get; }
        ISubSectorRepository SubSectors { get; }
        ITitleRepository Titles { get; }
        IUnderwritingRepository Underwritings { get; }
        IUnlockNhfPortalRepository UnlockNhfPortals { get; }
        IUserBelongRepository UserBelongs { get; }
        IUserRepository Users { get; }
    }
}