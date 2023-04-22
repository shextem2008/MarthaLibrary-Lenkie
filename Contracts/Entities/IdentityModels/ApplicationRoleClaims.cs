using Microsoft.AspNetCore.Identity;

namespace StakeHoldersWebApi.Models.IdentityModels
{
    public class ApplicationRoleClaims : IdentityRoleClaim<int>
    {
        public virtual ApplicationRole Role { get; set; }
    }
}
