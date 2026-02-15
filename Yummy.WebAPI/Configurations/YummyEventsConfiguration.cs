using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yummy.WebAPI.Entities;

namespace Yummy.WebAPI.Configurations
{
    public class YummyEventsConfiguration : IEntityTypeConfiguration<YummyEvents>
    {
        public void Configure(EntityTypeBuilder<YummyEvents> builder)
        {
            builder.Property(x => x.Title).HasColumnType("varchar").HasMaxLength(50);
            builder.Property(x => x.Description).HasColumnType("varchar").HasMaxLength(500);
            builder.Property(x => x.Price).HasColumnType("decimal(18,2)");
            builder.Property(x => x.ImageUrl).HasColumnType("varchar").HasMaxLength(80);
        }
    }
}
