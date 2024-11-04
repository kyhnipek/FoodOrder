using System.Net.Mail;
using FluentAssertions;
using WebApi.Application.CourierOperations.CreateCourier;

namespace WebApi.UnitTests.Application.CourierOperations.CreateCourier;

public class CreateCourierCommandValidatorTests
{
    [Theory]
    [InlineData("t", "t", Cities.Bandırma, States.Balikesir, "mail@mail.com", "123456")]
    [InlineData("t", "te", Cities.Bandırma, States.Balikesir, "mail@mail.com", "123456")]
    [InlineData("te", "t", Cities.Bandırma, States.Balikesir, "mail@mail.com", "123456")]
    [InlineData("te", "te", Cities.Bandırma, States.Balikesir, "mail", "123456")]
    [InlineData("te", "te", Cities.Bandırma, States.Balikesir, "mail@mail.com", "12345")]

    public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string name, string surname, Cities city, States state, string email, string password)
    {
        CreateCourierCommand command = new CreateCourierCommand(null);
        command.Model = new CreateCourierModel() { Name = name, Surname = surname, City = city, State = state, Email = email, Password = password };

        CreateCourierCommandValidator validator = new CreateCourierCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().BeGreaterThan(0);
    }
    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
    {
        CreateCourierCommand command = new CreateCourierCommand(null);
        command.Model = new CreateCourierModel() { Name = "testName", Surname = "testSurname", City = Cities.Bandırma, State = States.Balikesir, Email = "testmail@mail.com", Password = "123456" };

        CreateCourierCommandValidator validator = new CreateCourierCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().Be(0);
    }

}