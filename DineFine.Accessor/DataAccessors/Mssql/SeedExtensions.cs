using DineFine.DataObjects.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Identity;

namespace DineFine.Accessor.DataAccessors.Mssql
{
    public static class SeedExtensions
    {
        public static void SeedInitialData(this ModelBuilder builder)
        {
            SeedRoles(builder);
            SeedTableStatus(builder);
            SeedOrderStatus(builder);
            SeedUnits(builder);
            SeedDummyData(builder);
        }

        private static void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<Role>().HasData(
                new Role
                {
                    Id = 1,
                    Name = "SuperAdmin",
                    NormalizedName = "SUPERADMIN",
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    IsDeleted = false
                },
                new Role
                {
                    Id = 2,
                    Name = "RestaurantOwner",
                    NormalizedName = "RESTAURANTOWNER",
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    IsDeleted = false
                },
                new Role
                {
                    Id = 3,
                    Name = "KitchenPersonnel",
                    NormalizedName = "KITCHENPERSONNEL",
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    IsDeleted = false
                },
                new Role
                {
                    Id = 4,
                    Name = "Waiter",
                    NormalizedName = "WAITER",
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    IsDeleted = false
                },
                new Role
                {
                    Id = 5,
                    Name = "Customer",
                    NormalizedName = "CUSTOMER",
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    IsDeleted = false
                }
            );
        }

        private static void SeedTableStatus(ModelBuilder builder)
        {
            builder.Entity<TableStatus>().HasData(
                new TableStatus
                {
                    Id = 1,
                    Name = "Available",
                },
                new TableStatus
                {
                    Id = 2,
                    Name = "Full",
                },
                new TableStatus
                {
                    Id = 3,
                    Name = "Reserved",
                }
            );
        }

        private static void SeedOrderStatus(ModelBuilder builder)
        {
            builder.Entity<OrderStatus>().HasData(
                new OrderStatus
                {
                    Id = 1,
                    Name = "Received",
                },
                new OrderStatus
                {
                    Id = 2,
                    Name = "Preparing",
                },
                new OrderStatus
                {
                    Id = 3,
                    Name = "Ready",
                },
                new OrderStatus
                {
                    Id = 4,
                    Name = "Delivered",
                }
            );
        }

        private static void SeedUnits(ModelBuilder builder)
        {
            builder.Entity<Unit>().HasData(
                new Unit
                {
                    Id = 1,
                    Name = "kilograms",
                },
                new Unit
                {
                    Id = 2,
                    Name = "grams",
                },
                new Unit
                {
                    Id = 3,
                    Name = "liters",
                },
                new Unit
                {
                    Id = 4,
                    Name = "milliliters",
                }
            );
        }

        private static void SeedDummyData(ModelBuilder builder)
        {
            SeedCategories(builder);
            SeedIngredients(builder);
            SeedRestaurants(builder);
            SeedTablesOfRestaurant(builder);
            SeedUsers(builder);
        }

