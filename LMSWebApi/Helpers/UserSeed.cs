using Domain.Context;
using Microsoft.EntityFrameworkCore;
using Contracts.Entities;
using Contracts.Utils;
using Microsoft.AspNetCore.Identity;
using StakeHoldersWebApi.Models.IdentityModels;
using Contracts.Collections;
using Contracts.Entities.Enums;

namespace LMSWebApi.Helpers
{
    public class UserSeed
    {
       
        public static void SeedDatabase(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                scope.ServiceProvider.GetRequiredService<ApplicationDbContext>().Database.Migrate();
                CreateDefaultRolesAndPermissions(scope);
                CreateAdminAccount(scope);
            }

            Console.WriteLine("Done seeding database.");
        }

        public static void CreateAdminAccount(IServiceScope serviceScope)
        {

            var userMgr = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var admin = new ApplicationUser
            {
                UserName = CoreConstants.DefaultAccountName,
                Email = CoreConstants.DefaultAccount,
                EmailConfirmed = true,
                TenantId = 1,
                UserType = UserType.Employee,
                FirstName = CoreConstants.DefaultAccountName,
                LocationId = 1,
                CreationTime = DateTime.Now,
                IdentificationId = 1,
                WalletId = 1,
            };

            if (userMgr.FindByNameAsync(admin.UserName).Result is null)
            {
                var result = userMgr.CreateAsync(admin, "Adminx!2").Result;

                if (result.Succeeded)
                {
                    var adminRole = GetRole(serviceScope, CoreConstants.Roles.SuperAdmin);
                    if (adminRole != null)
                    {
                        var adminRoleResult = userMgr.AddToRoleAsync(admin, adminRole.Name).Result;
                    }
                }
            }
        }

        static ApplicationRole GetRole(IServiceScope serviceScope, string role)
        {
            var roleMgr = serviceScope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            return roleMgr.FindByNameAsync(role).Result;
        }
       
   
        public static void CreateDefaultRolesAndPermissions(IServiceScope serviceScope)
        {
            var roleMgr = serviceScope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            var systemRoles = PermissionClaimsProvider.GetSystemDefaultRoles();


            if (systemRoles is null || !systemRoles.Any())
                return;

            foreach (var systemClaims in systemRoles)
            {
                var role = roleMgr.FindByNameAsync(systemClaims.Key).Result;

                if (role is null)
                {
                    role = new ApplicationRole
                    {
                        Name = systemClaims.Key,
                        IsActive = true,
                        IsDefaultRole = true,
                        TenantId = 1,
                    };

                    var r = roleMgr.CreateAsync(role).Result;
                }

                var oldClaims = roleMgr.GetClaimsAsync(role).Result;

                foreach (var claim in systemClaims.Value)
                {
                    if (!oldClaims.Any(x => x.Value.Equals(claim.Value)))
                    {

                        var r = roleMgr.AddClaimAsync(role, claim).Result;
                    }
                }
            }
        }
    }
}
