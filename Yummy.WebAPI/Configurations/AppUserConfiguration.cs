using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yummy.WebAPI.Entities;

namespace Yummy.WebAPI.Configurations
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(x => x.Name).HasColumnType("varchar").HasMaxLength(30);
            builder.Property(x => x.Surname).HasColumnType("varchar").HasMaxLength(30);
            builder.Property(x => x.AvatarUrl).HasColumnType("varchar").HasMaxLength(80);
            builder.Property(x => x.UserName).HasColumnType("varchar").HasMaxLength(50);
            builder.Property(x => x.NormalizedUserName).HasColumnType("varchar").HasMaxLength(50);
            builder.Property(x => x.Email).HasColumnType("varchar").HasMaxLength(30);
            builder.Property(x => x.NormalizedEmail).HasColumnType("varchar").HasMaxLength(30);
            builder.Property(x => x.PhoneNumber).HasColumnType("varchar").HasMaxLength(15);
        }
    }
}
