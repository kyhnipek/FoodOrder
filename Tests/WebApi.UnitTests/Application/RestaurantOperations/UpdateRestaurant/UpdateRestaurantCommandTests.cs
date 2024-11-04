using AutoMapper;
using FluentAssertions;
using WebApi.Application.RestaurantOperations.UpdateRestaurant;
using WebApi.DBOperations;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.RestaurantOperations.UpdateRestaurant;

public class UpdateRestaurantCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly IFoodOrderDBContext _context;
    private readonly IMapper _mapper;

    public UpdateRestaurantCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenRestaurantIsNotExist_InvalidOperationException_ShouldBeReturned()
    {
        UpdateRestaurantCommand command = new UpdateRestaurantCommand(_context, _mapper);
        command.RestaurantId = 555;
        command.Model = new UpdateRestaurantModel() { Adress = "test", City = Cities.Bigadiç, State = States.Balikesir, Title = "test", UserId = 2 };

        FluentActions.Invoking(() => command.Handle())
                                        .Should().Throw<InvalidOperationException>()
                                        .And.Message.Should().Be("Restoran bulunamadı.");
    }

    [Fact]
    public void WhenValidInputsAreGiven_Restaurant_ShouldBeUpdated()
    {
        UpdateRestaurantModel model = new UpdateRestaurantModel() { Adress = "test", City = Cities.Bigadiç, State = States.Balikesir, Title = "test", UserId = 2 };
        UpdateRestaurantCommand command = new UpdateRestaurantCommand(_context, _mapper);
        command.RestaurantId = 1;
        command.Model = model;
        command.UserRole = "Admin";

        FluentActions.Invoking(() => command.Handle()).Invoke();

        var restaurant = _context.Restaurants.FirstOrDefault(x => x.Id == command.RestaurantId);

        restaurant.Should().NotBeNull();
        restaurant.Title.Should().Be(model.Title);
        restaurant.Adress.Should().Be(model.Adress);
        restaurant.City.Should().Be(model.City);
        restaurant.State.Should().Be(model.State);
        restaurant.UserId.Should().Be(model.UserId);
    }
}