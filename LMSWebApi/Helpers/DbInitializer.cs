using Contracts.Entities;
using Contracts.Entities.Enums;
using Contracts.Utils;
using Domain.Context;
using Microsoft.EntityFrameworkCore;


namespace LMSWebApi.Helpers
{
    internal class DbInitializer
    {
         internal static void Initialize(ApplicationDbContext dbContext, string vtype)
         {
            ArgumentNullException.ThrowIfNull(dbContext, nameof(dbContext));
            dbContext.Database.EnsureCreated();

            CreatelocationCityStateRegion(dbContext);
            CreateDefaultTenantNDepartmentAccounts(dbContext);
            CreateDefaultIdentityNImageByPass(dbContext);
            CreateDefaultEmployeeAfterSeeding(dbContext,vtype);
            //CreateDefaultAppMenuItem(dbContext);
        }

        static void CreateDefaultTenantNDepartmentAccounts(ApplicationDbContext dbContext)
        {
            #region Tenant
            if (dbContext.Tenant.Any()) return;

            var tenants = new Tenant[]
            {
            new Tenant
            {
                TenantNameA = Contracts.Utils.CoreConstants.TenantName,
                Email = Contracts.Utils.CoreConstants.TenantEmail,
                IsActive = true,
                CreationTime = DateTime.Now,
                LastModificationTime = DateTime.Now,
                IsDeleted = false
            }
            //add other tenant
            };

            foreach (var user in tenants)
                dbContext.Tenant.Add(user);

            dbContext.SaveChanges();
            #endregion

            #region department
            if (dbContext.Department.Any()) return;
            var departs = new Department[]
            {
                new Department
                {
                    Name = Contracts.Utils.CoreConstants.DepartmentName,
                    TenantId = 1,
                    Description = Contracts.Utils.CoreConstants.DepartmentName
                }      
            };

            foreach (var item in departs)
                dbContext.Department.Add(item);

            dbContext.SaveChanges();
            #endregion
        }

        static void CreatelocationCityStateRegion(ApplicationDbContext dbContext)
        {

            #region Region
            if (dbContext.Region.Any()) return;
            var regions = new Region[]
            {
                new Region{ Name = "South-West",  TenantId = 1 },
                new Region{ Name = "South-East",  TenantId = 1 },
                 new Region{ Name = "South-South",  TenantId = 1 },
                  new Region{ Name = "North-East",  TenantId = 1 },
                   new Region{ Name = "North-West",  TenantId = 1 },
                    new Region{ Name = "North-Central",  TenantId = 1 }
                //add other tenant
            };

            foreach (var model in regions)
                dbContext.Region.Add(model);

            dbContext.SaveChanges();
            #endregion

            #region State
            if (dbContext.State.Any()) return;
           
            var states = new State[]
            {
                new State{ Name = "lagos",  TenantId = 1 ,RegionId = 1},
                    //add other tenant
            };

            foreach (var model in states)
                dbContext.State.Add(model);

            dbContext.SaveChanges();
            #endregion

            #region city
            if (dbContext.City.Any()) return;
          
            var citys = new City[]
            {
                    new City{ Name = "Lagos-Mainland",  TenantId = 1,StateId=1,Latitude = 0,Longitude = 0 },
                    //add other tenant
            };

            foreach (var model in citys)
                dbContext.City.Add(model);

            dbContext.SaveChanges();
            #endregion

            #region location
            if (dbContext.Location.Any()) return;
            var locations = new Location[]
            {
               new Location{ Name = "Oshodi-Isolo",  TenantId = 1 ,CityId=1,Latitude = 0,Longitude = 0 },
                    //add other tenant
            };

            foreach (var model in locations)
                dbContext.Location.Add(model);

            dbContext.SaveChanges();
            #endregion

         
        }

        static void CreateDefaultIdentityNImageByPass(ApplicationDbContext dbContext)
        {


            #region Identity
            if (dbContext.Identification.Any()) return;
            var models = new Identification[]
            {
                new Identification{ IdentificationCode = "111111111",   IdentificationType = IdentificationType.Default },
            };

            foreach (var item in models)
                dbContext.Identification.Add(item);

            dbContext.SaveChanges();
            #endregion

            #region Image

            if (dbContext.ImageFile.Any()) return;

            var modelImgs = new ImageFile[]
            {
                new ImageFile{  FileName = "ByPassImage",   Code = "DFTOOO1", UploadType = UploadType.Image },
            };

            foreach (var item in modelImgs)
                dbContext.ImageFile.Add(item);

            dbContext.SaveChanges();
            #endregion


            #region Wallet

            if (dbContext.Wallet.Any()) return;
            // insert for wallet
            var walletNumber = CommonHelper.RandomNumber(9);
            var wallet = new Wallet
            {
                WalletNumber = walletNumber, Balance = 0.00M, TenantId = 1,
            };
            dbContext.Wallet.Add(wallet);
            dbContext.SaveChanges();

            #endregion


        }


        static void CreateDefaultEmployeeAfterSeeding(ApplicationDbContext dbContext,string vtype)
        {
            #region Employee
            if (vtype != "vafterseed") return;
            if (dbContext.Employee.Any()) return;

            var employee = new Employee[]
            {
            new Employee
            {
                EmployeeCode = CommonHelper.GenereateRandonAlphaNumeric(),
                DepartmentId = 1,
                UserId=1,
                IsActive = true,
                DateOfEmployment = DateTime.Now
            }

            };

            foreach (var user in employee)
                dbContext.Employee.Add(user);

            dbContext.SaveChanges();
            #endregion


        }


    }
}
