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
        public decimal InterestRate { get; set; }
        [Required(ErrorMessage = " Principal Amount cannot be null")]
        public decimal PrincipalAmount { get; set; }
        public int Tenor { get; set; }

        [Required(ErrorMessage = "please select a product ")]
        public string? Product { get; set; }
        public string? repaymentDate { get; set; }
        public string? Purpose { get; set; }

    }
}
