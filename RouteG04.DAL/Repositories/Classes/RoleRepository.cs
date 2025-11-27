using Microsoft.AspNetCore.Identity;
using RouteG04.DAL.Data.Contexts;
using RouteG04.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteG04.DAL.Repositories.Classes
{
    public class RoleRepository(ApplicationDbContext dbContext):IRoleRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

        public void Add(IdentityRole role)
        {
            _dbContext.Roles.Add(role);
        }

        public IEnumerable<IdentityRole> GetAll(bool withTracking = false)
        {
            if (withTracking)
            {
                return _dbContext.Roles.ToList();
            }
            else
            {
                return _dbContext.Roles.AsNoTracking().ToList();
            }
        }

        public IdentityRole? GetById(string id)
        {
            return _dbContext.Roles.Find(id);
        }

        public void Delete(IdentityRole role)
        {
            _dbContext.Roles.Remove(role);
        }

        public void Update(IdentityRole role)
        {
            _dbContext.Roles.Update(role);
        }
    }
}
