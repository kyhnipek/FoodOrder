using AutoMapper;
using FluentAssertions;
using WebApi.Application.CourierOperations.UpdateCourier;
using WebApi.DBOperations;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.CourierOperations.UpdateCourier;

public class UpdateCourierCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly IFoodOrderDBContext _context;

    public UpdateCourierCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
    }

    [Fact]
    public void WhenCourierIsNotExist_InvalidOperationException_ShouldBeReturned()
    {
        UpdateCourierCommand command = new UpdateCourierCommand(_context);
        command.CourierId = 555;
        command.Model = new UpdateCourierModel() { RestoranIds = [1] };

        FluentActions.Invoking(() => command.Handle())
                                        .Should().Throw<InvalidOperationException>()
                                        .And.Message.Should().Be("Kurye bulunamadı.");
    }

    [Fact]
    public void WhenValidInputsAreGiven_Courier_ShouldBeUpdated()
    {
        UpdateCourierModel model = new UpdateCourierModel() { RestoranIds = [2], };
        UpdateCourierCommand command = new UpdateCourierCommand(_context);
        command.CourierId = 1;
        command.Model = model;

        FluentActions.Invoking(() => command.Handle()).Invoke();

        var courier = _context.Couriers.FirstOrDefault(x => x.Id == command.CourierId);

        courier.Should().NotBeNull();
    }
}