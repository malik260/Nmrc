using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories;
using Mortgage.Ecosystem.DataAccess.Layer.Repositories;

namespace Mortgage.Ecosystem.DataAccess.Layer
{
    public class UnitOfWork : IUnitOfWork
    {
        private IAccountTypeRepository? _accountTypes;
        private IAccreditationFeeRepository _accreditationFees;
        private IAgentTypeRepository? _agentTypes;
        private IAlertTypeRepository? _alertTypes;
        private IAllNHFSubscriberRepository? _allnhfsubscribers;
        private IApprovalLogRepository? _approvalLogs;
        private IApprovalSetupRepository? _approvalSetups;
        private IAuditTrailRepository? _AuditTrails;
        private IAutoJobLogRepository? _autoJobLogs;
        private IAutoJobRepository? _autoJobs;
        private IApproveAgentsRepository? _approveAgents;
        private IApproveEmployerAggregatorRepository? _approveEmployerAggregators;
        private IAddDocumentProcedureRepository _addDocumentsProcedure;
        private IBankRepository? _banks;
        private IBranchRepository? _branches;
        private IBrokerRepository? _brokers;
        private IChangeEmployerRepository? _changeEmployers;
        private IChangePasswordRepository? _changePasswords;
        private IChargeSetupRepository? _chargeSetups;
        private IChecklistRepository? _checklists;
        private IChecklistProcedureRepository? _checklistsProcedure;

        private ICompanyRepository? _companies;
        private ICompanyClassRepository? _companyClasses;
        private ICompanyTypeRepository? _companyTypes;
        private IContributionFrequencyRepository? _contributionFrequencies;
        private IContributionRepository? _contributions;
        private IContributionHistoryRepository? _contributionHistories;
        private IContributionRefundPostingRepository? _contributionRefundPostings;
        private ICreditAssessmentIndexRepository? _creditAssessmentIndexes;
        private ICreditAssessmentIndexTitleRepository? _creditAssessmentIndexTitles;
        private ICreditAssessmentFactorIndexRepository _creditAssessmentFactorIndexes;
        private ICreditAssessmentRiskFactorRepository? _creditAssessmentRiskFactors;
        private ICreditScoreRepository? _creditScores;
        private ICreditTypeRepository? _creditTypes;
        private ICustomerProfileUpdateRepository? _customerProfileUpdates;
        private IDepartmentRepository? _departments;
        private IDeveloperRepository? _developers;
        private IDesignationRepository? _designations;
        private IDiasporaUserRepository? _diasporaUsers;
        private IDisbursementRepository? _disbursements;
        private IEmployeeRepository? _employees;
        private IErrorLogRepository? _errorLog;
        private IETicketRepository? _etickets;
        private IFeedBackFormRepository? _feedBackForms;
        private IFinanceCounterpartyTransactionRepository? _financeCounterpartyTransactions;
        private IFinanceTransactionRepository? _financeTransactions;
        private IGenderRepository? _genders;
        private IInternetBankingUsersRepository? _internetBankingUsers;
        private ILoanReviewRepository? _loanReviews;
        private ILenderRepository? _lenders;
        private ILenderTypeRepository? _lenderTypes;
        private ILoanRepaymentRepository? _loanRepayments;
        private ILoanInitiationRepository? _loanInitiations;
        private ILoanInitiationUploadRepository? _loanInitiationUpload;
        private ILoanScheduleRepository? _loanSchedules;
        private ILogLoginRepository? _logLogins;
        private ILogOperateRepository? _logOperates;
        private IMaritalStatusRepository? _maritalStatus;
        private IMenuAuthorizeRepository? _menuAuthorizes;
        private IMenuRepository? _menus;
        private INationalityRepository? _nationalities;
        private INextOfKinRepository? _nextOfKins;
        private INHFCustomerRequestRepository? _nhfcustomerrequests;
        private INHFRegCompanyRepository? _nhfregcompanies;
        private INHFRegUsersRepository? _nhfregusers;
        private IPaymentHistoryRepository? _paymentHistories;
        private IPropertyRegistrationRepository? _propertyRegistrations;
        private IPropertySubscriptionRepository? _propertySubscriptions;
        private IPropertyGalleryRepository? _propertyGalleries;
        private IPropertyUploadRepository? _propertyUploads;
        private IPmbRepository? _pmbs;
        private IRefinancingRepository? _refinancings;
        private IRemitaPaymentDetailsRepository? _remitaPaymentDetails;
        private IRelationRepository? _relations;
        private IRefundRepository? _refunds;
        private IRefundConditionRepository? _refundConditions;
        private IRefundProfilingRepository? _refundProfilings;
        private IRiskAssessmentSetupRepository? _riskAssessmentSetups;
        private IRiskAssessmentProcedureRepository? _riskAssessmentProcedure;

