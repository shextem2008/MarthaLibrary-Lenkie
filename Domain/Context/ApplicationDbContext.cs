using Contracts.Collections.Interfaces;
using Contracts.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using StakeHoldersWebApi.Models.IdentityModels;

namespace Domain.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int, IdentityUserClaim<int>, ApplicationUserRole,
     IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>> 
    {
        private const string IsDeletedProperty = "IsDeleted";
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions) : base(dbContextOptions)
        {
            try
            {
                var databaseCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
                if (databaseCreator != null)
                {
                    // Create Database if cannot connect
                    if (!databaseCreator.CanConnect()) databaseCreator.Create();

                    // Create Tables if no tables exist
                    if (!databaseCreator.HasTables()) databaseCreator.CreateTables();
                }
            }
            catch (Exception ex)
            {   
                Console.WriteLine(ex.Message);
            }
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId);

                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId);
            });
        }


        private void OnBeforeSavingData()
        {
            var entires = ChangeTracker.Entries().Where(e => e.State !=
                EntityState.Detached && e.State != EntityState.Unchanged);
            foreach (var entry in entires)
            {
                if (entry.Entity is IFullAudited trackable)
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            trackable.CreationTime = DateTime.Now;
                            trackable.LastModificationTime = DateTime.Now;
                            //trackable.IsDeleted = false;
                            break;
                        case EntityState.Modified:

                            trackable.LastModificationTime = DateTime.Now;
                            break;
                        case EntityState.Deleted:
                            entry.Property(IsDeletedProperty).CurrentValue = true;
                            entry.State = EntityState.Modified;
                            break;
                    }

                }


            }
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
        {
            OnBeforeSavingData();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSavingData();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }


        #region DBSET

        public DbSet<LibraryBook> LibraryBook { get; set; }
        public DbSet<CheckOut> CheckOut { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<Identification> Identification { get; set; }
        public DbSet<ImageFile> ImageFile { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<Region> Region { get; set; }
        public DbSet<State> State { get; set; }
        public DbSet<Tenant> Tenant { get; set; }
        public DbSet<Wallet> Wallet { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<Vendor> Vendor { get; set; }
        public DbSet<Client> Client { get; set; }


        #endregion


    }
}
