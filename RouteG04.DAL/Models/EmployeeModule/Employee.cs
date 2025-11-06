
using RouteG04.DAL.Models.DepartmentModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteG04.DAL.Models.EmployeeModule
{
    public class Employee : BaseEntity
    {
        public string Name { get; set; } = null!;
        public int Age { get; set; }
        public string? Address { get; set; }
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? HiringDate { get; set; }
        public Gender Gender { get; set; }
        public EmployeeTypes EmployeeType { get; set; }
        public int? DepartmentId { get; set; } 
        public virtual Department? Department { get; set; }
    }
}
