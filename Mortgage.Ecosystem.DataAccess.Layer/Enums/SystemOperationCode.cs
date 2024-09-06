using System.ComponentModel;

namespace Mortgage.Ecosystem.DataAccess.Layer.Enums
{
    public enum SystemOperationCode
    {
        #region CONTRIBUTION       
        [Description("Single Contribution")]
        SingleContribution = 1,
        [Description("Batch Contribution (Document Upload)")]
        BatchContribution,
        [Description("Check RRR Status")]
        CheckPaymentStatus,
        [Description("Get Employee Details")]
        GetEmployeeDetails,

        [Description("Batch Contribution Document Download")]
        TemplateDownload,
        [Description("BackLog Contribution Document Download")]
        TemplateDownload2,
        #endregion

        #region CONTRIBUTION HISTORY         
        [Description("Get Contribution History")]
        GetContributionHistoryPageListJson = 51,
        #endregion
        #region Change Employer 
        [Description("View Company Name")]
        ViewCompanyName = 101,
        #endregion

        #region Charge Setup 
        [Description("Get Charge Setup")]
        GetChargeSetupPageListJson = 151,
        #endregion

        #region Checklist 
        [Description("Get Checklist")]
        GetChecklistTreeListJson = 201,
        #endregion

        #region Company 
        [Description("Get Current Company")]
        GetCurrentCompany = 251,
        [Description("Get Companies Awaiting Approval")]
        GetApprovalPageListJson,
        [Description("Approve Company Registration")]
        ApproveFormJson,
        [Description("Reject Company Registration")]
        RejectFormJson,
        [Description("Get Company Info")]
        GetCompanyInfo,
        [Description("New company Registration")]
        RegisterCompany,
        #endregion

        #region Credit Assessment Factor Index
        [Description("Get Factor Index")]
        GetFactorIndex = 301,
        #endregion

        #region Credit Assessment  Index Title
        [Description("Get Index Title")]
        GetIndexTitle = 351,
        #endregion

        #region Credit Assessment Factor
        [Description("Get Risks")]
        GetRisks = 401,
        #endregion

        #region Credit Score
        [Description("Get Credit Score")]
        GetCreditScorePageListJson = 451,
        #endregion

        #region Credit Type
        [Description("Get Credit Type")]
        GetCreditTypePageListJson = 501,
        #endregion



        #region Customer Profile Update
        [Description("Get Customer Profile Update")]
        GetCustomerProfileUpdatePageListJson = 551,
        [Description("Get Customer Awaiting Profile Update")]
        GetCustomerProfileAwaitingUpdatePageListJson ,
        [Description("Customer Profile Update")]
        UpdateCustomer,
        #endregion

        #region DEPARTMNENT
        [Description("Get Department")]
        GetDepartmentTreeListJson = 601,
        #endregion

        #region DEVELOPER
        [Description("Get Pmb")]
        GetPmbTreeListJson = 651,
        #endregion

        #region DIASPORA USER
        [Description("Get Diaspora User")]
        GetDiasporaUserPageListJson = 701,
        #endregion

        #region EMPLOYEE
        [Description("Get Employee")]
        GetEmployeeTreeListJson = 751,
        [Description("Get Employee Awaiting Approval")]
        GetApprovalEmployee,
        [Description("Approve Employee Registration")]
        ApproveEmployee,
        [Description("Reject Employee Registration")]
        RejectEmployee,
        [Description("Employee Registration")]
        RegisterEmployee,

        #endregion

        #region E-TICKET
        [Description("Get Eticket")]
        GetEticketPageListJson = 801,
        [Description("Approved Eticket")]
        ApproveETicketForm,
        #endregion

        #region FEEDBACK
        [Description("Get FeedBackForm")]
        GetFeedBackFormPageListJson = 851,
        #endregion

        #region INTERNET BANKING
        [Description("Get Internet Banking User")]
        GetInternetBankingUsersPageListJson = 901,
        #endregion

