using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.IDGenerator;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Base
{
    // The base class of database entities, all database entity attribute types are nullable value types, in order to make judgments when doing conditional queries
    // Although it is a nullable value type, the attribute value of null will be assigned a default value at the bottom layer according to the attribute type. The string is string?.empty, the value is 0, and the date is 1970-01-01
    public class BaseEntity
    {
        // Primary key for all tables
        // When long returns to the front-end js, the precision will be lost, so it is converted into a string
        [Key, Column("Id"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        // WebApi does not have Cookie and Session, so Token needs to be passed in to identify the user
        [NotMapped]
        public string? Token { get; set; }

        // Create
        public virtual void Create()
        {
            Id = IDGeneratorHelper.Instance.GetId();
        }
    }

    // Basic creation of entities
    public class BaseCreateEntity : BaseEntity
    {
        // Creation time
        [Description("Creation time")]
        public DateTime BaseCreateTime { get; set; }

        // Creator ID
        [Column("BaseCreatorId")]
        public long BaseCreatorId { get; set; }

        // Create
        public new async Task Create()
        {
            base.Create();

            if (BaseCreateTime == default)
            {
                BaseCreateTime = DateTime.Now;
            }

            if (BaseCreatorId == default)
            {
                var user = await Operator.Operator.Instance.Current(Token);
                BaseCreatorId = user != null ? user.Employee : 0;
            }
        }
    }

    // Basic modification entity
    public class BaseModifyEntity : BaseCreateEntity
    {
        // Data update version, control concurrency
        [Column("BaseVersion")]
        public int BaseVersion { get; set; }

        // Change the time
        [Column("BaseModifyTime"), Description("Modify Time")]
        public DateTime BaseModifyTime { get; set; }

        // Modify the person ID
        [Column("BaseModifierId")]
        public long BaseModifierId { get; set; }

        // Adjustment
        public async Task Modify()
        {
            BaseVersion = 0;
            BaseModifyTime = DateTime.Now;

            if (BaseModifierId == default)
            {
                var user = await Operator.Operator.Instance.Current();
                BaseModifierId = user != null ? user.Employee : 0;
            }
        }
    }

    // Basic extension entity
    public class BaseExtensionEntity : BaseModifyEntity
    {
        // Whether to delete 1 yes, 0 no
        [Column("BaseIsDelete"), JsonIgnore]
        public int BaseIsDelete { get; set; }

        // Create
        public new async Task Create()
        {
            BaseIsDelete = 0;

            await base.Create();

            await base.Modify();
        }

        // Adjustment
        public new async Task Modify()
        {
            await base.Modify();
        }
    }

    // Basic fields
    public class BaseField
    {
        // Basic field List
        public static string[] BaseFieldList { get; } = new string[]
        {
            "Id",
            "BaseIsDelete",
            "BaseCreateTime",
            "BaseModifyTime",
            "BaseCreatorId",
            "BaseModifierId",
            "BaseVersion",
        };
    }
}