using FinalProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinalProject.Persistence.Configurations
{
    internal class PatientConfigurations : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasColumnType("nvarchar(100)");
            builder.Property(x => x.Surname).IsRequired().HasColumnType("nvarchar(100)");
            builder.Property(x => x.Email).IsRequired().HasMaxLength(256);
            builder.Property(x => x.Phone).IsRequired();
        }
    }
}
