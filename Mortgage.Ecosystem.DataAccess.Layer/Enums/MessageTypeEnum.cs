using System.ComponentModel;

namespace Mortgage.Ecosystem.DataAccess.Layer.Enums
{
    public enum MessageTypeEnum
    {
        [Description("Request")]
        Request = 1,

        [Description("Complaint")]
        Complaint = 2,

        [Description("Enquiry")]
        Enquiry = 3,


    }
}