using Microsoft.AspNetCore.Identity;
using RouteG04.DAL.Data.Contexts;
using RouteG04.DAL.Models.UserModule;
using RouteG04.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteG04.DAL.Repositories.Classes
{
    public class UserRepository(ApplicationDbContext dbContext) : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

        public void Add(ApplicationUser user)
        {
            _dbContext.Set<ApplicationUser>().Add(user);
        }

        public void Delete(ApplicationUser user)
        {
            _dbContext.Set<ApplicationUser>().Remove(user);
        }

        public IEnumerable<ApplicationUser> GetAll(bool IsTracking = false)
        {
            if (IsTracking)
            {
                return _dbContext.Set<ApplicationUser>().ToList();
            }
            else
            {
                return _dbContext.Set<ApplicationUser>().AsNoTracking().ToList();
            }
        }

        public ApplicationUser? GetById(string id) => _dbContext.Set<ApplicationUser>().Find(id);


        public void Update(ApplicationUser user)
        {
            _dbContext.Set<ApplicationUser>().Update(user);
        }
    }
}
