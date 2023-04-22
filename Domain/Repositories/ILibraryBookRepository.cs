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


    public interface ILibraryBookRepository : IRepositoryBase<LibraryBook>
    {
    }


    public class LibraryBookRepository : RepositoryBase<LibraryBook>, ILibraryBookRepository
    {
        public LibraryBookRepository(ApplicationDbContext context) : base(context)
        {
       
        }
   
    }
}