        #region Lender
        [Description("Get Lender")]
        GetLenderPageListJson = 911,
        #endregion

        #region LOAN INITIATION
        [Description("Get Customer Loan")]
        GetLoanInitiationPageListJson = 951,
        [Description("Perform Loan Affordability")]
        PerformLoanAffordability,
        [Description("Loan Initiation")]
        LoanInitiation,
        [Description("View Information")]
        ViewInformation,
        [Description("Upload Loan Documents")]
        LoanDocument,
        #endregion

        #region LOAN REPAYMENT
        [Description("Get Loan Repayment")]
        GetLoanRepaymentPageListJson = 1001,
        [Description("Single Loan Repayment")]
        SingleLoanRepayment,
        [Description("Batch Loan Repayment")]
        BatchLoanRepayment,
        #endregion

        #region LOAN SCHEDULE
        [Description("Get Loan Schedule")]
        GetLoanSchedulePageListJson = 1051,
        [Description("Get Customer Loan")]
        GetCustomerLoan,
        [Description("Get Customer Loan Schedule")]
        GetCustomerLoanSchedule,
        #endregion

        #region NHF Customer Request
        [Description("Get NHF Customer Request")]
        GetNHFCustomerRequestTreeListJson = 1101,
        #endregion

        #region Payment History
        [Description("Get Remita Payment History")]
        GetRemitaPageListJson = 1151,
        [Description("Get Etransact Payment History")]
        GetEtransactPageListJson,
        #endregion

        #region PMB
        [Description("Get Pmb Employee")]
        GetPmbEmployee = 1201,
        [Description("DisApprove PMB Registration")]
        DisApproveFormJson,
        [Description("Create New Pmb Employee")]
        SaveNewEmployee,
        #endregion

        #region Property Gallery
        [Description("Get Property Gallery")]
        GetPropertyGalleryPageListJsonn = 1251,
        #endregion

        #region Property Registration
        [Description("Get Images")]
        GetImagesJson = 1301,
        [Description("Get All Images")]
        GetAllImages,
        [Description("Get Property Registration")]
        GetPropertyRegistrationPageListJson,
        [Description("Register Property")]
        RegisterProperty,
        [Description("View Pmb Company Name")]
        ViewPmbCompanyName,
        #endregion

        #region Property Subscription
        [Description("Get Property Subscription")]
        GetPropertySubscriptionPageListJson = 1351,
        [Description("Subscribe Property")]
        SubscribeProperty,
        #endregion

        #region Refund
        [Description("Get Refund")]
        GetRefundPageListJson = 1401,
        [Description("View Customer Information")]
        ViewCustomerInformation,
        #endregion

        #region Scheme
        [Description("Get Scheme")]
        GetSchemePageListJson = 1411,
        #endregion

        #region Reset Password
        [Description("Generate Token")]
        GenerateTokenJson = 1451,
        [Description("Check Token")]
        CheckToken,
        #endregion

        #region Risk Assessment Setup
        [Description("Get Assessment Factors")]
        GetAssessmentFactorsPageListJson = 1501,
        #endregion

        #region Statement of Account
        [Description("Get Statement Of Account")]
        GetStatementOfAccountPageListJson = 1551,
        #endregion

        #region Underwriting
        [Description("Get Underwriting")]
        GetUnderwritingPageListJson = 1601,
        [Description("Get Batched Loans")]
        GetBatchedLoans,
        [Description("Get Loan For Review")]
        GetLoanForReview,
        [Description("Get Loan For Batching")]
        GetLoanForBatching,
        [Description("Get Batched Loan")]
        GetBatchedLoan,
        [Description("Get Loan For Underwriting")]
        GetLoanForUnderwriting,
        [Description("Batch Loan")]
        BatchLoan,
        [Description("UnBatch Loan")]
        UnBatchLoan,
        [Description("Apply Loan")]
        ApplyLoan,
        [Description("Approve Review")]
        ApproveReview,
        [Description("DisApprove Review")]
        DisApproveReview,
        [Description("Approve Underwriting")]
        DisApproveUnderwriting,
        [Description("Disapprove Underwriting")]
        ApproveUnderwriting,
        [Description("Perform Affordability")]
        PerformAffordability,
        #endregion

