using Microsoft.EntityFrameworkCore;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Repositories.Base;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models
{
    public partial class ApplicationDbContext : DbCommon
    {
        // Configure the database to use
        // <param name="optionsBuilder">Context Options Builder</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        // Configure the model discovered by convention
        // <param name="modelBuilder">Model Builder</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Initialize the database here
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<AccountTypeEntity>? AccountTypeEntity { get; set; }

        public DbSet<AgentTypeEntity>? AgentTypeEntity { get; set; }

        public DbSet<AlertTypeEntity>? AlertTypeEntity { get; set; }

        public DbSet<AllNHFSubscriberEntity>? AllNHFSubscriberEntity { get; set; }

        public DbSet<ApproveAgentsEntity>? ApproveAgentsEntity { get; set; }

        public DbSet<ApproveEmployerAggregatorEntity>? ApproveEmployerAggregatorEntity { get; set; }
        // Scheduled task table
        public DbSet<AutoJobEntity>? AutoJobEntity { get; set; }

        // Scheduled task group table
        public DbSet<AutoJobLogEntity>? AutoJobLogEntity { get; set; }

        public DbSet<BankEntity>? BankEntity { get; set; }

        public DbSet<BranchEntity>? BranchEntity { get; set; }

        public DbSet<ChangeEmployerEntity>? ChangeEmployerEntity { get; set; }

        public DbSet<ChangePasswordEntity>? ChangePasswordEntity { get; set; }

        public DbSet<ContributionEntity>? ContributionEntity { get; set; }

        public DbSet<ContributionHistoryEntity>? ContributionHistoryEntity { get; set; }

        public DbSet<ContributionRefundPostingEntity>? ContributionRefundPostingEntity { get; set; }

        public DbSet<CompanyClassEntity>? CompanyClassEntity { get; set; }

        public DbSet<CompanyEntity>? CompanyEntity { get; set; }

        public DbSet<CompanyTypeEntity>? CompanyTypeEntity { get; set; }

        public DbSet<ContributionFrequencyEntity>? ContributionFrequencyEntity { get; set; }

        // Department table
        public DbSet<DepartmentEntity>? DepartmentEntity { get; set; }

        public DbSet<DesignationEntity>? DesignationEntity { get; set; }

        public DbSet<DiasporaUserEntity>? DiasporaUserEntity { get; set; }

        public DbSet<EmployeeEntity>? EmployeeEntity { get; set; }

        public DbSet<ETicketEntity>? ETicketEntity { get; set; }

        public DbSet<FinanceCounterpartyTransactionEntity>? FinanceCounterpartyTransactionEntity { get; set; }

        public DbSet<FeedBackFormEntity>? FeedBackFormEntity { get; set; }

        public DbSet<FinanceTransactionEntity>? FinanceTransactionEntity { get; set; }
        public DbSet<GenderEntity>? GenderEntity { get; set; }

        public DbSet<InternetBankingUsersEntity>? InternetBankingUsersEntity { get; set; }

        // Login log table
        public DbSet<LogLoginEntity>? LogLoginEntity { get; set; }

        // Operation log table
        public DbSet<LogOperateEntity>? LogOperateEntity { get; set; }

        public DbSet<LoanRepaymentEntity>? LoanRepaymentEntity { get; set; }

        public DbSet<LoanInitiationEntity>? LoanInitiationEntity { get; set; }

        public DbSet<LoanScheduleEntity>? LoanScheduleEntity { get; set; }

        public DbSet<NHFCustomerRequestEntity>? NHFCustomerRequestEntity { get; set; }

        public DbSet<NHFRegUsersEntity>? NHFRegUsersEntity { get; set; }

        public DbSet<MaritalStatusEntity>? MaritalStatusEntity { get; set; }

        // Menu permission table
        public DbSet<MenuAuthorizeEntity>? MenuAuthorizeEntity { get; set; }

        // menu table
        public DbSet<MenuEntity>? MenuEntity { get; set; }

        public DbSet<NationalityEntity>? NationalityEntity { get; set; }

        public DbSet<NextOfKinEntity>? NextOfKinEntity { get; set; }

        public DbSet<PaymentHistoryEntity>? PaymentHistoryEntity { get; set; }

        // Property Registration table
        public DbSet<PropertyRegistrationEntity>? PropertyRegistrationEntity { get; set; }

        // Property Subscription table
        public DbSet<PropertySubscriptionEntity>? PropertySubscriptionEntity { get; set; }

        // Property Gallery table
        public DbSet<PropertyGalleryEntity>? PropertyGalleryEntity { get; set; }

        public DbSet<RelationEntity>? RelationEntity { get; set; }

        public DbSet<RemitaPaymentDetailsEntity>? RemitaPaymentDetailsEntity { get; set; }
        public DbSet<RefundConditionEntity>? RefundConditionEntity { get; set; }
        public DbSet<RefundEntity>? RefundEntity { get; set; }
        public DbSet<RefundProfilingEntity>? RefundProfilingEntity { get; set; }

        // role table
        public DbSet<RoleEntity>? RoleEntity { get; set; }

        public DbSet<SectorEntity>? SectorEntity { get; set; }

        public DbSet<StateEntity>? StateEntity { get; set; }

        public DbSet<SubSectorEntity>? SubSectorEntity { get; set; }

        public DbSet<TitleEntity>? TitleEntity { get; set; }

        public DbSet<UnlockNhfPortalEntity>? UnlockNhfPortalEntity { get; set; }

        // User's table
        public DbSet<UserBelongEntity>? UserBelongEntity { get; set; }

        // User table
        public DbSet<UserEntity>? UserEntity { get; set; }
    }
}