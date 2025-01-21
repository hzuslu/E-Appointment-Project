using eAppointmentServer.Domain.Entities;
using eAppointmentServer.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eAppointmentServer.Infrastructure.Configurations
{
    internal class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.FirstName)
                .HasColumnType("varchar(50)")
                .IsRequired();

            builder.Property(p => p.LastName)
                .HasColumnType("varchar(50)")
                .IsRequired();

            builder.Property(p => p.Department)
                .HasConversion<int>()
                .IsRequired();

            builder.Property(p => p.Department)
                 .HasConversion(
                        item => item.Value,
                        item => DepartmentEnumaration.FromValue(item)
                    ).HasColumnName("Department")
                    .IsRequired();
        }
    }
}
