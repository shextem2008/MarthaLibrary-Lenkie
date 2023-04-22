using Contracts.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Collections
{
    public class PermissionClaim : Claim
    {
        public PermissionClaim(string value) : base("Permission", value)
        {
        }
    }

    public class PermissionClaimsProvider
    {
        public static readonly PermissionClaim ManageEmployee = new PermissionClaim("manageemployee");
        public static readonly PermissionClaim ManageCustomer = new PermissionClaim("managecustomer");


        public static Dictionary<string, IEnumerable<PermissionClaim>> GetSystemDefaultRoles()
        {
            return new Dictionary<string, IEnumerable<PermissionClaim>>
            {
                    {    CoreConstants.Roles.SuperAdmin, new PermissionClaim []{
                                        ManageEmployee,
                                        ManageCustomer,
                         }
                    },
                    {    CoreConstants.Roles.Admin, new PermissionClaim []{
                         }
                    },
                    {    CoreConstants.Roles.Employee, new PermissionClaim []{
                         }
                    },
                    {    CoreConstants.Roles.Customer, new PermissionClaim []{
                         }
                    },
                    {    CoreConstants.Roles.Vendor, new PermissionClaim []{
                         }
                    },
                    {    CoreConstants.Roles.Bank, new PermissionClaim []{
                         }
                    }

            };
        }

        public static IEnumerable<PermissionClaim> GetClaims()
        {
            return new PermissionClaim[] {
                ManageEmployee,
                ManageCustomer ,

            };
        }
    }
}
