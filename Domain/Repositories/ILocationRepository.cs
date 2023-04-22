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


    public interface ILocationRepository : IRepositoryBase<Location>
    {
    }


    public class LocationRepository : RepositoryBase<Location>, ILocationRepository
    {
        public LocationRepository(ApplicationDbContext context) : base(context)
        {
       
        }
   
    }
}
