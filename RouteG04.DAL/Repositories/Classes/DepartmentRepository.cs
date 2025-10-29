using RouteG04.DAL.Data.Contexts;
using RouteG04.DAL.Models.DepartmentModule;
using RouteG04.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteG04.DAL.Repositories.Classes
{
    //Primary Constructor .Net
    public class DepartmentRepository(ApplicationDbContext dbContext) :GenericRepository<Department>(dbContext), IDepartmentRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

        //private readonly ApplicationDbContext _dbContext;

        //public DepartmentRepository(ApplicationDbContext dbContext)
        //{
        //    //Ask CLR For Creating Object From ApplicationDbContext
        //    _dbContext = dbContext;
        //}
        //CRUD
        


    }
}
