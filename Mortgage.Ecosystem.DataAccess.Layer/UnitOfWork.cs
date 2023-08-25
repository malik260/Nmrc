using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories;
using Mortgage.Ecosystem.DataAccess.Layer.Repositories;

namespace Mortgage.Ecosystem.DataAccess.Layer
{
    public class UnitOfWork : IUnitOfWork
    {
        private IAccountTypeRepository? _accountTypes;
        private IAgentTypeRepository? _agentTypes;
        private IAlertTypeRepository? _alertTypes;
        private IAllNHFSubscriberRepository? _allnhfsubscribers;
        private IApprovalLogRepository? _approvalLogs;
        private IApprovalSetupRepository? _approvalSetups;
        private IAutoJobLogRepository? _autoJobLogs;
        private IAutoJobRepository? _autoJobs;
        private IApproveAgentsRepository? _approveAgents;
        private IApproveEmployerAggregatorRepository? _approveEmployerAggregators;
        private IBankRepository? _banks;
        private IBranchRepository? _branches;
        private IChangeEmployerRepository? _changeEmployers;
        private IChangePasswordRepository? _changePasswords;
        private ICompanyRepository? _companies;
        private ICompanyClassRepository? _companyClasses;
        private ICompanyTypeRepository? _companyTypes;
        private IContributionFrequencyRepository? _contributionFrequencies;
        private IContributionRepository? _contributions;
        private IContributionHistoryRepository? _contributionHistories;
        private IContributionRefundPostingRepository? _contributionRefundPostings;
        private ICreditScoreRepository? _creditScores;
        private ICreditTypeRepository? _creditTypes;
        private ICustomerProfileUpdateRepository? _customerProfileUpdates;
        private IDepartmentRepository? _departments;
        private IDesignationRepository? _designations;
        private IDiasporaUserRepository? _diasporaUsers;
        private IEmployeeRepository? _employees;
        private IETicketRepository? _etickets;
        private IFeedBackFormRepository? _feedBackForms;
        private IFinanceCounterpartyTransactionRepository? _financeCounterpartyTransactions;
        private IFinanceTransactionRepository? _financeTransactions;
        private IGenderRepository? _genders;
        private IInternetBankingUsersRepository? _internetBankingUsers;
        private ILoanRepaymentRepository? _loanRepayments;
        private ILoanInitiationRepository? _loanInitiations;
        private ILoanScheduleRepository? _loanSchedules;
        private ILogLoginRepository? _logLogins;
        private ILogOperateRepository? _logOperates;
        private IMaritalStatusRepository? _maritalStatus;
        private IMenuAuthorizeRepository? _menuAuthorizes;
        private IMenuRepository? _menus;
        private INationalityRepository? _nationalities;
        private INextOfKinRepository? _nextOfKins;
        private INHFCustomerRequestRepository? _nhfcustomerrequests;
        private INHFRegUsersRepository? _nhfregusers;
        private IPaymentHistoryRepository? _paymentHistories;
        private IPropertyRegistrationRepository? _propertyRegistrations;
        private IPropertySubscriptionRepository? _propertySubscriptions;
        private IPropertyGalleryRepository? _propertyGalleries;
        private IRemitaPaymentDetailsRepository? _remitaPaymentDetails;
        private IRelationRepository? _relations;
        private IRefundRepository? _refunds;
        private IRefundConditionRepository? _refundConditions;
        private IRefundProfilingRepository? _refundProfilings;
        private IRoleRepository? _roles;
        private IStateRepository? _states;
        private ISectorRepository? _sectors;
        private ISubSectorRepository? _subSectors;
        private ITitleRepository? _titles;
        private IUnlockNhfPortalRepository? _unlockNhfPortals;
        private IUserBelongRepository? _userBelongs;
        private IUserRepository? _users;

        public UnitOfWork()
        {
        }

        public IAccountTypeRepository AccountTypes =>
            _accountTypes ??= new AccountTypeRepository();

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

        public IAutoJobLogRepository AutoJobLogs =>
            _autoJobLogs ??= new AutoJobLogRepository();

        public IAutoJobRepository AutoJobs =>
            _autoJobs ??= new AutoJobRepository();

        public IApproveAgentsRepository ApproveAgents =>
         _approveAgents ??= new ApproveAgentsRepository();
        public IApproveEmployerAggregatorRepository ApproveEmployerAggregators =>
          _approveEmployerAggregators ??= new ApproveEmployerAggregatorRepository();

        public IBankRepository Banks =>
            _banks ??= new BankRepository();

        public IBranchRepository Branches =>
            _branches ??= new BranchRepository();

        public IChangeEmployerRepository ChangeEmployers =>
          _changeEmployers ??= new ChangeEmployerRepository();


        public IChangePasswordRepository ChangePasswords =>
          _changePasswords ??= new ChangePasswordRepository();


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

        public IDesignationRepository Designations =>
            _designations ??= new DesignationRepository();

        public IDiasporaUserRepository DiasporaUsers =>
       _diasporaUsers ??= new DiasporaUserRepository();

        public IEmployeeRepository Employees =>
            _employees ??= new EmployeeRepository();

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

        public ILoanRepaymentRepository LoanRepayments =>
           _loanRepayments ??= new LoanRepaymentRepository();

        public ILoanInitiationRepository LoanInitiations =>
        _loanInitiations ??= new LoanInitiationRepository();

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

        public IRoleRepository Roles =>
            _roles ??= new RoleRepository();

        public IStateRepository States =>
            _states ??= new StateRepository();

        public ISectorRepository Sectors =>
            _sectors ??= new SectorRepository();

        public ISubSectorRepository SubSectors =>
            _subSectors ??= new SubSectorRepository();

        public ITitleRepository Titles =>
            _titles ??= new TitleRepository();

        public IUnlockNhfPortalRepository UnlockNhfPortals =>
         _unlockNhfPortals ??= new UnlockNhfPortalRepository();

        public IUserBelongRepository UserBelongs =>
            _userBelongs ??= new UserBelongRepository();

        public IUserRepository Users =>
            _users ??= new UserRepository();
    }
}