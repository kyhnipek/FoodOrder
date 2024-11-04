using System.Net.Mail;
using FluentAssertions;
using WebApi.Application.OrderOperations.CreateOrder;
using WebApi.Entities;

namespace WebApi.UnitTests.Application.OrderOperations.CreateOrder;

public class CreateOrderCommandValidatorTests
{
    [Theory]
    [InlineData(0, 1, 1, 2, 2)]
    [InlineData(1, 0, 1, 2, 2)]
    [InlineData(1, 1, 1, 2, 0)]
    [InlineData(1, 0, 1, 2, 0)]
    public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int userId, int restaurantId, int foodIds, int quantities, int courierId)
    {
        CreateOrderCommand command = new CreateOrderCommand(null, null);
        command.Model = new CreateOrderModel() { RestaurantId = restaurantId, FoodIds = new List<int>() { foodIds }, Quantities = new List<int>() { quantities }, CourierId = courierId };
        command.UserId = userId;

        CreateOrderCommandValidator validator = new CreateOrderCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
    {
        CreateOrderCommand command = new CreateOrderCommand(null, null);
        command.Model = new CreateOrderModel() { RestaurantId = 1, FoodIds = [1], Quantities = [2], CourierId = 2 };
        command.UserId = 2;

        CreateOrderCommandValidator validator = new CreateOrderCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().Be(0);
    }

}