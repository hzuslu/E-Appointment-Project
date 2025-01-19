using eAppointmentServer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eAppointmentServer.Infrastructure.Configurations
{
    internal class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.FirstName)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.Property(p => p.LastName)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.HasIndex(p => p.IdentityNumber)
                .IsUnique();

            builder.Property(p => p.FullName)
                .HasComputedColumnSql("CONCAT(FirstName, ' ', LastName)");

            builder.Property(p => p.City)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.Property(p => p.Town)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.Property(p => p.FullAddress)
                .HasColumnType("varchar(200)");

            builder.HasIndex(p => new { p.City, p.Town });

        }
    }
}
