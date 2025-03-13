using FinalProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinalProject.Persistence.Configurations
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasColumnType("nvarchar(100)");
            builder.Property(x => x.SKU).IsRequired().HasColumnType("varchar(10)");
            builder.Property(x => x.Description).IsRequired().HasColumnType("nvarchar(1000)");
            builder.Property(x => x.Price).HasColumnType("decimal(8,2)");
        }
    }
}
