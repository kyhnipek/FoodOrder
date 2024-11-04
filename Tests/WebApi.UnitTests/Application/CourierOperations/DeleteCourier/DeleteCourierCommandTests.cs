using FluentAssertions;
using WebApi.Application.CourierOperations.DeleteCourier;
using WebApi.DBOperations;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.CourierOperations.DeleteCourier;

public class DeleteCourierCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly IFoodOrderDBContext _context;

    public DeleteCourierCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
    }

    [Fact]
    public void WhenCourierNotExist_InvalidOperationException_ShouldBeReturn()
    {
        DeleteCourierCommand command = new DeleteCourierCommand(_context);
        command.CourierId = 999;

        FluentActions.Invoking(() => command.Handle())
                                .Should().Throw<InvalidOperationException>()
                                .And.Message.Should().Be("Kurye bulunamadı.");
    }

    [Fact]
    public void WhenValidInputIsGiven_Courier_ShouldBeDeleted()
    {
        DeleteCourierCommand command = new DeleteCourierCommand(_context);
        command.CourierId = 3;

        FluentActions.Invoking(() => command.Handle()).Invoke();
        var courier = _context.Couriers.Where(x => x.IsActive).FirstOrDefault(x => x.Id == command.CourierId);

        courier.Should().BeNull();
    }
}