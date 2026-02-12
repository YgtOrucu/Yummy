using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yummy.WebAPI.Entities;

namespace Yummy.WebAPI.Configurations
{
    public class ContactConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.Property(x => x.MapLocation).HasColumnType("varchar(MAX)");
            builder.Property(x => x.Address).HasColumnType("varchar").HasMaxLength(250);
            builder.Property(x => x.Phone).HasColumnType("varchar").HasMaxLength(30);
            builder.Property(x => x.Email).HasColumnType("varchar").HasMaxLength(30);
            builder.Property(x => x.OpenHours).HasColumnType("varchar").HasMaxLength(50);
        }
    }
}
