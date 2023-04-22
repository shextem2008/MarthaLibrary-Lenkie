using Microsoft.AspNetCore.Identity;

namespace StakeHoldersWebApi.Models.IdentityModels
{
    public class ApplicationRole : IdentityRole<int>
    {

        public bool IsActive { get; set; }
        public DateTime? CreationTime { get; set; }
        public bool IsDefaultRole { get; set; }
        public string? RolesDescription { get; set; }
        public int? TenantId { get; set; }
        public ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}
