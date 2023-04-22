using Contracts.Entities;
using Domain.Context;
using StakeHoldersWebApi.Models.IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{


    public interface IVendorRepository : IRepositoryBase<Vendor>
    {
    }


    public class VendorRepository : RepositoryBase<Vendor>, IVendorRepository
    {
        public VendorRepository(ApplicationDbContext context) : base(context)
        {
       
        }
   
    }
}
