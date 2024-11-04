using System.Net.Mail;
using FluentAssertions;
using WebApi.Application.OrderOperations.UpdateOrder;
using WebApi.Entities;

namespace WebApi.UnitTests.Application.OrderOperations.UpdateOrder;

public class UpdateOrderCommandValidatorTests
{
    [Theory]
    [InlineData(1, 1, 0, 2, Status.NewOrder)]
    [InlineData(1, 1, 1, 0, Status.NewOrder)]
    public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int foodIds, int quantities, int orderId, int userId, Status status)
    {
        UpdateOrderCommand command = new UpdateOrderCommand(null);
        command.Model = new UpdateOrderModel() { FoodIds = new List<int>() { foodIds }, Quantities = new List<int>() { quantities }, OrderStatus = status };
        command.UserId = userId;
        command.OrderId = orderId;

        UpdateOrderCommandValidator validator = new UpdateOrderCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
    {
        UpdateOrderCommand command = new UpdateOrderCommand(null);
        command.Model = new UpdateOrderModel() { FoodIds = [1], Quantities = [2], OrderStatus = Status.ConfirmedByRestaurant };
        command.UserId = 2;
        command.OrderId = 2;

        UpdateOrderCommandValidator validator = new UpdateOrderCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().Be(0);
    }

}