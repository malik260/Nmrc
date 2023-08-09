using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos
{
    public class InitiateLoanDto
    {
        public decimal? InterestAmount { get; set; }
        public decimal InterestRate { get; set; }
        public decimal MaturityAmount { get; set; }
        public string narration { get; set; }
        [Required(ErrorMessage = " Principal Amount cannot be null")]
        public decimal? PrincipalAmount { get; set; }
        public int? Tenor { get; set; }
        public string TypeOfLoan { get; set; }
        public string PaymentType { get; set; }
        [Required(ErrorMessage = "please select a product ")]
        public int? Product { get; set; }
        public string repaymentDate { get; set; }
        [Required(ErrorMessage = "please select a sector")]
        public string subsectorId { get; set; }
        [Required(ErrorMessage = "please enter loan purpose")]
        public string Purpose { get; set; }

    }
}
