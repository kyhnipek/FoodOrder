using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebApi.Application.OrderOperations.CreateOrder;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.Services;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.OrderOperations.CreateOrder;

public class CreateOrderCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly IFoodOrderDBContext _context;
    private readonly IMapper _mapper;

    public CreateOrderCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenValidInputsAreGiven_Order_ShouldBeCreated()
    {
        CreateOrderModel model = new CreateOrderModel()
        {
            FoodIds = [2],
            Quantities = [2],
            RestaurantId = 1,
            CourierId = 1,
        };
        CreateOrderCommand command = new CreateOrderCommand(_context, _mapper);
        command.Model = model;
        command.UserId = 5;

        FluentActions.Invoking(() => command.Handle()).Invoke();
        var order = _context.Orders.FirstOrDefault(r => r.UserId == command.UserId && r.RestaurantId == model.RestaurantId);

        order.Should().NotBeNull();
        order.CourierId.Should().Be(model.CourierId);
        order.UserId.Should().Be(command.UserId);
    }


}