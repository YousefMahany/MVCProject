using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteG04.DAL.Repositories.Interfaces
{
    public interface IRoleRepository
    {
        void Add(IdentityRole role);
        IEnumerable<IdentityRole> GetAll(bool withTracking = false);
        IdentityRole? GetById(string id);
        void Delete(IdentityRole role);
        void Update(IdentityRole role);
    }
}
