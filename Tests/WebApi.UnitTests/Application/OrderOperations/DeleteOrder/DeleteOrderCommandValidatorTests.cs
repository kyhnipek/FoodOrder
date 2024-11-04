using FluentAssertions;
using WebApi.Application.OrderOperations.DeleteOrder;

namespace WebApi.UnitTests.Application.OrderOperations.DeleteOrder;

public class DeleteOrderCommandValidatorTests
{
    [Fact]
    public void WhenInvalidInputIsGiven_Validator_ShouldBeReturnErrors()
    {
        DeleteOrderCommand command = new DeleteOrderCommand(null);
        command.OrderId = 0;

        DeleteOrderCommandValidator validator = new DeleteOrderCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
    {
        DeleteOrderCommand command = new DeleteOrderCommand(null);
        command.OrderId = 1;

        DeleteOrderCommandValidator validator = new DeleteOrderCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().Be(0);
    }

}