        private IRoleRepository? _roles;
        private ISecondaryLenderRepository? _secondaryLenders;
        private ISecondaryLenderChecklistRepository? _secondaryLenderChecklist;
        private ISchemeRepository? _schemes;
        private ISchemeLenderRepository? _schemeLenders;
        private IStateRepository? _states;
        private IStatementOfAccountRepository? _statementOfAccounts;
        private ISectorRepository? _sectors;
        private ISubSectorRepository? _subSectors;
        private ITitleRepository? _titles;
        private IUnderwritingRepository? _underwritings;
        private IUnlockAdminUserRepository? _unlockAdminUsers;
        private IUnlockNhfPortalRepository? _unlockNhfPortals;
        private IUserBelongRepository? _userBelongs;
        private IUserRepository? _users;
        private IResetPasswordTokenRepository? _resetPasswordTokens;


        public UnitOfWork()
        {
        }

        public IAccountTypeRepository AccountTypes =>
            _accountTypes ??= new AccountTypeRepository();
        public IAccreditationFeeRepository AccreditationFees =>
         _accreditationFees ?? new AccreditationFeeRepository();
        public IAgentTypeRepository AgentTypes =>
            _agentTypes ??= new AgentTypeRepository();

        public IAlertTypeRepository AlertTypes =>
            _alertTypes ??= new AlertTypeRepository();

        public IAllNHFSubscriberRepository AllNHFSubscribers =>
           _allnhfsubscribers ??= new AllNHFSubscriberRepository();

        public IApprovalLogRepository ApprovalLogs =>
            _approvalLogs ??= new ApprovalLogRepository();

        public IApprovalSetupRepository ApprovalSetups =>
            _approvalSetups ??= new ApprovalSetupRepository();

        public IAuditTrailRepository AuditTrails =>
           _AuditTrails ??= new AuditTrailRepository();
        public IAutoJobLogRepository AutoJobLogs =>
            _autoJobLogs ??= new AutoJobLogRepository();

        public IAutoJobRepository AutoJobs =>
            _autoJobs ??= new AutoJobRepository();

        public IApproveAgentsRepository ApproveAgents =>
         _approveAgents ??= new ApproveAgentsRepository();
        public IApproveEmployerAggregatorRepository ApproveEmployerAggregators =>
          _approveEmployerAggregators ??= new ApproveEmployerAggregatorRepository();

        public IAddDocumentProcedureRepository AddDocumentsProcedure =>
         _addDocumentsProcedure ??= new AddDocumentProcedureRepository();

        public IBankRepository Banks =>
            _banks ??= new BankRepository();

        public IBranchRepository Branches =>
            _branches ??= new BranchRepository();


        public IBrokerRepository Brokers =>
        _brokers ??= new BrokerRepository();



        public IChangeEmployerRepository ChangeEmployers =>
          _changeEmployers ??= new ChangeEmployerRepository();


        public IChangePasswordRepository ChangePasswords =>
          _changePasswords ??= new ChangePasswordRepository();

        public IChargeSetupRepository ChargeSetups =>
         _chargeSetups ??= new ChargeSetupRepository();
        public IChecklistRepository Checklists =>
        _checklists ??= new ChecklistRepository();

        public IChecklistProcedureRepository ChecklistsProcedure =>
        _checklistsProcedure ??= new ChecklistProcedureRepository();

        public ICompanyRepository Companies =>
            _companies ??= new CompanyRepository();

        public ICompanyClassRepository CompanyClasses =>
            _companyClasses ??= new CompanyClassRepository();

        public ICompanyTypeRepository CompanyTypes =>
            _companyTypes ??= new CompanyTypeRepository();

