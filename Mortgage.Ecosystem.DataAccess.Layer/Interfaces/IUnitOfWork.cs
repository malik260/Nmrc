using Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories;
using Mortgage.Ecosystem.DataAccess.Layer.Repositories;

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
        IAuditTrailRepository AuditTrails { get; }
        IAutoJobLogRepository AutoJobLogs { get; }
        IAutoJobRepository AutoJobs { get; }
        IApproveEmployerAggregatorRepository ApproveEmployerAggregators { get; }
        IAddDocumentProcedureRepository AddDocumentsProcedure { get; }
        IBankRepository Banks { get; }
        IBrokerRepository Brokers { get; }
        IBranchRepository Branches { get; }
        IChangeEmployerRepository ChangeEmployers { get; }
        IChangePasswordRepository ChangePasswords { get; }
        IChargeSetupRepository ChargeSetups { get; }
        IChecklistRepository Checklists { get; }
        IChecklistProcedureRepository ChecklistsProcedure { get; }

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
        IDisbursementRepository Disbursements { get; }
        IDepartmentRepository Departments { get; }
        IDesignationRepository Designations { get; }
        IDiasporaUserRepository DiasporaUsers { get; }
        IDeveloperRepository Developers { get; }
        IEmployeeRepository Employees { get; }
        IErrorLogRepository ErrorLog { get; }
        IETicketRepository ETickets { get; }
        IFeedBackFormRepository FeedBackForms { get; }
        IFinanceCounterpartyTransactionRepository FinanceCounterpartyTransactions { get; }
        IFinanceTransactionRepository FinanceTransactions { get; }
        IGenderRepository Genders { get; }
        IInternetBankingUsersRepository InternetBankingUsers { get; }
         ILenderRepository Lenders { get; }
         ILenderTypeRepository LenderTypes { get; }
        ILoanInitiationRepository LoanInitiations { get; }
        ILoanInitiationUploadRepository? LoanInitiationUploads { get; }
        ILoanReviewRepository LoanReviews { get; }
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
        INHFRegCompanyRepository NHFRegCompanies { get; }
        INmrcCategoryRepository NmrcCategories { get; }
        INmrcActivityRepository NmrcActivity{ get; }
        INmrcEligibilityRepository NmrcEligibilities { get; }
        INHFRegUsersRepository NHFRegUsers { get; }
        IRefundRepository Refunds { get; }
        IRefundConditionRepository RefundConditions { get; }
        IRefundProfilingRepository RefundProfilings { get; }
        IRiskAssessmentSetupRepository RiskAssessmentSetups { get; }
        IRiskAssessmentProcedureRepository RiskAssessmentProcedure { get; }
        ISchemeRepository Schemes { get; }
        IPmbRepository Pmbs { get; }
        IPaymentHistoryRepository PaymentHistories { get; }
        IPropertySubscriptionRepository PropertySubscriptions { get; }
        IPropertyRegistrationRepository PropertyRegistrations { get; }
        IPropertyGalleryRepository PropertyGalleries { get; }
        IPropertyUploadRepository PropertyUploads { get; }
        IRemitaPaymentDetailsRepository RemitaPaymentDetails { get; }
        IRefinancingRepository Refinancings { get; }
        IRelationRepository Relations { get; }
        IResetPasswordTokenRepository ResetPasswordTokens { get; }

        IRoleRepository Roles { get; }
        ISecondaryLenderRepository SecondaryLenders { get; }
        IStateRepository States { get; }
        ISchemeLenderRepository SchemeLenders { get; }
        IStatementOfAccountRepository StatementOfAccounts { get; }
        ISectorRepository Sectors { get; }
        ISubSectorRepository SubSectors { get; }
        ITitleRepository Titles { get; }
        IUnderwritingRepository Underwritings { get; }
        IUnlockAdminUserRepository UnlockAdminUsers { get; }
        IUnlockNhfPortalRepository UnlockNhfPortals { get; }
        IUserBelongRepository UserBelongs { get; }
        IUserRepository Users { get; }
        ILoanDisbursementRepository LoanDisbursement { get; }
    }
}