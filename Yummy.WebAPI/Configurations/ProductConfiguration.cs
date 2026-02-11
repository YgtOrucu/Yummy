using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yummy.WebAPI.Entities;

namespace Yummy.WebAPI.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.ProductName).HasMaxLength(35);
            builder.Property(x => x.Description).HasMaxLength(250);
            builder.Property(x => x.Price).HasColumnType("decimal(10,2)");
        }
    }
}
