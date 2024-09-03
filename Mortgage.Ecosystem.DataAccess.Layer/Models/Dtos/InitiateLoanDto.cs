using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos
{
    public class InitiateLoanDto
    {
        public decimal InterestRate { get; set; }
        public decimal PrincipalAmount { get; set; }
        public int Tenor { get; set; }
        public string? Product { get; set; }
        public string? repaymentDate { get; set; }
        public string? Purpose { get; set; }
        public string NhfNumber { get; set; }
        public string? PMB { get; set; }
        public string? DocumentTitle { get; set; }
        public string? LoanProduct { get; set; }
        public string? RepaymentPattern { get; set; }
        public string? MonthlyIncome { get; set; }

        [NotMapped]
        [Column("file")]
        public List<IFormFile>? file { get; set; }

    }
}