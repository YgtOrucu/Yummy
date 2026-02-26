using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yummy.WebAPI.Entities;

namespace Yummy.WebAPI.Configurations
{
    public class AboutConfiguration : IEntityTypeConfiguration<About>
    {

        public void Configure(EntityTypeBuilder<About> builder)
        {
            builder.Property(x => x.Title).HasColumnType("varchar").HasMaxLength(35);
            builder.Property(x => x.Description).HasColumnType("varchar").HasMaxLength(250);
            builder.Property(x => x.ListDescription1).HasColumnType("varchar").HasMaxLength(50);
            builder.Property(x => x.ListDescription2).HasColumnType("varchar").HasMaxLength(50);
            builder.Property(x => x.ListDescription3).HasColumnType("varchar").HasMaxLength(50);
            builder.Property(x => x.ReservationNumber).HasColumnType("varchar").HasMaxLength(20);
        }
    }
}