        #region Unlock Admin User
        [Description("Get Unlock Admin User")]
        GetUnlockAdminUserPageListJson = 1651,
        #endregion

        #region Unlock NHF Portal
        [Description("Get Unlock NHF Portal")]
        GetUnlockNhfPortalPageListJson = 1701,
        #endregion

        #region USER
        [Description("Get User Authorize")]
        GetUserAuthorizeJson = 1751,
        [Description("Change User")]
        ChangeUserJson,
        [Description("Import User")]
        ImportUserJson,
        [Description("Export User")]
        ExportUserJson,
        #endregion

        #region Account Type
        [Description("Get Account Type Name")]
        GetAccountTypeName = 1801,
        #endregion

        #region Agent Type
        [Description("Get Agent type Name")]
        GetAgentTypeName = 1851,
        #endregion

        #region Alert Type
        [Description("Get Alert type Name")]
        GetAlertTypeName = 1901,
        #endregion

        #region Appproval Log
        [Description("Get Approval Log Name")]
        GetApprovalLogName = 1951,
        #endregion

        #region Approval Setup
        [Description("Get Approval Setup Name")]
        GetApprovalSetupName = 2001,
        [Description("Assign Privilege")]
        AssignPrivilege,
        #endregion

        #region Bank
        [Description("Get Bank Name")]
        GetBankName = 2051,
        #endregion

        #region Branch
        [Description("Get Branch Name")]
        GetBranchName = 2101,
        #endregion

        #region Company Class
        [Description("Get Company Class Name")]
        GetCompanyClassName = 2151,
        #endregion

        #region Company Type
        [Description("Get Company Type Name")]
        GetCompanyTypeName = 2201,
        #endregion

        #region Contribution Frequency
        [Description("Get Contribution Frequency Name")]
        GetContributionFrequencyName = 2251,
        #endregion

        #region Designation
        [Description("Get Designation Name")]
        GetDesignationName = 2301,
        #endregion

        #region Gender
        [Description("Get Gender Name")]
        GetGenderName = 2351,
        #endregion

        //#region Log Login
        //[Description("Remove All Form Json")]
        //RemoveAllFormJson,
        //#endregion

        #region Marital Status
        [Description("Get Marital Status Name")]
        GetMaritalStatusName = 2401,
        #endregion

        #region Menu
        [Description("Get System Menus")]
        GetMenuTreeListJson = 2451,
        [Description("Get Menu2")]
        GetMenuTreeListJson2,
        #endregion

        #region Nationality
        [Description("Get Nationality Name")]
        GetNationalityName = 2501,
        #endregion

        #region Relation
        [Description("Get Relation Name")]
        GetRelationName = 2551,
        #endregion

        #region ROLE
        [Description("Get Role")]
        GetRoleTreeListJson = 2601,
        [Description("Get Role Name")]
        GetRoleName,
        #endregion

        #region SECTOR
        [Description("Get Sector Name")]
        GetSectorName = 2651,
        #endregion

        #region STATE
        [Description("Get State Name")]
        GetStateName = 2701,
        #endregion

        #region SUBSECTOR
        [Description("Get SubSector Name")]
        GetSubSectorName = 2751,
        #endregion

        #region TITLE
        [Description("Get Title Name")]
        GetTitleName = 2801,
        #endregion


        #region USER
        [Description("Change Password")]
        ChangePasswordJson = 90000000,
        [Description("Forgot Password")]
        ForgotPasswordJson,
        [Description("Export Employeee to Excel")]
        ExportEmployee,
        #endregion

        #region Base
        [Description("On Action Execution")]
        OnActionExecutionAsync = 2851,
        [Description("On Action Executed")]
        OnActionExecuted,
        #endregion

