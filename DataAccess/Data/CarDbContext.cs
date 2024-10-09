using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace DataAccess.Data
{
    public class CarDbContext:IdentityDbContext
    {
        public CarDbContext(DbContextOptions<CarDbContext> options) : base(options) {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            DbInitializer.SeedData(modelBuilder);
        }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Category> Categories { get; set; }    
        public DbSet<User> Users { get; set; }
    }
}