        public IContributionFrequencyRepository ContributionFrequencies =>
            _contributionFrequencies ??= new ContributionFrequencyRepository();

        public IContributionHistoryRepository ContributionHistories =>
        _contributionHistories ??= new ContributionHistoryRepository();

        public IContributionRepository Contributions =>
            _contributions ??= new ContributionRepository();

        public IContributionRefundPostingRepository ContributionRefundPostings =>
         _contributionRefundPostings ??= new ContributionRefundPostingRepository();
        public ICreditAssessmentFactorIndexRepository CreditAssessmentFactorIndexes =>
      _creditAssessmentFactorIndexes ??= new CreditAssessmentFactorIndexRepository();

        public ICreditAssessmentIndexRepository CreditAssessmentIndexes =>
       _creditAssessmentIndexes ??= new CreditAssessmentIndexRepository();

        public ICreditAssessmentIndexTitleRepository CreditAssessmentIndexTitles =>
        _creditAssessmentIndexTitles ??= new CreditAssessmentIndexTitleRepository();
        public ICreditAssessmentRiskFactorRepository CreditAssessmentRiskFactors =>
        _creditAssessmentRiskFactors ??= new CreditAssessmentRiskFactorRepository();

        public ICreditScoreRepository CreditScores =>
     _creditScores ??= new CreditScoreRepository();
        public ICreditTypeRepository CreditTypes =>
         _creditTypes ??= new CreditTypeRepository();
        public IFinanceCounterpartyTransactionRepository FinanceCounterpartyTransactions =>
          _financeCounterpartyTransactions ??= new FinanceCounterpartyTransactionRepository();
        public ICustomerProfileUpdateRepository CustomerProfileUpdates =>
      _customerProfileUpdates ??= new CustomerProfileUpdateRepository();
        public IDepartmentRepository Departments =>
            _departments ??= new DepartmentRepository();
        public IDisbursementRepository Disbursements =>
        _disbursements ??= new DisbursementRepository();
        public IPmbRepository Pmbs =>
          _pmbs ??= new PmbRepository();

        public IDesignationRepository Designations =>
            _designations ??= new DesignationRepository();

        public IDiasporaUserRepository DiasporaUsers =>
       _diasporaUsers ??= new DiasporaUserRepository();

        public IDeveloperRepository Developers =>
       _developers ??= new DeveloperRepository();

        public IEmployeeRepository Employees =>
            _employees ??= new EmployeeRepository();

        public IErrorLogRepository ErrorLog =>
                    _errorLog ??= new ErrorLogRepository();

        public IETicketRepository ETickets =>
        _etickets ??= new ETicketRepository();
        public IFeedBackFormRepository FeedBackForms =>
          _feedBackForms ??= new FeedBackFormRepository();

        public IFinanceTransactionRepository FinanceTransactions =>
        _financeTransactions ??= new FinanceTransactionRepository();

        public IGenderRepository Genders =>
            _genders ??= new GenderRepository();

        public IInternetBankingUsersRepository InternetBankingUsers =>
        _internetBankingUsers ??= new InternetBankingUsersRepository();

        public ILoanReviewRepository LoanReviews =>
         _loanReviews ??= new LoanReviewRepository();

        public ILoanRepaymentRepository LoanRepayments =>
           _loanRepayments ??= new LoanRepaymentRepository();

        public ILenderRepository Lenders =>
      _lenders ??= new LenderRepository();

        public ILenderTypeRepository LenderTypes =>
        _lenderTypes ??= new LenderTypeRepository();
        public ILoanInitiationRepository LoanInitiations =>
        _loanInitiations ??= new LoanInitiationRepository();

        public ILoanInitiationUploadRepository LoanInitiationUploads =>
            _loanInitiationUpload ??= new LoanInitiationUploadRepository();

        public ILoanScheduleRepository LoanSchedules =>
          _loanSchedules ??= new LoanScheduleRepository();

        public ILogLoginRepository LogLogins =>
            _logLogins ??= new LogLoginRepository();

        public ILogOperateRepository LogOperates =>
            _logOperates ??= new LogOperateRepository();

        public IMaritalStatusRepository MaritalStatus =>
            _maritalStatus ??= new MaritalStatusRepository();

