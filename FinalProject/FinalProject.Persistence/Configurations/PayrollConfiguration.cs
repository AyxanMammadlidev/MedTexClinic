using FinalProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinalProject.Persistence.Configurations
{
    internal class PayrollConfiguration : IEntityTypeConfiguration<Payroll>
    {
        public void Configure(EntityTypeBuilder<Payroll> builder)
        {
            builder.Property(x => x.Salary).HasColumnType("decimal(8,2)");
            builder.Property(x => x.NetSalary).HasColumnType("decimal(8,2)");
            builder.Property(x => x.TaxRate).HasColumnType("decimal(6,3)");
            builder.Property(x => x.InsuranceRate).HasColumnType("decimal(6,3)");
        }
    }
}
