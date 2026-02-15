using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Yummy.WebAPI.Entities;

namespace Yummy.WebAPI.Context
{
    public class YummyContext : DbContext
    {
        public YummyContext(DbContextOptions<YummyContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        public DbSet<Hero> Heroes { get; set; }
        public DbSet<About> Abouts { get; set; }
        public DbSet<Feature> Features  { get; set; }
        public DbSet<Category> Categories  { get; set; }
        public DbSet<Product> Products  { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }
        public DbSet<Chef> Chefs { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Gallery> Galleries  { get; set; }
        public DbSet<Contact> Contacts  { get; set; }
        public DbSet<Message> Messages   { get; set; }
        public DbSet<YummyEvents> YummyEvents { get; set; }
    }
}
