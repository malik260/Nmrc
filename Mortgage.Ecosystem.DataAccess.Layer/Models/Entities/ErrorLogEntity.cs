using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities
{
    [Table("ErrorLogs")]
    public class ErrorLogEntity
    {
        [Key]
        [Column("Id"), Description("Id")]
        public int Id { get; set; }

        [Column("Level"), Description("Level")]
        public string Level { get; set; }

        [Column("Message"), Description("Message")]
        public string Message { get; set; }

        [Column("StackTrace"), Description("StackTrace")]
        public string StackTrace { get; set; }

        [Column("Callsite"), Description("Callsite")]
        public string Callsite { get; set; }

        [Column("Username"), Description("Username")]
        public string Username { get; set; }

        [Column("LoggedOnDate"), Description("LoggedOnDate")]
        public DateTime LoggedOnDate { get; set; }

        [Column("IpAddress"), Description("IpAddress")]
        public string IpAddress { get; set; }
        [Column("Device"), Description("Device")]
        public string Device { get; set; }
        [Column("InnerException"), Description("InnerException")]
        public string InnerException { get; set; }

        [Column("AdditionalInfo"), Description("AdditionalInfo")]
        public string AdditionalInfo { get; set; }

        [Column("OriginatingProcess"), Description("OriginatingProcess")]
        public string OriginatingProcess { get; set; }

        [Column("Type"), Description("Type")]
        public string Type { get; set; }
        
        [Column("ErrorCode"), Description("ErrorCode")]
        public string ErrorCode { get; set; }
    }
}
