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


    public interface ITenantRepository : IRepositoryBase<Tenant>
    {
    }


    public class TenantRepository : RepositoryBase<Tenant>, ITenantRepository
    {
        public TenantRepository(ApplicationDbContext context) : base(context)
        {
       
        }
   
    }
}
