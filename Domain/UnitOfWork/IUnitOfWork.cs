using Domain.Context;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UnitOfWork
{
    public interface IUnitOfWork
    {

        ILibraryBookRepository libraryBookRepository  { get; }
        ICheckOutRepository checkOutRepository  { get; }
        IUserRepository userRepository {get; }
        IRoleClaimRepository roleClaimRepository { get; }
        ITenantRepository tenantRepository { get; }
        IEmployeeRepository employeeRepository  { get;  }
        IWalletRepository walletRepository  { get;  }
        IClientRepository clientRepository { get; }
        IVendorRepository vendorRepository { get; }
        ILocationRepository locationRepository  { get; }
        IDepartmentRepository departmentRepository   { get; }
        Task<bool> SaveChangesAsync();
        void BeginTransaction();
        void Commit();
        void Rollback();

    }

    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _context;
        private bool disposed = false;
      
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        #region constructor to inject repo in unit works

        private ILibraryBookRepository _libraryBookRepository;
        private ICheckOutRepository _checkOutRepository ;
        private IUserRepository _userRepository;
        private IRoleClaimRepository _roleClaimRepository;
        private ITenantRepository _tenantRepository;
        private IEmployeeRepository _employeeRepository ;
        private IWalletRepository _walletRepository;
        private IClientRepository _clientRepository;
        private IVendorRepository _vendorRepository ;
        private ILocationRepository _locationRepository;
        private IDepartmentRepository _departmentRepository ;

        public ILibraryBookRepository libraryBookRepository  => _libraryBookRepository ??=
                                                        new LibraryBookRepository(_context);
        public ICheckOutRepository checkOutRepository  => _checkOutRepository ??=
                                                         new CheckOutRepository(_context);
        public IUserRepository userRepository => _userRepository ??= new UserRepository(_context);

        public IRoleClaimRepository roleClaimRepository => _roleClaimRepository ??= 
                                                           new RoleClaimRepository(_context);
        public ITenantRepository tenantRepository  => _tenantRepository ??=
                                                         new TenantRepository(_context);
        public IEmployeeRepository employeeRepository  => _employeeRepository ??=
                                                        new EmployeeRepository(_context);
        public IWalletRepository walletRepository => _walletRepository ??=
                                                        new WalletRepository(_context);
        public IClientRepository clientRepository  => _clientRepository ??=
                                                  new ClientRepository(_context);
        public IVendorRepository vendorRepository => _vendorRepository ??=
                                                  new VendorRepository(_context);
        public ILocationRepository locationRepository  => _locationRepository ??=
                                                  new LocationRepository(_context);
        public IDepartmentRepository departmentRepository  => _departmentRepository ??=
                                                  new DepartmentRepository(_context);
        #endregion

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }

            this.disposed = true;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }


        public void BeginTransaction()
        {
            _context.ChangeTracker.AutoDetectChangesEnabled = false;

            if (_context.Database.GetDbConnection().State != ConnectionState.Open)
                _context.Database.OpenConnection();

            _context.Database.BeginTransaction();
        }

        public void Commit()
        {
            _context.ChangeTracker.DetectChanges();
            SaveChanges();
            _context.Database.CurrentTransaction.Commit();
        }

        public void Rollback()
        {
            _context.Database.CurrentTransaction?.Rollback();
        }
    }

}
