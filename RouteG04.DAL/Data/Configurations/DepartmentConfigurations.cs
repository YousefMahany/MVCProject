
namespace RouteG04.DAL.Data.Configurations
{
    public class DepartmentConfigurations : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.Property(p => p.Id).UseIdentityColumn(10, 10);
            builder.Property(p => p.Name).HasColumnType("varchar(20)");
            builder.Property(p => p.Code).HasColumnType("varchar(20)");
            builder.Property(p => p.CreatedOn).HasDefaultValueSql("GETDATE()");
            builder.Property(p => p.LastModificationOn).HasComputedColumnSql("GETDATE()");

        }
    }
}
