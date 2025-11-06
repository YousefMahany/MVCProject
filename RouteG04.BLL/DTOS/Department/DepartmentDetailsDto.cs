using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteG04.BLL.DTOS
{
    public class DepartmentDetailsDto
    {
        public int DeptId { get; set; }
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime? DateOfCreation { get; set; }
        public int LastModificationBy { get; set; }//UserId
        public DateTime? LastModificationOn { get; set; }
        public int CreatedBy { get; set; } //UserId
        public bool IsDeleted { get; set; }//SoftDelete
       
    }
}
