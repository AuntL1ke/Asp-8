using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
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
        public void AddOrder(Order order)
        {
            Orders.Add(order);
            SaveChanges();
        }
        public List<Car> GetCarList()
        {
            IQueryable<Car> carsAsNoTracking = Cars.AsNoTracking();
            IQueryable<Car> cars = IncludeCategories(carsAsNoTracking).AsNoTracking();

            return cars.ToList();
        }
        public IIncludableQueryable<Car, Category> IncludeCategories(IQueryable<Car> cars)
        {
            return cars.Include(car => car.Category)!;
        }
        public List<Car> GetCarsFromIdsString(string carIdsString)
        {
            List<Car> cars = GetCarList();

            string formattedCarIds = carIdsString
                                        .Replace("[", "")
                                        .Replace("]", "");

            List<string> carIdsStrings = formattedCarIds.Split(',').ToList();
            List<int> carIds = carIdsStrings.Select(id => int.Parse(id)).ToList();
            List<Car> selectedCars = carIds.SelectMany(id => cars.Where(car => car.Id == id).Take(1)).ToList();

            return selectedCars;
        }
        public List<Order> GetOrderListByUserId(string userId)
        {
            IQueryable<Order> orders = Orders.Where(order => order.UserId.Equals(userId));

            return orders.ToList();
        }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Category> Categories { get; set; }    
        public DbSet<Order> Orders { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
