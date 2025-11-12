using RouteG04.DAL.Data.Contexts;
using RouteG04.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteG04.DAL.Repositories.Classes
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Lazy<IEmployeeRepository> _employeeRepository;
        private readonly Lazy<IDepartmentRepository> _departmentRepository;
        private readonly ApplicationDbContext _dbContext;
        public UnitOfWork(IEmployeeRepository employeeRepository,IDepartmentRepository departmentRepository ,ApplicationDbContext dbContext)
        {
            _employeeRepository = new Lazy<IEmployeeRepository>(()=>new EmployeeRepository(_dbContext));
            _departmentRepository = new Lazy<IDepartmentRepository>(() => new DepartmentRepository(_dbContext));
            _dbContext = dbContext;
        }
        public IEmployeeRepository EmployeeRepository => _employeeRepository.Value;

        public IDepartmentRepository DepartmentRepository => _departmentRepository.Value;

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }
    }
}
