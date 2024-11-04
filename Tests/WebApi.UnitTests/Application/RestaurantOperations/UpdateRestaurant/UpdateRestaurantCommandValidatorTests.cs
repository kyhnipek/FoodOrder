using System.Net.Mail;
using FluentAssertions;
using WebApi.Application.RestaurantOperations.UpdateRestaurant;

namespace WebApi.UnitTests.Application.RestaurantOperations.UpdateRestaurant;

public class UpdateRestaurantCommandValidatorTests
{
    [Theory]
    [InlineData("t", "t", Cities.Bandırma, Type.Bakery, States.Balikesir, 1, 1)]
    [InlineData("t", "te", Cities.Bandırma, Type.Bakery, States.Balikesir, 2, 1)]
    [InlineData("te", "t", Cities.Bandırma, Type.Bakery, States.Balikesir, 0, 1)]
    [InlineData("t", "te", Cities.Bandırma, Type.Bakery, States.Balikesir, 0, 1)]
    [InlineData("te", "te", Cities.Bandırma, Type.Bakery, States.Balikesir, 0, 1)]
    [InlineData("te", "te", Cities.Bandırma, Type.Bakery, States.Balikesir, 1, 0)]

    public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string title, string adress, Cities city, Type type, States state, int userId, int restaurantId)
    {
        UpdateRestaurantCommand command = new UpdateRestaurantCommand(null, null);
        command.Model = new UpdateRestaurantModel() { Title = title, Adress = adress, Type = type, City = city, State = state, UserId = userId };
        command.RestaurantId = restaurantId;

        UpdateRestaurantCommandValidator validator = new UpdateRestaurantCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().BeGreaterThan(0);
    }
    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
    {
        UpdateRestaurantCommand command = new UpdateRestaurantCommand(null, null);
        command.Model = new UpdateRestaurantModel() { Title = "test", Adress = "testsdfadsfadsafad", City = Cities.Bandırma, State = States.Balikesir, UserId = 2 };
        command.RestaurantId = 1;

        UpdateRestaurantCommandValidator validator = new UpdateRestaurantCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().Be(0);
    }

}