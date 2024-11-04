using System.Net.Mail;
using FluentAssertions;
using WebApi.Application.FoodOperations.UpdateFood;

namespace WebApi.UnitTests.Application.FoodOperations.UpdateFood;

public class UpdateFoodCommandValidatorTests
{
    [Theory]
    [InlineData("t", "t", "t", 100, 1)]
    [InlineData("te", "t", "t", 100, 1)]
    [InlineData("t", "te", "t", 100, 1)]
    [InlineData("te", "te", "t", 100, 0)]

    public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string title, string description, string imgUrl, decimal price, int foodId)
    {
        UpdateFoodCommand command = new UpdateFoodCommand(null, null);
        command.Model = new UpdateFoodModel() { Title = title, Description = description, ImgUrl = imgUrl, Price = price };
        command.FoodId = foodId;

        UpdateFoodCommandValidator validator = new UpdateFoodCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().BeGreaterThan(0);
    }
    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
    {
        UpdateFoodCommand command = new UpdateFoodCommand(null, null);
        command.Model = new UpdateFoodModel() { Title = "test", Description = "tester", ImgUrl = "tester", Price = 100 };
        command.FoodId = 1;

        UpdateFoodCommandValidator validator = new UpdateFoodCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().Be(0);
    }

}