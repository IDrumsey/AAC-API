using AnimalAdoptionCenter.Services.Authentication;
using AnimalAdoptionCenterModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalAdoptionCenter.Data
{
    public class AppDatabaseContext : DbContext
    {
        private readonly IPasswordHasher _passwordHasher;
        public AppDatabaseContext(DbContextOptions options, IPasswordHasher passwordHasher) : base(options)
        {
            this._passwordHasher = passwordHasher;
        }

        public DbSet<Store> Stores { get; set; }
        public DbSet<Animal> Animals { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // config tables, columns, and relationships
            modelBuilder.Entity<Store>().ToTable("Store");
            modelBuilder.Entity<Animal>().ToTable("Animal");
            modelBuilder.Entity<User>().ToTable("User");

            // relationships, not sure if these lines were necessary
            modelBuilder.Entity<Store>().HasMany(store => store.Animals);

            modelBuilder.Entity<Store>().HasMany(store => store.operationHours);

            modelBuilder.Entity<Store>().HasMany(store => store.pictures).WithOne().HasForeignKey("StoreId").OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Animal>().HasMany(animal => animal.pictures).WithOne().HasForeignKey("AnimalId").OnDelete(DeleteBehavior.Cascade);

            // animals
            var Charlie = new Animal
            {
                AnimalId = 1,
                name = "Charlie",
                age = 5,
                gender = 'm',
                classificationName = "Dog",
                species = "Golden Retriever",
                heightInches = 30,
                weight = 80,
                favoriteToy = "Rope",
                favoriteActivity = "Going on long walks",
                description = "Charlie was raised as a pup here from a litter of 9 other brothers and sisters.",
                storeId = 1
            };

            var Bailey = new Animal
            {
                AnimalId = 2,
                name = "Bailey",
                age = 2,
                gender = 'f',
                classificationName = "Dog",
                species = "West Highland White terrier",
                heightInches = 15,
                weight = 40,
                favoriteToy = "Squeaky toys",
                favoriteActivity = "Going to the park",
                description = "Bailey was found on the side of a road and brought to the center for rehabilitation and has been here almost a year.",
                storeId = 1
            };

            // add animal data
            modelBuilder.Entity<Animal>().HasData
            (
                Charlie,
                Bailey
            );

            modelBuilder.Entity<SimpleTime>().HasData
            (
                new SimpleTime(1, 1, IntervalSide.open, 8, 0, 0),
                new SimpleTime(2, 1, IntervalSide.close, 17, 0, 0),

                new SimpleTime(3, 2, IntervalSide.open, 8, 0, 0),
                new SimpleTime(4, 2, IntervalSide.close, 17, 0, 0),

                new SimpleTime(5, 3, IntervalSide.open, 8, 0, 0),
                new SimpleTime(6, 3, IntervalSide.close, 17, 0, 0),

                new SimpleTime(7, 4, IntervalSide.open, 8, 0, 0),
                new SimpleTime(8, 4, IntervalSide.close, 17, 0, 0),

                new SimpleTime(9, 5, IntervalSide.open, 8, 0, 0),
                new SimpleTime(10, 5, IntervalSide.close, 17, 0, 0),

                new SimpleTime(11, 6, IntervalSide.open, 7, 0, 0),
                new SimpleTime(12, 6, IntervalSide.close, 20, 0, 0)
            );

            modelBuilder.Entity<DayOperationHours>().HasMany(h => h.times);

            // operation hours
            var Sunday = new DayOperationHours { DayOperationHoursId = 1, day = DayNames.Sunday, StoreId = 1 };
            var Monday = new DayOperationHours { DayOperationHoursId = 2, day = DayNames.Monday, StoreId = 1 };
            var Tuesday = new DayOperationHours { DayOperationHoursId = 3, day = DayNames.Tuesday, StoreId = 1 };
            var Wednesday = new DayOperationHours { DayOperationHoursId = 4, day = DayNames.Wednesday, StoreId = 1 };
            var Thursday = new DayOperationHours { DayOperationHoursId = 5, day = DayNames.Thursday, StoreId = 1 };
            var Friday = new DayOperationHours { DayOperationHoursId = 6, day = DayNames.Friday, StoreId = 1 };
            var Saturday = new DayOperationHours { DayOperationHoursId = 7, day = DayNames.Saturday, StoreId = 1 };

            // add the operation hours data
            modelBuilder.Entity<DayOperationHours>().HasData(Sunday, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday);

            // I don't think this line was necessary because of the naming convention used
            modelBuilder.Entity<DayOperationHours>().HasKey(p => p.DayOperationHoursId);

            // add store data
            modelBuilder.Entity<Store>().HasData
            (
                new Store
                {
                    address = "123 abcd st. NW someCity, FL",
                    StoreId = 1
                }
            );

            // add user data
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = 1,
                    username = "testuser1",
                    password = this._passwordHasher.HashPassword("testPassword1"),
                    role = "Employee"
                },
                new User
                {
                    UserId = 2,
                    username = "testuser2",
                    password = this._passwordHasher.HashPassword("testPassword2"),
                    role = "Manager"
                }
            );
        }
    }
}
