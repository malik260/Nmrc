using Mortgage.Ecosystem.DataAccess.Layer.Helpers;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Base;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities
{
    [Table("tbl_AutoJob")]
    public class AutoJobEntity : BaseExtensionEntity
    {
        public string? JobGroupName { get; set; }
        public string? JobName { get; set; }
        public int? JobStatus { get; set; }
        public string? CronExpression { get; set; }
        [JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime? StartTime { get; set; }
        [JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime? EndTime { get; set; }
        [JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime? NextStartTime { get; set; }
        // Remark
        public string? Remark { get; set; }
    }
}