        public IMenuAuthorizeRepository MenuAuthorizes =>
            _menuAuthorizes ??= new MenuAuthorizeRepository();

        public IMenuRepository Menus =>
            _menus ??= new MenuRepository();

        public INationalityRepository Nationalities =>
            _nationalities ??= new NationalityRepository();

        public INextOfKinRepository NextOfKins =>
            _nextOfKins ??= new NextOfKinRepository();

        public INHFRegUsersRepository NHFRegUsers =>
          _nhfregusers ??= new NHFRegUsersRepository();
        public INHFRegCompanyRepository NHFRegCompanies =>
         _nhfregcompanies ??= new NHFRegCompanyRepository();

        public INHFCustomerRequestRepository NHFCustomerRequests =>
            _nhfcustomerrequests ??= new NHFCustomerRequestRepository();

        public IPaymentHistoryRepository PaymentHistories =>
            _paymentHistories ??= new PaymentHistoryRepository();

        public IPropertySubscriptionRepository PropertySubscriptions =>
         _propertySubscriptions ??= new PropertySubscriptionRepository();

        public IPropertyGalleryRepository PropertyGalleries =>
         _propertyGalleries ??= new PropertyGalleryRepository();
        public IPropertyRegistrationRepository PropertyRegistrations =>
          _propertyRegistrations ??= new PropertyRegistrationRepository();
        public IPropertyUploadRepository PropertyUploads =>
          _propertyUploads ??= new PropertyUploadRepository();
        public IRefinancingRepository Refinancings =>
          _refinancings ??= new RefinancingRepository();

        public IRefundRepository Refunds =>
          _refunds ??= new RefundRepository();
        public IRefundConditionRepository RefundConditions =>
          _refundConditions ??= new RefundConditionRepository();
        public IRefundProfilingRepository RefundProfilings =>
          _refundProfilings ??= new RefundProfilingRepository();

        public IRemitaPaymentDetailsRepository RemitaPaymentDetails =>
          _remitaPaymentDetails ??= new RemitaPaymentDetailsRepository();

        public IRelationRepository Relations =>
            _relations ??= new RelationRepository();
        public IRiskAssessmentSetupRepository RiskAssessmentSetups =>
     _riskAssessmentSetups ??= new RiskAssessmentSetupRepository();
        public IRiskAssessmentProcedureRepository RiskAssessmentProcedure =>
         _riskAssessmentProcedure ??= new RiskAssessmentProcedureRepository();

        public IResetPasswordTokenRepository ResetPasswordTokens =>
          _resetPasswordTokens ??= new ResetPasswordTokenRepository();
        public IRoleRepository Roles =>
            _roles ??= new RoleRepository();
        public ISchemeRepository Schemes =>
         _schemes ??= new SchemeRepository();

        public ISecondaryLenderRepository SecondaryLenders =>
     _secondaryLenders ??= new SecondaryLenderRepository();

        public ISecondaryLenderChecklistRepository SecondaryLenderChecklist =>
        _secondaryLenderChecklist ??= new SecondaryLenderChecklistRepository();

        public ISchemeLenderRepository SchemeLenders =>
  _schemeLenders ??= new SchemeLenderRepository();
        public IStateRepository States =>
            _states ??= new StateRepository();
        public IStatementOfAccountRepository StatementOfAccounts =>
    _statementOfAccounts ??= new StatementOfAccountRepository();

        public ISectorRepository Sectors =>
            _sectors ??= new SectorRepository();

        public ISubSectorRepository SubSectors =>
            _subSectors ??= new SubSectorRepository();

        public ITitleRepository Titles =>
            _titles ??= new TitleRepository();
        public IUnderwritingRepository Underwritings =>
       _underwritings ??= new UnderwritingRepository();
        public IUnlockAdminUserRepository UnlockAdminUsers =>
       _unlockAdminUsers ??= new UnlockAdminUserRepository();

        public IUnlockNhfPortalRepository UnlockNhfPortals =>
         _unlockNhfPortals ??= new UnlockNhfPortalRepository();

        public IUserBelongRepository UserBelongs =>
            _userBelongs ??= new UserBelongRepository();

        public IUserRepository Users =>
            _users ??= new UserRepository();
    }
}