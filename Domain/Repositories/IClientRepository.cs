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


    public interface IClientRepository : IRepositoryBase<Client>
    {
    }


    public class ClientRepository : RepositoryBase<Client>, IClientRepository
    {
        public ClientRepository(ApplicationDbContext context) : base(context)
        {
       
        }
   
    }
}
