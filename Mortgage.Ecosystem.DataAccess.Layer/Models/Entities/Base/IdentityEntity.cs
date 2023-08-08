using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Base
{
    // The identity class of database entities, all database entity attribute types are nullable value types, in order to make judgments when doing conditional queries
    // Although it is a nullable value type, the attribute value of null will be assigned a default value at the bottom layer according to the attribute type. The string is string?.empty, the value is 0, and the date is 1970-01-01
    public class IdentityEntity
    {
        // Primary key
        [Key, Column("Id")]
        public int Id { get; set; }

        // WebApi does not have Cookie and Session, so Token needs to be passed in to identify the user
        [NotMapped]
        public string? Token { get; set; }

        // Create
        public virtual void Create()
        {
        }
    }

    // Basic creation of entities
    public class IdentityCreateEntity : IdentityEntity
    {
        // Creation time
        [Description("Creation time")]
        public DateTime BaseCreateTime { get; set; }

        // Creator ID
        [Column("BaseCreatorId")]
        public long BaseCreatorId { get; set; }

        // Approving process (Menu Id)
        [Column("BaseProcessMenu")]
        public long BaseProcessMenu { get; set; }

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

            if (BaseProcessMenu == default)
            {
                var user = await Operator.Operator.Instance.Current(Token);
                BaseProcessMenu = user != null ? user.CurrentMenu : 0;
            }
        }
    }

    // Basic modification entity
    public class IdentityModifyEntity : IdentityCreateEntity
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
    public class IdentityExtensionEntity : IdentityModifyEntity
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
    public class IdentityField
    {
        // Basic field List
        public static string[] IdentityFieldList { get; } = new string[]
        {
            "Id",
            "BaseProcessMenu",
            "BaseIsDelete",
            "BaseCreateTime",
            "BaseModifyTime",
            "BaseCreatorId",
            "BaseModifierId",
            "BaseVersion",
        };
    }
}