using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Base;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities
{
    // Charge Setup table
    [Table("tbl_ChargeSetup")]
    public class ChargeSetupEntity : BaseExtensionEntity
    {
        // Reference Number        
        [Column("ReferenceNumber"), Description("Reference Number")]
        public string? ReferenceNumber { get; set; }

        // Fee Catergory      
        [Column("FeeCatergory"), Description("Fee Catergory")]
        public string? FeeCatergory { get; set; }

        // Fee Catergory      
        [Column("FeeRate"), Description("Fee Rate%")]
        public string? FeeRate { get; set; }

        //Charge Name
        [Column("ChargeName"), Description("Charge Name")]
        public string? ChargeName { get; set; }

        //Rate
        [Column("Rate"), Description("Rate")]
        public decimal Rate { get; set; }

        //Property Type
        [Column("Property Type"), Description("Property Type")]
        public string? PropertyType { get; set; }

        //Amount
        [Column("Amount"), Description("Amount")]
        public string? Amount { get; set; }

    }
}