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


    public interface IWalletRepository : IRepositoryBase<Wallet>
    {
    }


    public class WalletRepository : RepositoryBase<Wallet>, IWalletRepository
    {
        public WalletRepository(ApplicationDbContext context) : base(context)
        {
       
        }
   
    }
}
