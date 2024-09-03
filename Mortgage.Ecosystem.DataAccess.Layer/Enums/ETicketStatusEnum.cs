using System.ComponentModel;

public enum ETicketStatusEnum
{
    [Description("Pending")]
    Pending = 1,

    [Description("In-Progress")]
    InProgress = 2,

    [Description("Closed")]
    Closed = 3,

}

public enum AdminETicketStatusEnum
{
    [Description("In-Progress")]
    InProgress = 2,

    [Description("Closed")]
    Closed = 3,

}