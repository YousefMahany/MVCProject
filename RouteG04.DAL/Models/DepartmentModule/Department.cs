using Microsoft.EntityFrameworkCore.Storage.Json;
using RouteG04.DAL.Models.EmployeeModule;
using RouteG04.DAL.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteG04.DAL.Models.DepartmentModule
{
    public class Department : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        public string? Description { get; set; } 
        public virtual ICollection<Employee> employees { get; set; } = new HashSet<Employee>();

    }
}
