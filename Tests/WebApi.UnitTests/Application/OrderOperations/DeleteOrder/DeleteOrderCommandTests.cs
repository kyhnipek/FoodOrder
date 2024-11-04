using FluentAssertions;
using WebApi.Application.OrderOperations.DeleteOrder;
using WebApi.DBOperations;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.OrderOperations.DeleteOrder;

public class DeleteOrderCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly IFoodOrderDBContext _context;

    public DeleteOrderCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
    }

    [Fact]
    public void WhenOrderNotExist_InvalidOperationException_ShouldBeReturn()
    {
        DeleteOrderCommand command = new DeleteOrderCommand(_context);
        command.OrderId = 999;

        FluentActions.Invoking(() => command.Handle())
                                .Should().Throw<InvalidOperationException>()
                                .And.Message.Should().Be("Sipariş bulunamadı.");
    }

    [Fact]
    public void WhenValidInputIsGiven_Order_ShouldBeDeleted()
    {
        DeleteOrderCommand command = new DeleteOrderCommand(_context);
        command.OrderId = 3;

        FluentActions.Invoking(() => command.Handle()).Invoke();
        var order = _context.Orders.Where(x => x.IsActive).FirstOrDefault(x => x.Id == command.OrderId);

        order.Should().BeNull();
    }
}