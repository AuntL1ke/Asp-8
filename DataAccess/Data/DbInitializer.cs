using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Data
{
    public static class DbInitializer
    {
        public static void SeedData(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(new List<Category>()
            {
                new Category { Id = 1, Name = "Sedan", Description = "Standard sedan" },
                new Category { Id = 2, Name = "SUV", Description = "Sport Utility Vehicle" },
                new Category { Id = 3, Name = "Coupe", Description = "Two-door coupe" },
                new Category { Id = 4, Name = "Hatchback", Description = "Compact hatchback" },
                new Category { Id = 5, Name = "Convertible", Description = "Convertible with a retractable roof" }
            });

            modelBuilder.Entity<Car>().HasData(new List<Car>()
            {
                new Car { Id = 1, Model = "Ford Mondeo", Color = "White", Year = 2008, CategoryId = 1 },
                new Car { Id = 2, Model = "BMW 3 Series", Color = "Black", Year = 2015, CategoryId = 3 },
                new Car { Id = 3, Model = "Mercedes-Benz C-Class", Color = "Silver", Year = 2017, CategoryId = 1 },
                new Car { Id = 4, Model = "Audi Q7", Color = "Gray", Year = 2020, CategoryId = 2 },
                new Car { Id = 5, Model = "Volkswagen Golf", Color = "Blue", Year = 2019, CategoryId = 4 },
                new Car { Id = 6, Model = "Tesla Model 3", Color = "Red", Year = 2022, CategoryId = 1 },
                new Car { Id = 7, Model = "Toyota RAV4", Color = "Green", Year = 2021, CategoryId = 2 },
                new Car { Id = 8, Model = "Mazda MX-5", Color = "White", Year = 2016, CategoryId = 5 },
                new Car { Id = 9, Model = "Chevrolet Camaro", Color = "Yellow", Year = 2018, CategoryId = 3 },
                new Car { Id = 10, Model = "Ford Explorer", Color = "Black", Year = 2020, CategoryId = 2 },
                new Car { Id = 11, Model = "Nissan Juke", Color = "Orange", Year = 2015, CategoryId = 4 },
                new Car { Id = 12, Model = "Porsche 911", Color = "Red", Year = 2021, CategoryId = 5 },
                new Car { Id = 13, Model = "Hyundai Elantra", Color = "Silver", Year = 2019, CategoryId = 1 },
                new Car { Id = 14, Model = "Subaru Outback", Color = "Blue", Year = 2018, CategoryId = 2 },
                new Car { Id = 15, Model = "Mini Cooper", Color = "Green", Year = 2017, CategoryId = 4 },
                new Car { Id = 16, Model = "Dodge Challenger", Color = "Black", Year = 2020, CategoryId = 3 },
                new Car { Id = 17, Model = "Jaguar F-Type", Color = "White", Year = 2022, CategoryId = 5 },
                new Car { Id = 18, Model = "Honda Accord", Color = "Gray", Year = 2016, CategoryId = 1 },
                new Car { Id = 19, Model = "Lexus RX", Color = "Blue", Year = 2021, CategoryId = 2 },
                new Car { Id = 20, Model = "Volkswagen Beetle", Color = "Yellow", Year = 2019, CategoryId = 4 }
            });

        }
    }
}