        private static void SeedUsers(ModelBuilder builder)
        {
            var superAdmin = new User
            {
                Id = 1,
                UserName = "SuperAdmin@nttdata.com",
                NormalizedUserName = "SUPERADMIN@NTTDATA.COM",
                Email = "SuperAdmin@nttdata.com",
                NormalizedEmail = "SUPERADMIN@NTTDATA.COM",
                EmailConfirmed = true,
                PhoneNumber = "",
                PhoneNumberConfirmed = true,
                FirstName = "Super Admin",
                LastName = "Super Admin",
                SecurityStamp = Guid.NewGuid().ToString(),
                LockoutEnabled = true,
                TwoFactorEnabled = false,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsDeleted = false,
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                AccessFailedCount = 0,
            };

            var restaurantOwner1 = new User
            {
                Id = 2,
                UserName = "RestaurantOwner1@nttdata.com",
                NormalizedUserName = "RESTAURANTOWNER1@NTTDATA.COM",
                Email = "RestaurantOwner1@nttdata.com",
                NormalizedEmail = "RESTAURANTOWNER1@NTTDATA.COM",
                TenantId = 1,
                EmailConfirmed = true,
                PhoneNumber = "",
                PhoneNumberConfirmed = true,
                FirstName = "RestaurantOwner",
                LastName = "RestaurantOwner",
                SecurityStamp = Guid.NewGuid().ToString(),
                LockoutEnabled = true,
                TwoFactorEnabled = false,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsDeleted = false,
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                AccessFailedCount = 0,
            };
            
            var restaurantOwner2 = new User
            {
                Id = 3,
                UserName = "RestaurantOwner2@nttdata.com",
                NormalizedUserName = "RESTAURANTOWNER2@NTTDATA.COM",
                Email = "RestaurantOwner2@nttdata.com",
                NormalizedEmail = "RESTAURANTOWNER2@NTTDATA.COM",
                TenantId = 2,
                EmailConfirmed = true,
                PhoneNumber = "",
                PhoneNumberConfirmed = true,
                FirstName = "RestaurantOwner",
                LastName = "RestaurantOwner",
                SecurityStamp = Guid.NewGuid().ToString(),
                LockoutEnabled = true,
                TwoFactorEnabled = false,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsDeleted = false,
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                AccessFailedCount = 0,
            };
            
            var restaurantOwner3 = new User
            {
                Id = 4,
                UserName = "RestaurantOwner3@nttdata.com",
                NormalizedUserName = "RESTAURANTOWNER3@NTTDATA.COM",
                Email = "RestaurantOwner3@nttdata.com",
                NormalizedEmail = "RESTAURANTOWNER3@NTTDATA.COM",
                TenantId = 3,
                EmailConfirmed = true,
                PhoneNumber = "",
                PhoneNumberConfirmed = true,
                FirstName = "RestaurantOwner",
                LastName = "RestaurantOwner",
                SecurityStamp = Guid.NewGuid().ToString(),
                LockoutEnabled = true,
                TwoFactorEnabled = false,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsDeleted = false,
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                AccessFailedCount = 0,
            };

            var kitchenPersonnel1 = new User
            {
                Id = 5,
                UserName = "KitchenPersonnel1@nttdata.com",
                NormalizedUserName = "KITCHENPERSONNEL1@NTTDATA.COM",
                Email = "KitchenPersonnel1@nttdata.com",
                NormalizedEmail = "KITCHENPERSONNEL1@NTTDATA.COM",
                TenantId = 1,
                EmailConfirmed = true,
                PhoneNumber = "",
                PhoneNumberConfirmed = true,
                FirstName = "KitchenPersonnel",
                LastName = "KitchenPersonnel",
                SecurityStamp = Guid.NewGuid().ToString(),
                LockoutEnabled = true,
                TwoFactorEnabled = false,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsDeleted = false,
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                AccessFailedCount = 0,
            };
            
            var kitchenPersonnel2 = new User
            {
                Id = 6,
                UserName = "KitchenPersonnel2@nttdata.com",
                NormalizedUserName = "KITCHENPERSONNEL2@NTTDATA.COM",
                Email = "KitchenPersonnel2@nttdata.com",
                NormalizedEmail = "KITCHENPERSONNEL2@NTTDATA.COM",
                TenantId = 2,
                EmailConfirmed = true,
                PhoneNumber = "",
                PhoneNumberConfirmed = true,
                FirstName = "KitchenPersonnel",
                LastName = "KitchenPersonnel",
                SecurityStamp = Guid.NewGuid().ToString(),
                LockoutEnabled = true,
                TwoFactorEnabled = false,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsDeleted = false,
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                AccessFailedCount = 0,
            };
            
            var kitchenPersonnel3 = new User
            {
                Id = 7,
                UserName = "KitchenPersonnel3@nttdata.com",
                NormalizedUserName = "KITCHENPERSONNEL3@NTTDATA.COM",
                Email = "KitchenPersonnel3@nttdata.com",
                NormalizedEmail = "KITCHENPERSONNEL3@NTTDATA.COM",
                TenantId = 3,
                EmailConfirmed = true,
                PhoneNumber = "",
                PhoneNumberConfirmed = true,
                FirstName = "KitchenPersonnel",
                LastName = "KitchenPersonnel",
                SecurityStamp = Guid.NewGuid().ToString(),
                LockoutEnabled = true,
                TwoFactorEnabled = false,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsDeleted = false,
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                AccessFailedCount = 0,
            };

            var waiter1 = new User
            {
                Id = 8,
                UserName = "Waiter1@nttdata.com",
                NormalizedUserName = "WAITER1@NTTDATA.COM",
                Email = "Waiter1@nttdata.com",
                NormalizedEmail = "WAITER1@NTTDATA.COM",
                EmailConfirmed = true,
                PhoneNumber = "",
                TenantId = 1,
                PhoneNumberConfirmed = true,
                FirstName = "Waiter",
                LastName = "Waiter",
                SecurityStamp = Guid.NewGuid().ToString(),
                LockoutEnabled = true,
                TwoFactorEnabled = false,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsDeleted = false,
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                AccessFailedCount = 0,
            };
            
            var waiter2 = new User
            {
                Id = 9,
                UserName = "Waiter2@nttdata.com",
                NormalizedUserName = "WAITER2@NTTDATA.COM",
                Email = "Waiter2@nttdata.com",
                NormalizedEmail = "WAITER2@NTTDATA.COM",
                EmailConfirmed = true,
                PhoneNumber = "",
                TenantId = 2,
                PhoneNumberConfirmed = true,
                FirstName = "Waiter",
                LastName = "Waiter",
                SecurityStamp = Guid.NewGuid().ToString(),
                LockoutEnabled = true,
                TwoFactorEnabled = false,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsDeleted = false,
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                AccessFailedCount = 0,
            };
            
            var waiter3 = new User
            {
                Id = 10,
                UserName = "Waiter3@nttdata.com",
                NormalizedUserName = "WAITER3@NTTDATA.COM",
                Email = "Waiter3@nttdata.com",
                NormalizedEmail = "WAITER3@NTTDATA.COM",
                EmailConfirmed = true,
                PhoneNumber = "",
                TenantId = 3,
                PhoneNumberConfirmed = true,
                FirstName = "Waiter",
                LastName = "Waiter",
                SecurityStamp = Guid.NewGuid().ToString(),
                LockoutEnabled = true,
                TwoFactorEnabled = false,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsDeleted = false,
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                AccessFailedCount = 0,
            };

            var customerUser = new User
            {
                Id = 11,
                UserName = "Customer@nttdata.com",
                NormalizedUserName = "CUSTOMER@NTTDATA.COM",
                Email = "Customer@nttdata.com",
                NormalizedEmail = "CUSTOMER@NTTDATA.COM",
                EmailConfirmed = true,
                PhoneNumber = "",
                PhoneNumberConfirmed = true,
                FirstName = "Customer",
                LastName = "Customer",
                SecurityStamp = Guid.NewGuid().ToString(),
                LockoutEnabled = true,
                TwoFactorEnabled = false,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsDeleted = false,
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                AccessFailedCount = 0,
            };
            
            var userList = new List<User>
            {
                superAdmin,
                restaurantOwner1,
                restaurantOwner2,
                restaurantOwner3,
                kitchenPersonnel1,
                kitchenPersonnel2,
                kitchenPersonnel3,
                waiter1,
                waiter2,
                waiter3,
                customerUser
            };
            
            userList.ForEach(user =>
                user.PasswordHash = CreatePasswordHash(user, "123NttData-_-")
            );
            
            builder.Entity<User>().HasData(userList);
        }

