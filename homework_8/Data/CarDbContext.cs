using homework_8.Models;
using Microsoft.EntityFrameworkCore;

namespace homework_8.Data
{
    public class CarDbContext:DbContext
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
    }
}
