using FluentAssertions;
using WebApi.Application.RestaurantOperations.DeleteRestaurant;
using WebApi.DBOperations;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.RestaurantOperations.DeleteRestaurant;

public class DeleteRestaurantCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly IFoodOrderDBContext _context;

    public DeleteRestaurantCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
    }

    [Fact]
    public void WhenRestaurantNotExist_InvalidOperationException_ShouldBeReturn()
    {
        DeleteRestaurantCommand command = new DeleteRestaurantCommand(_context);
        command.RestaurantId = 999;

        FluentActions.Invoking(() => command.Handle())
                                .Should().Throw<InvalidOperationException>()
                                .And.Message.Should().Be("Restoran bulunamadı.");
    }

    [Fact]
    public void WhenValidInputIsGiven_Restaurant_ShouldBeDeleted()
    {
        DeleteRestaurantCommand command = new DeleteRestaurantCommand(_context);
        command.RestaurantId = 3;

        FluentActions.Invoking(() => command.Handle()).Invoke();
        var restaurant = _context.Restaurants.Where(x => x.IsActive).FirstOrDefault(x => x.Id == command.RestaurantId);

        restaurant.Should().BeNull();
    }
}