        private static void SeedTablesOfRestaurant(ModelBuilder builder)
        {
            builder.Entity<TableOfRestaurant>().HasData(
                new TableOfRestaurant
                {
                    Id = 1,
                    RestaurantId = 1,
                    TableStatusId = 1
                },
                new TableOfRestaurant
                {
                    Id = 2,
                    RestaurantId = 1,
                    TableStatusId = 1
                },
                new TableOfRestaurant
                {
                    Id = 3,
                    RestaurantId = 1,
                    TableStatusId = 1
                },
                new TableOfRestaurant
                {
                    Id = 4,
                    RestaurantId = 2,
                    TableStatusId = 1
                },
                new TableOfRestaurant
                {
                    Id = 5,
                    RestaurantId = 2,
                    TableStatusId = 1
                },
                new TableOfRestaurant
                {
                    Id = 6,
                    RestaurantId = 2,
                    TableStatusId = 1
                },
                new TableOfRestaurant
                {
                    Id = 7,
                    RestaurantId = 3,
                    TableStatusId = 1
                },
                new TableOfRestaurant
                {
                    Id = 8,
                    RestaurantId = 3,
                    TableStatusId = 1
                },
                new TableOfRestaurant
                {
                    Id = 9,
                    RestaurantId = 3,
                    TableStatusId = 1
                }
            );
        }

        private static void SeedCategories(ModelBuilder builder)
        {
            builder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    Name = "Breakfast",
                },
                new Category
                {
                    Id = 2,
                    Name = "Fish",
                },
                new Category
                {
                    Id = 3,
                    Name = "Soups",
                },
                new Category
                {
                    Id = 4,
                    Name = "Meat Dishes",
                },
                new Category
                {
                    Id = 5,
                    Name = "Salads",
                },
                new Category
                {
                    Id = 6,
                    Name = "Desserts",
                },
                new Category
                {
                    Id = 7,
                    Name = "Drinks",
                },
                new Category
                {
                    Id = 8,
                    Name = "Snacks",
                }
            );
        }

        private static void SeedRestaurants(ModelBuilder builder)
        {
            builder.Entity<Restaurant>().HasData(
                new Restaurant
                {
                    Id = 1,
                    Name = "Happy Moons",
                },
                new Restaurant
                {
                    Id = 2,
                    Name = "Cadıköy",
                },
                new Restaurant
                {
                    Id = 3,
                    Name = "Kimyon",
                }
            );
        }

        private static void SeedIngredients(ModelBuilder builder)
        {
            builder.Entity<Ingredient>().HasData(
                new Ingredient
                {
                    Id = 1,
                    Name = "Eggs",
                },
                new Ingredient
                {
                    Id = 2,
                    Name = "Wine",
                },
                new Ingredient
                {
                    Id = 3,
                    Name = "Chicken",
                },
                new Ingredient
                {
                    Id = 4,
                    Name = "Tomatoes",
                },
                new Ingredient
                {
                    Id = 5,
                    Name = "Tuna",
                },
                new Ingredient
                {
                    Id = 6,
                    Name = "Milk",
                },
                new Ingredient
                {
                    Id = 7,
                    Name = "Onions",
                }
            );
        }
        private static string CreatePasswordHash(User user, string password)
        {
            var passwordHasher = new PasswordHasher<User>();
            return passwordHasher.HashPassword(user, password);
        }
    }
}
