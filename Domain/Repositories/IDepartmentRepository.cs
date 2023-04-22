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


    public interface IDepartmentRepository : IRepositoryBase<Department>
    {

    }

    public class DepartmentRepository : RepositoryBase<Department>, IDepartmentRepository
    {
        public DepartmentRepository(ApplicationDbContext context) : base(context)
        {
       
        }
   
    }
}
