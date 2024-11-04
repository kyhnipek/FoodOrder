using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WebApi.Application.OrderOperations.GetOrderDetail;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.OrderOperations.GetOrderDetail;

public class GetOrderDetailQueryTests : IClassFixture<CommonTestFixture>
{
    private readonly IFoodOrderDBContext _context;
    private readonly IMapper _mapper;

    public GetOrderDetailQueryTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenOrderIsNotExist_InvalidOperation_ExceptionShouldBeReturned()
    {
        GetOrderDetailQuery query = new GetOrderDetailQuery(_context, _mapper);
        query.OrderId = 999;

        FluentActions.Invoking(() => query.Handle())
                                            .Should().Throw<InvalidOperationException>()
                                            .And.Message.Should().Be("Sipariş bulunamadı.");
    }

    [Fact]
    public void WhenOrderIsExist_Order_ShouldBeReturned()
    {
        var order = _context.Orders.Include(o => o.Foods).FirstOrDefault(c => c.Id == 1);
        GetOrderDetailQuery query = new GetOrderDetailQuery(_context, _mapper);
        query.OrderId = 1;
        query.UserRole = "Admin";

        OrderDetailViewModel vm = query.Handle();

        vm.Should().NotBeNull();
        vm.OrderDate.Should().Be(order.OrderDate);
        vm.OrderStatus.Should().Be(order.OrderStatus.ToString());
        vm.OrderTotal.Should().Be(order.OrderTotal);
    }
}