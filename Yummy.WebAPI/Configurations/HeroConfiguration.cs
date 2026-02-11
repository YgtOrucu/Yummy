using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yummy.WebAPI.Entities;

namespace Yummy.WebAPI.Configurations
{
    public class HeroConfiguration : IEntityTypeConfiguration<Hero>
    {
        public void Configure(EntityTypeBuilder<Hero> builder)
        {
            builder.Property(x => x.Title).HasColumnType("varchar").HasMaxLength(35);
            builder.Property(x => x.SubTitle).HasColumnType("varchar").HasMaxLength(35);
            builder.Property(x => x.Description).HasColumnType("varchar").HasMaxLength(250);

        }
    }
}
