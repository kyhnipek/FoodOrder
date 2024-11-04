using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WebApi.Application.FoodOperations.CreateFood;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.FoodOperations.CreateFood;

public class CreateFoodCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly IFoodOrderDBContext _context;
    private readonly IMapper _mapper;

    public CreateFoodCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenFoodIsExist_InvalidOperationException_ShouldBeReturn()
    {
        var food = new Food()
        {
            Title = "TestFood",
            Description = "TestFoodDescription",
            ImgUrl = "www.TestFoodUrl.com/food.jpg",
            Price = 1,
            RestaurantId = 1
        };
        _context.Foods.Add(food);
        _context.SaveChanges();

        CreateFoodCommand command = new CreateFoodCommand(_context, _mapper);
        command.Model = new CreateFoodModel() { Title = food.Title, Description = food.Description, ImgUrl = food.ImgUrl, Price = food.Price, RestaurantId = food.RestaurantId };
        command.UserRole = "Admin";

        FluentActions.Invoking(() => command.Handle())
                            .Should().Throw<InvalidOperationException>()
                            .And.Message.Should().Be("Yiyecek restoranda zaten mevcut.");
    }

    [Fact]
    public void WhenValidInputsAreGiven_Food_ShouldBeCreated()
    {
        CreateFoodModel model = new CreateFoodModel()
        {
            Title = "TestFood2",
            Description = "TestFoodDescription2",
            ImgUrl = "www.TestFoodUrl2.com/food2.jpg",
            Price = 1,
            RestaurantId = 1
        };
        CreateFoodCommand command = new CreateFoodCommand(_context, _mapper);
        command.Model = model;
        command.UserRole = "Admin";

        FluentActions.Invoking(() => command.Handle()).Invoke();
        var food = _context.Foods.FirstOrDefault(f => f.Title == model.Title && f.RestaurantId == model.RestaurantId);

        food.Should().NotBeNull();
        food.Title.Should().Be(model.Title);
        food.Description.Should().Be(model.Description);
        food.ImgUrl.Should().Be(model.ImgUrl);
        food.Price.Should().Be(model.Price);
        food.RestaurantId.Should().Be(model.RestaurantId);

    }


}