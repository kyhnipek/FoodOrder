using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.UnitTests.TestSetup;

public static class DataGenerator
{
    public static void AddFoods(this FoodOrderDBContext context)
    {
        context.Foods.AddRange(
            new Food { Id = 1, Title = "Lahmacun", Price = 50, RestaurantId = 1 },
            new Food { Id = 2, Title = "Kuşbaşı Kaşarlı Pide", Price = 200, RestaurantId = 1 },
            new Food { Id = 3, Title = "Karışık Pizza", Price = 250, RestaurantId = 2 },
            new Food { Id = 4, Title = "Texas Smokehouse Menü", Price = 275, RestaurantId = 3 }
        );
        context.SaveChanges();
    }
    public static void AddRestaurants(this FoodOrderDBContext context)
    {
        context.Restaurants.AddRange(
            new Restaurant { Id = 1, Title = "Konya Pide Salonu", State = States.Balikesir, City = Cities.Bandırma, Type = Type.Kebap, UserId = 3 },
            new Restaurant { Id = 2, Title = "Little Cheasers Pizza", State = States.Balikesir, City = Cities.Bandırma, Type = Type.Pizzeria, UserId = 6 },
            new Restaurant { Id = 3, Title = "Burger King", State = States.Balikesir, City = Cities.Bandırma, Type = Type.FastFood, UserId = 7 }
        );
        context.SaveChanges();
    }
    public static void AddUsers(this FoodOrderDBContext context)
    {
        context.Users.AddRange(
                new User
                {
                    Id = 1,
                    Name = "Kayhan",
                    Surname = "İpek",
                    City = Cities.Bandırma,
                    State = States.Balikesir,
                    Email = "admin@mail.com",
                    PasswordHash = "46bta141eEl63jNWiZs71AlTpUkp4JKO7clubUo7dwU=",// Password = "123456",
                    PasswordSalt = "RdIfrSdtNLjMz7r3Rr6PctzLgwL0Zra0kSUd85/0sa8=",

                    Role = Role.Admin
                },
                new User
                {
                    Id = 2,
                    Name = "Ali",
                    Surname = "Veli",
                    City = Cities.Bandırma,
                    State = States.Balikesir,
                    Email = "customer@mail.com",
                    PasswordHash = "46bta141eEl63jNWiZs71AlTpUkp4JKO7clubUo7dwU=",// Password = "123456",
                    PasswordSalt = "RdIfrSdtNLjMz7r3Rr6PctzLgwL0Zra0kSUd85/0sa8=",
                    Role = Role.Customer
                },
                new User
                {
                    Id = 3,
                    Name = "Konya",
                    Surname = "Pide",
                    City = Cities.Bandırma,
                    State = States.Balikesir,
                    Email = "konyapide@mail.com",
                    PasswordHash = "46bta141eEl63jNWiZs71AlTpUkp4JKO7clubUo7dwU=",// Password = "123456",
                    PasswordSalt = "RdIfrSdtNLjMz7r3Rr6PctzLgwL0Zra0kSUd85/0sa8=",
                    Role = Role.Restaurant
                },
                new User
                {
                    Id = 4,
                    Name = "Mahsun",
                    Surname = "J.",
                    City = Cities.Bandırma,
                    State = States.Balikesir,
                    Email = "mahsunj@mail.com",
                    PasswordHash = "46bta141eEl63jNWiZs71AlTpUkp4JKO7clubUo7dwU=",// Password = "123456",
                    PasswordSalt = "RdIfrSdtNLjMz7r3Rr6PctzLgwL0Zra0kSUd85/0sa8=",
                    Role = Role.Courier
                },
                new User
                {
                    Id = 5,
                    Name = "Recep",
                    Surname = "İvedik",
                    City = Cities.Bandırma,
                    State = States.Balikesir,
                    Email = "recepivedik_95-@mail.com",
                    PasswordHash = "46bta141eEl63jNWiZs71AlTpUkp4JKO7clubUo7dwU=",// Password = "123456",
                    PasswordSalt = "RdIfrSdtNLjMz7r3Rr6PctzLgwL0Zra0kSUd85/0sa8=",
                    Role = Role.Courier
                },
                new User
                {
                    Id = 6,
                    Name = "Little Cheasers",
                    Surname = "Pizza",
                    City = Cities.Bandırma,
                    State = States.Balikesir,
                    Email = "lcp@mail.com",
                    PasswordHash = "46bta141eEl63jNWiZs71AlTpUkp4JKO7clubUo7dwU=",// Password = "123456",
                    PasswordSalt = "RdIfrSdtNLjMz7r3Rr6PctzLgwL0Zra0kSUd85/0sa8=",
                    Role = Role.Restaurant
                },
                new User
                {
                    Id = 7,
                    Name = "Burger",
                    Surname = "King",
                    City = Cities.Bandırma,
                    State = States.Balikesir,
                    Email = "bk@mail.com",
                    PasswordHash = "46bta141eEl63jNWiZs71AlTpUkp4JKO7clubUo7dwU=",// Password = "123456",
                    PasswordSalt = "RdIfrSdtNLjMz7r3Rr6PctzLgwL0Zra0kSUd85/0sa8=",
                    Role = Role.Restaurant
                },
                new User
                {
                    Id = 8,
                    Name = "Jason",
                    Surname = "Statham",
                    City = Cities.Bandırma,
                    State = States.Balikesir,
                    Email = "js@mail.com",
                    PasswordHash = "46bta141eEl63jNWiZs71AlTpUkp4JKO7clubUo7dwU=",// Password = "123456",
                    PasswordSalt = "RdIfrSdtNLjMz7r3Rr6PctzLgwL0Zra0kSUd85/0sa8=",
                    Role = Role.Courier
                }
            );
        context.SaveChanges();
    }
    public static void AddCouriers(this FoodOrderDBContext context)
    {
        var rest = context.Restaurants.ToDictionary(r => r.Id);
        context.Couriers.AddRange(
                new Courier
                {
                    Id = 1,
                    UserId = 4,
                    Restaurants = { rest[1] }
                },
                new Courier
                {
                    Id = 2,
                    UserId = 5,
                    Restaurants = { rest[2] }
                },
                new Courier
                {
                    Id = 3,
                    UserId = 8,
                    Restaurants = { rest[3] }
                }
        );
        context.SaveChanges();
    }
    public static void AddQuantities(this FoodOrderDBContext context)
    {
        context.Quantities.AddRange(
            new Quantity()
            {
                Id = 1,
                FoodId = 1,
                OrderId = 1,
                Quantities = 2
            },
            new Quantity()
            {
                Id = 2,
                FoodId = 2,
                OrderId = 1,
                Quantities = 3
            },
            new Quantity()
            {
                Id = 3,
                FoodId = 3,
                OrderId = 2,
                Quantities = 2
            },
            new Quantity()
            {
                Id = 4,
                FoodId = 4,
                OrderId = 3,
                Quantities = 3
            }
        );
        context.SaveChanges();

    }
    public static void AddOrders(this FoodOrderDBContext context)
    {
        var food = context.Foods.Include(f => f.Quantities).ToDictionary(d => d.Id);
        var quantity = context.Quantities.ToDictionary(d => d.Id);
        context.Orders.AddRange(

            new Order
            {
                Id = 1,
                Foods = new List<Food>() { food[1], food[2] },
                RestaurantId = 1,
                OrderDate = new DateTime(2024, 10, 22),
                OrderTotal = 700,
                UserId = 2,
                CourierId = 1,
                OrderStatus = Status.Delivered
            },
            new Order
            {
                Id = 2,
                Foods = new List<Food>() { food[3] },
                RestaurantId = 2,
                OrderDate = new DateTime(2024, 10, 22),
                OrderTotal = 500,
                UserId = 2,
                CourierId = 2,
                OrderStatus = Status.ReceivedByCourier
            },
            new Order
            {
                Id = 3,
                Foods = new List<Food>() { food[4] },
                RestaurantId = 3,
                OrderDate = new DateTime(2024, 10, 22),
                OrderTotal = 825,
                UserId = 2,
                CourierId = 3,
                OrderStatus = Status.ConfirmedByRestaurant
            }

        );
        context.SaveChanges();
    }

}