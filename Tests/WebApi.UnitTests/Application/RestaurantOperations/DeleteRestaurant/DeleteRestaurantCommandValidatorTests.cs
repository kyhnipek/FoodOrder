using FluentAssertions;
using WebApi.Application.RestaurantOperations.DeleteRestaurant;

namespace WebApi.UnitTests.Application.RestaurantOperations.DeleteRestaurant;

public class DeleteRestaurantCommandValidatorTests
{
    [Fact]
    public void WhenInvalidInputIsGiven_Validator_ShouldBeReturnErrors()
    {
        DeleteRestaurantCommand command = new DeleteRestaurantCommand(null);
        command.RestaurantId = 0;

        DeleteRestaurantCommandValidator validator = new DeleteRestaurantCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
    {
        DeleteRestaurantCommand command = new DeleteRestaurantCommand(null);
        command.RestaurantId = 1;

        DeleteRestaurantCommandValidator validator = new DeleteRestaurantCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().Be(0);
    }

}