using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Operator
{
    // Operator information
    public class OperatorInfo
    {
        public int Id { get; set; } // Id
        public long Company { get; set; } // Company
        public long Employee { get; set; } // Employee
        public int LoginCount { get; set; } // Login counter
        public int UserStatus { get; set; } // User Status
        public int IsOnline { get; set; } // Is Online
        public string? UserName { get; set; } // User Name
        public string? RealName { get; set; } // Real Name
        public string? WebToken { get; set; } // WebToken
        public string? ApiToken { get; set; } // ApiToken
        public int IsSystem { get; set; } // Whether the system user
        public string? Password { get; set; }
        public string? Salt { get; set; }

        [NotMapped]
        public CompanyListParam? CompanyInfo { get; set; } // Company Information

        [NotMapped]
        public EmployeeListParam? EmployeeInfo { get; set; } // Employee Information

        [NotMapped]
        public BranchListParam? BranchInfo { get; set; } // Branch Information

        [NotMapped]
        public DepartmentListParam? DepartmentInfo { get; set; } // Department Information

        [NotMapped]
        public string? RoleIds { get; set; } // Role Ids

        [NotMapped]
        public long CurrentMenu { get; set; } // Current Menu ID

        [NotMapped]
        public List<EmployeeEntity>? ApprovalEmployeeItems { get; set; } // Approval Employee Items

        [NotMapped]
        public List<CompanyEntity>? ApprovalEmployerItems { get; set; } // Approval Employer Items

        [NotMapped]
        public int ApprovalItemCount { get; set; } // Approval Items Count
    }

    // Character information
    public class RoleInfo
    {
        public long RoleId { get; set; } // Character ID
    }
}