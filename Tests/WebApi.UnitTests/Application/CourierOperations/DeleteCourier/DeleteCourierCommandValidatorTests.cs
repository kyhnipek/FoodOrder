using FluentAssertions;
using WebApi.Application.CourierOperations.DeleteCourier;

namespace WebApi.UnitTests.Application.CourierOperations.DeleteCourier;


public class DeleteCourierCommandValidatorTests
{
    [Fact]
    public void WhenInvalidInputIsGiven_Validator_ShouldBeReturnErrors()
    {
        DeleteCourierCommand command = new DeleteCourierCommand(null);
        command.CourierId = 0;

        DeleteCourierCommandValidator validator = new DeleteCourierCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
    {
        DeleteCourierCommand command = new DeleteCourierCommand(null);
        command.CourierId = 1;

        DeleteCourierCommandValidator validator = new DeleteCourierCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().Be(0);
    }

}