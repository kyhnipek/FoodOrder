using System.Net.Mail;
using FluentAssertions;
using WebApi.Application.RestaurantOperations.CreateRestaurant;

namespace WebApi.UnitTests.Application.RestaurantOperations.CreateRestaurant;

public class CreateRestaurantCommandValidatorTests
{
    [Theory]
    [InlineData("t", "t", Cities.Bandırma, Type.Bakery, States.Balikesir, 1)]
    [InlineData("t", "te", Cities.Bandırma, Type.Bakery, States.Balikesir, 2)]
    [InlineData("te", "t", Cities.Bandırma, Type.Bakery, States.Balikesir, 0)]
    [InlineData("t", "te", Cities.Bandırma, Type.Bakery, States.Balikesir, 0)]
    [InlineData("te", "te", Cities.Bandırma, Type.Bakery, States.Balikesir, 0)]

    public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string title, string adress, Cities city, Type type, States state, int userId)
    {
        CreateRestaurantCommand command = new CreateRestaurantCommand(null, null);
        command.Model = new CreateRestaurantModel() { Title = title, Adress = adress, Type = type, City = city, State = state, UserId = userId };

        CreateRestaurantCommandValidator validator = new CreateRestaurantCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().BeGreaterThan(0);
    }
    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
    {
        CreateRestaurantCommand command = new CreateRestaurantCommand(null, null);
        command.Model = new CreateRestaurantModel() { Title = "test", Adress = "test", City = Cities.Bandırma, State = States.Balikesir, UserId = 2 };

        CreateRestaurantCommandValidator validator = new CreateRestaurantCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().Be(0);
    }

}