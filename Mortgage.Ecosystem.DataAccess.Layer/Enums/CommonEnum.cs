using System.ComponentModel;

namespace Mortgage.Ecosystem.DataAccess.Layer.Enums
{
    public enum StatusEnum
    {
        [Description("Enable")]
        Yes = 1,

        [Description("Disable")]
        No = 0
    }

    public enum IsEnum
    {
        [Description("Yes")]
        Yes = 1,

        [Description("No")]
        No = 0
    }

    public enum NeedEnum
    {
        [Description("Not required")]
        NotNeed = 0,

        [Description("Required")]
        Need = 1
    }

    public enum OperateStatusEnum
    {
        [Description("Failure")]
        Fail = 0,

        [Description("Success")]
        Success = 1
    }

    public enum UploadFileType
    {
        [Description("Avatar")]
        Portrait = 1,

        [Description("Imported file")]
        Import = 10
    }

    public enum PlatformEnum
    {
        [Description("Web background")]
        Web = 1,

        [Description("WebApi")]
        WebApi = 2
    }

    public enum PayStatusEnum
    {
        [Description("Unknown")]
        Unknown = 0,

        [Description("Paid")]
        Success = 1,

        [Description("Transfer refund")]
        Refund = 2,

        [Description("Unpaid")]
        NotPay = 3,

        [Description("Closed")]
        Closed = 4,

        [Description("Revoked (payment with payment code)")]
        Revoked = 5,

        [Description("User is paying (payment with payment code)")]
        UserPaying = 6,

        [Description("Payment failed (other reasons, such as bank return failure)")]
        PayError = 7
    }

    public enum RoleEnum
    {
        [Description("User")]
        User = 1,

        [Description("Restricted User")]
        Restricted = 2,

        [Description("Admin")]
        Admin = 3,

        [Description("Super User")]
        Super = 4,

        [Description("General")]
        General = 5
    }

  

    public enum ApprovalEnum
    {
        [Description("Pending")]
        Pending = 0,

        [Description("Approved")]
        Approved = 1,

        [Description("Rejected")]
        Rejected = 2,

        [Description("Cancelled")]
        Cancelled = 3
    }

    public enum UserTypeEnum
    {
        [Description("Employee")]
        Employee = 1,

        [Description("Diaspora")]
        Diaspora = 2
    }

    public enum ConditionForApplicationEnum
    {
        [Description("Age")]
        Age = 1,

        [Description("Incapacitation")]
        Incapacitation = 2,

        [Description("Death")]

        Death = 3,
    }

    public enum FeeCategoryEnum
    {
        [Description("Flat")]
        Flat = 1,

        [Description("Rate")]
        Rate = 2,

        [Description("No Charges")]

        NoCharges = 3,
    }
}