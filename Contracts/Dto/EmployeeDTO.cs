using Contracts.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Dto
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        public string? EmployeeCode { get; set; }
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? FullName => FirstName + " " + LastName;
        public string? MiddleName { get; set; }
        public Gender Gender { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? EmployeePhoto { get; set; }
        public string? NextOfKin { get; set; }
        public string? NextOfKinPhone { get; set; }
        public int? WalletId { get; set; }
        public int? DepartmentId { get; set; }
        public string? DepartmentName { get; set; }

        public int? LocationId { get; set; }
        public string? locationName { get; set; }

        public DateTime? DateOfEmployment { get; set; }

        public int? UserId { get; set; }
        
        public bool IsActive { get; set; }
        public string? Desgination { get; set; }
        public string? ReportTo { get; set; }
        public int? TenantId { get; set; }

        public string? RoleName { get; set; }


    }
}
