using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteG04.DAL.Models
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public int CreatedBy { get; set; } //UserId
        public DateTime? CreatedOn { get; set; }
        public int LastModificationBy { get; set; }//UserId
        public DateTime? LastModificationOn { get; set; }
        public bool IsDeleted { get; set; }//SoftDelete
    }
}
