using FluentAssertions;
using WebApi.Application.FoodOperations.DeleteFood;

namespace WebApi.UnitTests.Application.FoodOperations.DeleteFood;

public class DeleteFoodCommandValidatorTests
{
    [Fact]
    public void WhenInvalidInputIsGiven_Validator_ShouldBeReturnErrors()
    {
        DeleteFoodCommand command = new DeleteFoodCommand(null);
        command.FoodId = 0;

        DeleteFoodCommandValidator validator = new DeleteFoodCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
    {
        DeleteFoodCommand command = new DeleteFoodCommand(null);
        command.FoodId = 1;

        DeleteFoodCommandValidator validator = new DeleteFoodCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().Be(0);
    }

}