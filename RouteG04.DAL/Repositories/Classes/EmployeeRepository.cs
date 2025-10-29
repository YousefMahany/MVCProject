using Microsoft.EntityFrameworkCore;
using RouteG04.DAL.Data.Contexts;
using RouteG04.DAL.Models.DepartmentModule;
using RouteG04.DAL.Models.EmployeeModule;
using RouteG04.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteG04.DAL.Repositories.Classes
{
    public class EmployeeRepository(ApplicationDbContext dbContext) :GenericRepository<Employee>(dbContext), IEmployeeRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

    }
}
