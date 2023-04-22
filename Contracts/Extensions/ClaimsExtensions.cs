using IdentityModel;
using StakeHoldersWebApi.Models.IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Extensions
{
    public static class ClaimsExtensions
    {
        public static List<Claim> UserToClaims(this ApplicationUser user)
        {
            //These wont be null
            var claims = new List<Claim>
            {
                new Claim(JwtClaimTypes.Id, user.Id.ToString()),
                new Claim(JwtClaimTypes.Name, user.UserName),
                new Claim(JwtClaimTypes.Email, user.Email)
            };

            //these can.

            if (!string.IsNullOrWhiteSpace(user.FirstName))
            {
                claims.Add(new Claim(JwtClaimTypes.GivenName, user.FirstName));
            }

            if (!string.IsNullOrWhiteSpace(user.LastName))
            {
                claims.Add(new Claim(JwtClaimTypes.FamilyName, user.LastName));
            }
            if (!string.IsNullOrWhiteSpace(user.TenantId.ToString()))
            {
                claims.Add(new Claim(JwtClaimTypes.PreferredUserName, user.TenantId.ToString()));
            }
            return claims;
        }
    }
}
