using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yummy.WebAPI.Entities;

namespace Yummy.WebAPI.Configurations
{
    public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.Property(x => x.Name).HasColumnType("varchar").HasMaxLength(50);
            builder.Property(x => x.Email).HasColumnType("varchar").HasMaxLength(50);
            builder.Property(x => x.Phone).HasColumnType("varchar").HasMaxLength(20);
            builder.Property(x => x.ReservationTime).HasColumnType("varchar").HasMaxLength(20);
            builder.Property(x => x.Message).HasColumnType("varchar").HasMaxLength(500);
            builder.Property(x => x.Status).HasColumnType("varchar").HasMaxLength(25);
        }
    }
}
