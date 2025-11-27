using Microsoft.AspNetCore.Identity;
using RouteG04.DAL.Models.UserModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteG04.DAL.Repositories.Interfaces
{
    public interface IUserRepository
    {
        void Add(ApplicationUser user);
        IEnumerable<ApplicationUser> GetAll(bool withtracking = false);
        ApplicationUser? GetById(string id);
        void Delete(ApplicationUser user);
        void Update(ApplicationUser user);
    }
}
