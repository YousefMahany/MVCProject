using RouteG04.DAL.Models.Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteG04.BLL.DTOS.Employee
{
    public class EmployeeDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int? Age { get; set; }
        public string? Address { get; set; }
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        //---------------------
        public DateTime? HiringDate { get; set; }
        public Gender EmpGender { get; set; }
        [Display(Name = "Employee Type")]
        public EmployeeTypes EmployeeType { get; set; }
        //----------------------------
        public int CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
    }
}
