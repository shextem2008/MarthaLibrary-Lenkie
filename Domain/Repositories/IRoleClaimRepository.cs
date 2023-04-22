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


    public interface IRoleClaimRepository : IRepositoryBase<ApplicationRoleClaims>
    {
    }


    public class RoleClaimRepository : RepositoryBase<ApplicationRoleClaims>, IRoleClaimRepository
    {
        public RoleClaimRepository(ApplicationDbContext context) : base(context)
        {
       
        }
   
    }
}
