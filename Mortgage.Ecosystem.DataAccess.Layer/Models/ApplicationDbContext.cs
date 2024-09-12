using Microsoft.EntityFrameworkCore;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Repositories.Base;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models
{
    public partial class ApplicationDbContext : DbCommon
    {
        // Configure the database to use
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        // Configure the model discovered by convention
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Initialize the database here
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<AccountTypeEntity>? AccountTypeEntity { get; set; }
        public DbSet<AccreditationFeeEntity>? AccredidationFeeEntity { get; set; }
        public DbSet<AgentTypeEntity>? AgentTypeEntity { get; set; }
        public DbSet<AlertTypeEntity>? AlertTypeEntity { get; set; }
        public DbSet<AllNHFSubscriberEntity>? AllNHFSubscriberEntity { get; set; }
        public DbSet<ApprovalLogEntity>? ApprovalLogEntity { get; set; }
        public DbSet<ApprovalSetupEntity>? ApprovalSetupEntity { get; set; }
        public DbSet<ApproveAgentsEntity>? ApproveAgentsEntity { get; set; }
        public DbSet<AddDocumentProcedureEntity>? AddDocumentProcedureEntity { get; set; }

        public DbSet<ApproveEmployerAggregatorEntity>? ApproveEmployerAggregatorEntity { get; set; }
        public DbSet<AuditTrailEntity>? AuditTrails { get; set; } // Audit Trailtable

        public DbSet<AutoJobEntity>? AutoJobEntity { get; set; } // Scheduled task table        
        public DbSet<AutoJobLogEntity>? AutoJobLogEntity { get; set; } // Scheduled task group table
        public DbSet<BankEntity>? BankEntity { get; set; }
        public DbSet<BranchEntity>? BranchEntity { get; set; }
        public DbSet<BrokerEntity>? BrokerEntity { get; set; }
        public DbSet<ChangeEmployerEntity>? ChangeEmployerEntity { get; set; }
        public DbSet<ChangePasswordEntity>? ChangePasswordEntity { get; set; }
        public DbSet<ChargeSetupEntity>? ChargeSetupEntity { get; set; }
        public DbSet<ChecklistEntity>? ChecklistEntity { get; set; }
        public DbSet<ChecklistProcedureEntity>? ChecklistProcedureEntity { get; set; }

        public DbSet<ContributionEntity>? ContributionEntity { get; set; }
        public DbSet<ContributionHistoryEntity>? ContributionHistoryEntity { get; set; }
        public DbSet<ContributionRefundPostingEntity>? ContributionRefundPostingEntity { get; set; }
        public DbSet<CompanyClassEntity>? CompanyClassEntity { get; set; }
        public DbSet<CompanyEntity>? CompanyEntity { get; set; }
        public DbSet<CompanyTypeEntity>? CompanyTypeEntity { get; set; }
        public DbSet<ContributionFrequencyEntity>? ContributionFrequencyEntity { get; set; }
        public DbSet<CreditAssessmentIndexEntity>? CreditAssessmentIndex { get; set; }
        public DbSet<CreditAssessmentRiskFactorEntity>? CreditAssessmentRiskFactor { get; set; }

        public DbSet<CreditAssessmentFactorIndexEntity>? CreditAssessmentFactorIndex { get; set; }

        public DbSet<CreditAssessmentIndexTitleEntity>? CreditAssessmentIndexTitle { get; set; }

        public DbSet<CreditScoreEntity>? CreditScoreEntity { get; set; }

        public DbSet<CreditTypeEntity>? CreditTypeEntity { get; set; }
        public DbSet<CustomerProfileUpdateEntity>? CustomerProfileUpdateEntity { get; set; }
        public DbSet<DisbursementEntity>? DisbursementEntity { get; set; }
        public DbSet<DepartmentEntity>? DepartmentEntity { get; set; } // Department table
        public DbSet<DesignationEntity>? DesignationEntity { get; set; }
        public DbSet<DiasporaUserEntity>? DiasporaUserEntity { get; set; }
        public DbSet<DeveloperEntity>? DeveloperEntity { get; set; }

        public DbSet<EmployeeEntity>? EmployeeEntity { get; set; }
        public DbSet<ETicketEntity>? ETicketEntity { get; set; }
        public DbSet<ErrorLogEntity>? ErrorLogEntity { get; set; }

        public DbSet<FinanceCounterpartyTransactionEntity>? FinanceCounterpartyTransactionEntity { get; set; }
        public DbSet<FeedBackFormEntity>? FeedBackFormEntity { get; set; }
        public DbSet<FinanceTransactionEntity>? FinanceTransactionEntity { get; set; }
        public DbSet<GenderEntity>? GenderEntity { get; set; }
        public DbSet<InternetBankingUsersEntity>? InternetBankingUsersEntity { get; set; }
        public DbSet<LenderTypeEntity>? LenderTypeEntity { get; set; }
        public DbSet<LenderSetupEntity>? LenderSetupEntity { get; set; } // Lender Setup table     
        public DbSet<LogLoginEntity>? LogLoginEntity { get; set; } // Login log table        
        public DbSet<LogOperateEntity>? LogOperateEntity { get; set; } // Operation log table
        public DbSet<LoanReviewEntity>? LoanReviewEntity { get; set; }
        public DbSet<LoanRepaymentEntity>? LoanRepaymentEntity { get; set; }
        public DbSet<LoanInitiationEntity>? LoanInitiationEntity { get; set; }
        public DbSet<LoanInitiationUploadEntity>? loanInitiationUploadEntity { get; set; }
        public DbSet<LoanScheduleEntity>? LoanScheduleEntity { get; set; }
        public DbSet<NHFCustomerRequestEntity>? NHFCustomerRequestEntity { get; set; }
        public DbSet<NHFRegCompanyEntity>? NHFRegCompanyEntity { get; set; }

        public DbSet<NHFRegUsersEntity>? NHFRegUsersEntity { get; set; }
        public DbSet<NmrcCategoryEntity>? NmrcCategoryEntity { get; set; }
        public DbSet<NmrcEligibilityEntity>? NmrcEligibilityEntity { get; set; }
        public DbSet<MaritalStatusEntity>? MaritalStatusEntity { get; set; }
        public DbSet<MenuAuthorizeEntity>? MenuAuthorizeEntity { get; set; } // Menu permission table        
        public DbSet<MenuEntity>? MenuEntity { get; set; } // Menu table
        public DbSet<NationalityEntity>? NationalityEntity { get; set; }
        public DbSet<NextOfKinEntity>? NextOfKinEntity { get; set; }
        public DbSet<PaymentHistoryEntity>? PaymentHistoryEntity { get; set; }
        public DbSet<PmbEntity>? PmbEntity { get; set; }
        public DbSet<PropertyRegistrationEntity>? PropertyRegistrationEntity { get; set; } // Property Registration table       
        public DbSet<PropertySubscriptionEntity>? PropertySubscriptionEntity { get; set; } // Property Subscription table     
        public DbSet<PropertyGalleryEntity>? PropertyGalleryEntity { get; set; } // Property Gallery table
        public DbSet<PropertyUploadEntity>? PropertyUploadEntity { get; set; } // Property Gallery table
        public DbSet<RefinancingEntity>? RefinancingEntity { get; set; }
        public DbSet<RelationEntity>? RelationEntity { get; set; }
        public DbSet<RemitaPaymentDetailsEntity>? RemitaPaymentDetailsEntity { get; set; }
        public DbSet<RefundConditionEntity>? RefundConditionEntity { get; set; }
        public DbSet<RefundEntity>? RefundEntity { get; set; }
        public DbSet<RefundProfilingEntity>? RefundProfilingEntity { get; set; }
        public DbSet<RiskAssessmentSetupEntity>? RiskAssessmentSetupEntity { get; set; }

        public DbSet<RiskAssessmentProcedureEntity>? RiskAssessmentProcedureEntity { get; set; }

        public DbSet<RoleEntity>? RoleEntity { get; set; } // Role table

        public DbSet<SchemeSetupEntity>? SchemeSetupEntity { get; set; } // Scheme table     
        public DbSet<SchemeLenderEntity>? SchemeLenderEntity { get; set; } // Scheme Lender table     
        public DbSet<SectorEntity>? SectorEntity { get; set; }
        public DbSet<SecondaryLenderEntity>? SecondaryLenderEntity { get; set; } // Secondary Lender table
        public DbSet<SecondaryLenderChecklistEntity>? SecondaryLenderChecklistEntity { get; set; } // Secondary Lender CheckList table
        public DbSet<StateEntity>? StateEntity { get; set; }
        public DbSet<StatementOfAccountEntity>? StatementOfAccountEntity { get; set; }
        public DbSet<SubSectorEntity>? SubSectorEntity { get; set; }
        public DbSet<TitleEntity>? TitleEntity { get; set; }
        public DbSet<UnderwritingEntity>? UnderwritingEntity { get; set; }

        public DbSet<UnlockAdminUserEntity>? UnlockAdminUserEntity { get; set; }
        public DbSet<UnlockNhfPortalEntity>? UnlockNhfPortalEntity { get; set; }
        public DbSet<UserBelongEntity>? UserBelongEntity { get; set; } // User role table        
        public DbSet<UserEntity>? UserEntity { get; set; } // User table
        public DbSet<ResetPasswordTokenEntity>? ResetPasswordTokenEntity { get; set; }
        public DbSet<LoanDisbursementEntity>? LoanDisbursement { get; set; }


    }
}