using System.Configuration;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Common;
using WebApi.DBOperations;


namespace WebApi.UnitTests.TestSetup;

public class CommonTestFixture
{
    public FoodOrderDBContext Context { get; set; }
    public IMapper Mapper { get; set; }
    public CommonTestFixture()
    {
        var options = new DbContextOptionsBuilder<FoodOrderDBContext>()
                                .UseInMemoryDatabase(databaseName: $"FoodOrderTestDB_{Guid.NewGuid()}")
                                .Options;

        Context = new FoodOrderDBContext(options);
        Context.Database.EnsureCreated();

        Context.AddFoods();
        Context.AddRestaurants();
        Context.AddUsers();
        Context.AddCouriers();
        Context.AddQuantities();
        Context.AddOrders();

        Mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>()).CreateMapper();
    }
}