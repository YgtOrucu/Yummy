using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yummy.WebAPI.Entities;

namespace Yummy.WebAPI.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(x => x.CategoryName).HasColumnType("varchar").HasMaxLength(35);
            builder.Property(x => x.DataTarget).HasColumnType("varchar").HasMaxLength(50);
        }
    }
}
