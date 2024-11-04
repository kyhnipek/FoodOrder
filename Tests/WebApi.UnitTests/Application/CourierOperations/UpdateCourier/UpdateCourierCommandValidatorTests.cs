using System.Net.Mail;
using FluentAssertions;
using WebApi.Application.CourierOperations.UpdateCourier;

namespace WebApi.UnitTests.Application.CourierOperations.UpdateCourier;

public class UpdateCourierCommandValidatorTests
{
    [Fact]
    public void WhenInvalidInputIsGiven_Validator_ShouldBeReturnError()
    {
        UpdateCourierCommand command = new UpdateCourierCommand(null);
        command.Model = new UpdateCourierModel() { RestoranIds = [1] };
        command.CourierId = 0;

        UpdateCourierCommandValidator validator = new UpdateCourierCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().BeGreaterThan(0);
    }
    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
    {
        UpdateCourierCommand command = new UpdateCourierCommand(null);
        command.Model = new UpdateCourierModel() { RestoranIds = [1] };
        command.CourierId = 1;

        UpdateCourierCommandValidator validator = new UpdateCourierCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().Be(0);
    }

}