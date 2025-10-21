using RouteG04.DAL.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteG04.DAL.Repositories
{
    //Primary Constructor .Net
    public class DepartmentRepository(ApplicationDbContext dbContext) : IDepartmentRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

        //private readonly ApplicationDbContext _dbContext;

        //public DepartmentRepository(ApplicationDbContext dbContext)
        //{
        //    //Ask CLR For Creating Object From ApplicationDbContext
        //    _dbContext = dbContext;
        //}
        //CRUD
        //Get All
        public IEnumerable<Department> GetAll(bool IsTracking = false)
        {
            if (IsTracking)
            {
                return _dbContext.Departments.ToList();
            }
            else
            {
                return _dbContext.Departments.AsNoTracking().ToList();
            }
        }

        //Get Department By Id
        public Department? GetById(int id)
        {
            var Department = _dbContext.Departments.Find(id);
            return Department;
        }

        //Add

        public int Add(Department department)
        {
            _dbContext.Departments.Add(department);
            return _dbContext.SaveChanges();
        }

        //Update

        public int Update(Department department)
        {
            _dbContext.Departments.Update(department);
            return _dbContext.SaveChanges();
        }

        //Delete
        public int Delete(Department department)
        {
            _dbContext.Departments.Remove(department);
            return _dbContext.SaveChanges();
        }


    }
}
