using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteG04.DAL.Data.Configurations
{
    public class BaseEntityConfigurations<T> : IEntityTypeConfiguration<T> where T : BaseEntity
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(p => p.CreatedOn).HasDefaultValueSql("GETDATE()");
            builder.Property(p => p.LastModificationOn).HasComputedColumnSql("GETDATE()");
        }
    }
}
