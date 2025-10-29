using RouteG04.DAL.Models.EmployeeModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteG04.DAL.Data.Configurations
{
    public class EmployeeConfigurations :BaseEntityConfigurations<Employee>, IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(e => e.Name).HasColumnType("varchar(50)");
            builder.Property(e => e.Address).HasColumnType("varchar(50)");
            builder.Property(e => e.Salary).HasColumnType("decimal(10,2)");
            builder.Property(p => p.Gender)
                .HasConversion((EmpGender) => EmpGender.ToString(),
                (_gender) => (Gender)Enum.Parse(typeof(Gender), _gender));
            builder.Property(p=>p.EmployeeType)
                .HasConversion((EmpType)=>EmpType.ToString(),
                (_type) => (EmployeeTypes)Enum.Parse(typeof(EmployeeTypes), _type));
            base.Configure(builder);
        }
    }
}
