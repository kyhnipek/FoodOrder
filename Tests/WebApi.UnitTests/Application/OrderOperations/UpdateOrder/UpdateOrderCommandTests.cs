using AutoMapper;
using FluentAssertions;
using WebApi.Application.OrderOperations.UpdateOrder;
using WebApi.DBOperations;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.OrderOperations.UpdateOrder;

public class UpdateOrderCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly IFoodOrderDBContext _context;

    public UpdateOrderCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
    }

    [Fact]
    public void WhenOrderIsNotExist_InvalidOperationException_ShouldBeReturned()
    {
        UpdateOrderCommand command = new UpdateOrderCommand(_context);
        command.OrderId = 555;
        command.Model = new UpdateOrderModel() { FoodIds = [1, 2], OrderStatus = Status.ConfirmedByRestaurant, Quantities = [2, 5] };

        FluentActions.Invoking(() => command.Handle())
                                        .Should().Throw<InvalidOperationException>()
                                        .And.Message.Should().Be("Sipariş bulunamadı.");
    }

    [Fact]
    public void WhenValidInputsAreGiven_Order_ShouldBeUpdated()
    {
        UpdateOrderModel model = new UpdateOrderModel() { FoodIds = [1, 2], OrderStatus = Status.ConfirmedByRestaurant, Quantities = [2, 5] };
        UpdateOrderCommand command = new UpdateOrderCommand(_context);
        command.OrderId = 1;
        command.Model = model;
        command.UserRole = "Admin";

        FluentActions.Invoking(() => command.Handle()).Invoke();

        var order = _context.Orders.FirstOrDefault(x => x.Id == command.OrderId);

        order.Should().NotBeNull();
        order.OrderStatus.Should().Be(model.OrderStatus);
    }
}