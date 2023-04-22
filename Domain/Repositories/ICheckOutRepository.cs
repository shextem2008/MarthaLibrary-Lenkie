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


    public interface ICheckOutRepository : IRepositoryBase<CheckOut>
    {
    }


    public class CheckOutRepository : RepositoryBase<CheckOut>, ICheckOutRepository
    {
        public CheckOutRepository(ApplicationDbContext context) : base(context)
        {
       
        }
   
    }
}
