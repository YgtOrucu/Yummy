using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yummy.WebAPI.Entities;

namespace Yummy.WebAPI.Configurations
{
    public class EmployeeTaskConfiguration : IEntityTypeConfiguration<EmployeeTask>
    {
        public void Configure(EntityTypeBuilder<EmployeeTask> builder)
        {
            builder.Property(x => x.TaskName).HasColumnType("varchar").HasMaxLength(50);
            builder.Property(x => x.Priority).HasColumnType("varchar").HasMaxLength(15);
        }
    }
}
