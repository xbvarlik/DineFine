using DineFine.DataObjects.Entities;
using Microsoft.EntityFrameworkCore;

namespace DineFine.Accessor.DataAccessors.Mssql;

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
        SeedUser(builder);
    }

    private static void SeedUser(ModelBuilder builder)
    {
        // Add TenantIds to Users
        builder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                UserName = "SuperAdminUser",
                FirstName = "SuperAdmin",
                LastName = "User",
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsDeleted = false
            },
            new User
            {
                Id = 2,
                UserName = "RestaurantOwnerUser",
                FirstName = "RestaurantOwner",
                LastName = "User",
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsDeleted = false
            },
            new User
            {
                Id = 3,
                UserName = "KitchenPersonnelrUser",
                FirstName = "KitchenPersonnel",
                LastName = "User",
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsDeleted = false
            },
            new User
            {
                Id = 4,
                UserName = "WaiterUser",
                FirstName = "Waiter",
                LastName = "User",
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsDeleted = false
            },
            new User
            {
                Id = 5,
                UserName = "CustomerUser",
                FirstName = "Customer",
                LastName = "User",
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsDeleted = false
            }
        );
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
}