        #region File
        [Description("Upload File")]
        UploadFile = 2901,
        [Description("Delete File")]
        DeleteFile,
        [Description("Download File")]
        DownloadFile,
        #endregion


        #region HOME
        [Description("Login")]
        Login = 2951,
        [Description("Login Off")]
        LoginOffJson,
        [Description("Get Captcha Image")]
        GetCaptchaImage,
        [Description("Login Json")]
        LoginJson,
        #endregion

        #region ControllersRegion
        [Description("Contribution")]
        Contribution = 3001,
        [Description("CONTRIBUTION HISTORY")]
        ContributionHistory,
        [Description("Change Employer")]
        ChangeEmployer,
        [Description("Charge Setup")]
        ChargeSetup,
        [Description("Checklist")]
        Checklist,
        [Description("Company")]
        Company,
        [Description("Credit Assessment Factor Index")]
        CreditAssessmentFactorIndex,
        [Description("Credit Assessment Index Title")]
        CreditAssessmentIndexTitle,
        [Description("Credit Assessment Factor")]
        CreditAssessmentFactor,
        [Description("Credit Score")]
        CreditScore,
        [Description("Credit Type")]
        CreditType,
        [Description("Customer Profile Update")]
        CustomerProfileUpdate,
        [Description("DEPARTMNENT")]
        Department,
        [Description("DEVELOPER")]
        Developer,
        [Description("DIASPORA USER")]
        DiasporaUser,
        [Description("EMPLOYEE")]
        Employee,
        [Description("E-TICKET")]
        ETicket,
        [Description("FEEDBACK")]
        FeedBack,
        [Description("INTERNET BANKING")]
        InternetBanking,
        [Description("Lender")]
        Lender,
        [Description("LOAN INITIATION")]
        LoanInitiationController,
        [Description("LOAN REPAYMENT")]
        LoanRepayment,
        [Description("LOAN SCHEDULE")]
        LoanSchedule,
        [Description("NHF Customer Request")]
        NHFCustomerRequest,
        [Description("Payment History")]
        PaymentHistory,
        [Description("PMB")]
        PMB,
        [Description("Property Gallery")]
        PropertyGallery,
        [Description("Property Registration")]
        PropertyRegistration,
        [Description("Property Subscription")]
        PropertySubscription,
        [Description("Refund")]
        Refund,
        [Description("Reset Password")]
        ResetPassword,
        [Description("Risk Assessment Setup")]
        RiskAssessmentSetup,
        [Description("Statement of Account")]
        StatementOfAccount,
        [Description("Scheme")]
        Scheme,
        [Description("Underwriting")]
        Underwriting,
        [Description("Unlock Admin User")]
        UnlockAdminUser,
        [Description("Unlock NHF Portal")]
        UnlockNHFPortal,
        [Description("User")]
        User,
        [Description("Account Type")]
        AccountType,
        [Description("Agent Type")]
        AgentType,
        [Description("Alert Type")]
        AlertType,
        [Description("Appproval Log")]
        AppprovalLog,
        [Description("Approval Setup")]
        ApprovalSetup,
        [Description("Bank")]
        Bank,
        [Description("Branch")]
        Branch,
        [Description("Company Class")]
        CompanyClass,
        [Description("Company Type")]
        CompanyType,
        [Description("Contribution Frequency")]
        ContributionFrequency,
        [Description("Designation")]
        Designation,
        [Description("Gender")]
        Gender,
        [Description("Marital Status")]
        MaritalStatus,
        [Description("Menu")]
        Menu,
        [Description("Nationality")]
        Nationality,
        [Description("Relation")]
        Relation,
        [Description("ROLE")]
        Role,
        [Description("SECTOR")]
        Sector,
        [Description("STATE")]
        State,
        [Description("SUBSECTOR")]
        SubSector,
        [Description("TITLE")]
        Title,
        [Description("BASE")]
        Base,
        [Description("FILE")]
        File,
        [Description("HOME")]
        Home,
        #endregion
    }
}