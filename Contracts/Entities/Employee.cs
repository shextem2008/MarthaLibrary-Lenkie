
using Contracts.Utils.Auditing;
using StakeHoldersWebApi.Models.IdentityModels;

namespace Contracts.Entities
{
    public class Employee : FullAuditedEntity
    {
        public string? EmployeeCode { get; set; }
        public DateTime? DateOfEmployment { get; set; }
        public int? DepartmentId { get; set; }
        public Department? Department { get; set; }
        public int? UserId { get; set; }
        public virtual ApplicationUser? User { get; set; }
        public bool IsActive { get; set; }
        public string? Desgination { get; set; }
        public string? ReportTo { get; set; }

    }
}
