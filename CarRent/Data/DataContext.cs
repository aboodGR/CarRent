using CarRent.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRent.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<Car> tblCars { get; set; }
        public DbSet<Customer> tblCustomers { get; set; }
        public DbSet<Rental> tblRentals { get; set; }
        public DbSet<CustomerCar> CustomerCars { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CustomerCar>()
           .HasKey(cc => new { cc.CustomerId, cc.CarId });

            modelBuilder.Entity<CustomerCar>()
           .HasOne(cc => cc.Customer)
           .WithMany(c => c.CustomerCars)
           .HasForeignKey(cc => cc.CustomerId);

            modelBuilder.Entity<CustomerCar>()
           .HasOne(cc => cc.Car)
           .WithMany(c => c.CustomerCars)
           .HasForeignKey(cc => cc.CarId);


            modelBuilder.Entity<Car>().HasData(
                new Car
                {
                    CarId = 1,
                    Brand = "Toyota",
                    Model = "Corolla",
                    Year = 2020,
                    RentalType = ERentalType.Daily,
                    PricePerDay = 30
                },
                new Car
                {
                    CarId = 2,
                    Brand = "Hyundai",
                    Model = "Elantra",
                    Year = 2021,
                    RentalType = ERentalType.Daily,
                    PricePerDay = 35
                },
                new Car
                {
                    CarId = 3,
                    Brand = "BMW",
                    Model = "X3",
                    Year = 2019,
                    RentalType = ERentalType.Weekly,
                    PricePerDay = 60
                },
                new Car
                {
                    CarId = 4,
                    Brand = "Mercedes",
                    Model = "C200",
                    Year = 2022,
                    RentalType = ERentalType.Monthly,
                    PricePerDay = 120
                }
            );

            // Customers
            modelBuilder.Entity<Customer>().HasData(
                new Customer { CustomerId = 1, FullName = "Alice", Email = "alice@email.com" },
                new Customer { CustomerId = 2, FullName = "Bob", Email = "bob@email.com" }
            );

            // CustomerCar (many-to-many)
            modelBuilder.Entity<CustomerCar>().HasData(
                new CustomerCar { CustomerId = 1, CarId = 1 },
                new CustomerCar { CustomerId = 1, CarId = 2 },
                new CustomerCar { CustomerId = 2, CarId = 3 }
            );
        }

    }
}
