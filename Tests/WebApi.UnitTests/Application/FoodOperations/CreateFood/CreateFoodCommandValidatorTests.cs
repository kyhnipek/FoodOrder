using System.Net.Mail;
using FluentAssertions;
using WebApi.Application.FoodOperations.CreateFood;

namespace WebApi.UnitTests.Application.FoodOperations.CreateFood;

public class CreateFoodCommandValidatorTests
{
    [Theory]
    [InlineData("t", "t", "t", 100, 1)]
    [InlineData("te", "t", "t", 100, 1)]
    [InlineData("t", "te", "t", 100, 1)]
    [InlineData("te", "te", "t", 100, 0)]

    public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string title, string description, string imgUrl, decimal price, int restaurantId)
    {
        CreateFoodCommand command = new CreateFoodCommand(null, null);
        command.Model = new CreateFoodModel() { Title = title, Description = description, ImgUrl = imgUrl, Price = price, RestaurantId = restaurantId };

        CreateFoodCommandValidator validator = new CreateFoodCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().BeGreaterThan(0);
    }
    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
    {
        CreateFoodCommand command = new CreateFoodCommand(null, null);
        command.Model = new CreateFoodModel() { Title = "test", Description = "test", ImgUrl = "test", Price = 100, RestaurantId = 1 };

        CreateFoodCommandValidator validator = new CreateFoodCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().Be(0);
    }

}