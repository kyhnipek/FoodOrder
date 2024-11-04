using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WebApi.Application.RestaurantOperations.CreateRestaurant;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.Services;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.RestaurantOperations.CreateRestaurant;

public class CreateRestaurantCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly IFoodOrderDBContext _context;
    private readonly IMapper _mapper;

    public CreateRestaurantCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenRestaurantIsExist_InvalidOperationException_ShouldBeReturn()
    {
        var restaurant = new Restaurant()
        {
            Title = "TestRestaurant",
            Adress = "testAdress",
            City = Cities.Bandırma,
            State = States.Balikesir,
            UserId = 1,
        };
        _context.Restaurants.Add(restaurant);
        _context.SaveChanges();

        CreateRestaurantCommand command = new CreateRestaurantCommand(_context, _mapper);
        command.Model = new CreateRestaurantModel() { Title = restaurant.Title, Adress = restaurant.Adress, City = restaurant.City, State = restaurant.State, UserId = restaurant.UserId };

        FluentActions.Invoking(() => command.Handle())
                            .Should().Throw<InvalidOperationException>()
                            .And.Message.Should().Be("Restoran zaten mevcut.");
    }

    [Fact]
    public void WhenValidInputsAreGiven_Restaurant_ShouldBeCreated()
    {
        CreateRestaurantModel model = new CreateRestaurantModel()
        {
            Title = "TestRestaurant2",
            Adress = "testAdress2",
            City = Cities.Bandırma,
            State = States.Balikesir,
            UserId = 1,
        };
        CreateRestaurantCommand command = new CreateRestaurantCommand(_context, _mapper);
        command.Model = model;

        FluentActions.Invoking(() => command.Handle()).Invoke();
        var restaurant = _context.Restaurants.FirstOrDefault(r => r.Title == model.Title & r.UserId == model.UserId);

        restaurant.Should().NotBeNull();
        restaurant.Title.Should().Be(model.Title);
        restaurant.Adress.Should().Be(model.Adress);
        restaurant.City.Should().Be(model.City);
        restaurant.State.Should().Be(model.State);
        restaurant.UserId.Should().Be(model.UserId);
    }


}