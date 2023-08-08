using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.BusinessLogic.Layer.Services;
using Mortgage.Ecosystem.DataAccess.Layer;
using Mortgage.Ecosystem.DataAccess.Layer.Configurations;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Base;
using Mortgage.Ecosystem.DataAccess.Layer.Models;
using Mortgage.Ecosystem.DataAccess.Layer.Repositories.Base;

namespace Mortgage.Ecosystem.Web.Authorization
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPortalSettings(this IServiceCollection services)
        {
            SetConfiguration();

            services.AddApplicationDbContext();
            services.AddConfigureServices();

            return services;
        }

        private static IServiceCollection AddApplicationDbContext(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>();
            return services;
        }

        private static IServiceCollection AddConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IDbRepository, DbRepository>();
            //services.AddScoped<BaseBusinessCache<T>>();
            //services.AddScoped<MenuAuthorizeCache>();
            //services.AddScoped<MenuCache>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAccountTypeService, AccountTypeService>();
            services.AddScoped<IAgentTypeService, AgentTypeService>();
            services.AddScoped<IAlertTypeService, AlertTypeService>();
            services.AddScoped<IAllNHFSubscriberService, AllNHFSubscriberService>();
            services.AddScoped<IApprovalLogService, ApprovalLogService>();
            services.AddScoped<IApprovalSetupService, ApprovalSetupService>();
            services.AddScoped<IApproveAgentsService, ApproveAgentsService>();
            services.AddScoped<IApproveEmployerAggregatorService, ApproveEmployerAggregatorService>();
            services.AddScoped<IAutoJobLogService, AutoJobLogService>();
            services.AddScoped<IAutoJobService, AutoJobService>();
            services.AddScoped<IBankService, BankService>();
            services.AddScoped<IBranchService, BranchService>();
            services.AddScoped<IChangeEmployerService, ChangeEmployerService>();
            services.AddScoped<IChangePasswordService, ChangePasswordService>();
            services.AddScoped<ICompanyClassService, CompanyClassService>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<ICompanyTypeService, CompanyTypeService>();
            services.AddScoped<IContributionFrequencyService, ContributionFrequencyService>();
            services.AddScoped<IContributionHistoryService, ContributionHistoryService>();
            services.AddScoped<IContributionService, ContributionService>();
            services.AddScoped<IContributionRefundPostingService, ContributionRefundPostingService>();
            services.AddScoped<ICustomerProfileUpdateService, CustomerProfileUpdateService>(); services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IDesignationService, DesignationService>();
            services.AddScoped<IDiasporaUserService, DiasporaUserService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IETicketService, ETicketService>();
            services.AddScoped<IFeedBackFormService, FeedBackFormService>();
            services.AddScoped<IFinanceCounterpartyTransactionService, FinanceCounterpartyTransactionService>();
            services.AddScoped<IFinanceTransactionService, FinanceTransactionService>();
            services.AddScoped<IGenderService, GenderService>();
            services.AddScoped<IInternetBankingUsersService, InternetBankingUsersService>();
            services.AddScoped<ILoanInitiationService, LoanInitiationService>();
            services.AddScoped<ILoanRepaymentService, LoanRepaymentService>();
            services.AddScoped<ILoanScheduleService, LoanScheduleService>();
            services.AddScoped<ILogLoginService, LogLoginService>();
            services.AddScoped<ILogOperateService, LogOperateSevice>();
            services.AddScoped<IMaritalStatusService, MaritalStatusService>();
            services.AddScoped<IMenuAuthorizeService, MenuAuthorizeService>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<INationalityService, NationalityService>();
            services.AddScoped<INextOfKinService, NextOfKinService>();
            services.AddScoped<INHFRegUsersService, NHFRegUsersService>();
            services.AddScoped<INHFCustomerRequestService, NHFCustomerRequestService>();
            services.AddTransient<IPaymentIntegrationService, PaymentIntegrationService>();
            services.AddScoped<IPaymentHistoryService, PaymentHistoryService>();
            services.AddScoped<IPropertySubscriptionService, PropertySubscriptionService>();
            services.AddScoped<IPropertyRegistrationService, PropertyRegistrationService>();
            services.AddScoped<IPropertyGalleryService, PropertyGalleryService>();
            services.AddScoped<IRefundService, RefundService>();
            services.AddScoped<IRefundConditionService, RefundConditionService>();
            services.AddScoped<IRefundProfilingService, RefundProfilingService>();
            services.AddScoped<IRemitaPaymentDetailsService, RemitaPaymentDetailsService>();
            services.AddScoped<IRelationService, RelationService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<ISectorService, SectorService>();
            services.AddScoped<IStateService, StateService>();
            services.AddScoped<ISubSectorService, SubSectorService>();
            services.AddScoped<ITitleService, TitleService>();
            services.AddScoped<IUnlockNhfPortalService, UnlockNhfPortalService>();
            services.AddScoped<IUserService, UserService>();
            //services.AddTransient<IEmailSender, EmailSender>();
            services.AddSingleton<RepositoryFactory>();

            return services;
        }

        //public static void AddConfigureCacheServices(this IServiceCollection services)
        //{
        //    services.Configure<CacheConfiguration>(GlobalContext.Configuration?.GetSection("CacheConfiguration"));
        //    services.AddMemoryCache();

        //    services.AddTransient<MemoryCacheService>();
        //    services.AddTransient<RedisCacheService>();
        //    services.AddTransient<Func<CacheTechniques, ICacheService>>(serviceProvider => key =>
        //    {
        //        switch (key)
        //        {
        //            case CacheTechniques.Memory:
        //                return serviceProvider.GetService<MemoryCacheService>();
        //            case CacheTechniques.Redis:
        //                return serviceProvider.GetService<RedisCacheService>();
        //            default:
        //                return serviceProvider.GetService<MemoryCacheService>();
        //        }
        //    });

        //    services.AddHangfireServer();
        //}

        //public static void AddConfigureHangfire(this IServiceCollection services)
        //{
        //    // Add Hangfire services.
        //    // services.AddHangfire(options => options.UseMemoryStorage());
        //    // services.AddHangfire(x => x.UseSqlServerStorage(configuration.GetConnectionString("HangfireSQLServer")));
        //    services.AddHangfire(config => config
        //        .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
        //        .UseSimpleAssemblyNameTypeSerializer()
        //        .UseRecommendedSerializerSettings()
        //        .UseSqlServerStorage(GlobalContext.Configuration.GetConnectionString("HangfireServer"),
        //        new SqlServerStorageOptions
        //        {
        //            CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
        //            SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
        //            QueuePollInterval = TimeSpan.Zero, // QueuePollInterval = TimeSpan.Zero OR QueuePollInterval = TimeSpan.FromSeconds(15)
        //            UseRecommendedIsolationLevel = true,
        //            DisableGlobalLocks = true
        //        }));
        //}

        private static void SetConfiguration()
        {
            var databaseProvider = SystemConfig.Database?.DBProvider;
            var connectionString = SystemConfig.Database?.DBConnectionString;

            Configuration.CreateInstance(databaseProvider, connectionString);

            SystemConfig.Instance.InstallLocation = Environment.CurrentDirectory;

            var googleCredPath = SystemConfig.Google?.CredentialPath;

            if (!string.IsNullOrWhiteSpace(googleCredPath))
            {
                SystemConfig.Instance.GoogleConfig = new GoogleConfig
                {
                    CredentialPath = googleCredPath,
                    DefaultAccountName = SystemConfig.Google?.DefaultAccountName
                };
            }
        }
    }
}