using System.ComponentModel.DataAnnotations.Schema;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Base;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities
{
    // Company Information table
    [Table("tbl_LoanRepaymentEntity")]
    public class LoanRepaymentEntity : BaseExtensionEntity
    {
        // Total Amount
        [Column("Totalamount")]
        public decimal Totalamount { get; set; }
        // Value Date
        [Column("Valuedate")]
        public DateTime Valuedate { get; set; }
        // Transaction Id
        [Column("Transactionid")]
        public string Transactionid { get; set; }
        // Narration
        [Column("Narration")]
        public string Narration { get; set; }
        // Repayment Date
        [Column("Repaymentdate")]
        public DateTime Repaymentdate { get; set; }
        // Month
        [Column("Month")]
        public string Month { get; set; }
        // 
        [Column("Year")]
        public string Year { get; set; }
        // Employer Name
        [Column("EmployerNumber")]
        public string EmployerNumber { get; set; }
        // Last Name
        [Column("Lastname")]
        public string Lastname { get; set; }
        // First Name
        [Column("Firstname")]
        public string Firstname { get; set; }
        // Middle Name
        [Column("MiddleName")]
        public string MiddleName { get; set; }
        // Amount
        [Column("Amount")]
        public decimal Amount { get; set; }
        // Employee Number
        [Column("EmployeeNumber")]
        public string EmployeeNumber { get; set; }
        // Payment Option
        [Column("Paymentoption")]
        public string PaymentOption { get; set; }

        // Payment Status
        [Column("PaymentStatus")]
        public string PaymentStatus { get; set; }
    }
}