using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using Yummy.WebAPI.Entities;

namespace Yummy.WebAPI.Configurations
{
    public class ChefTaskConfiguration : IEntityTypeConfiguration<ChefTask>
    {
        public void Configure(EntityTypeBuilder<ChefTask> builder)
        {
            builder.HasKey(ct => new { ct.ChefId, ct.EmployeeTaskId });
            builder.HasOne(ct => ct.Chef).WithMany(c => c.ChefTasks).HasForeignKey(ct => ct.ChefId);
            builder.HasOne(ct => ct.EmployeeTask).WithMany(et => et.ChefTasks).HasForeignKey(ct => ct.EmployeeTaskId);
        }
    }
}
