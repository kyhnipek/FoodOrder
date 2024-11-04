using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WebApi.Application.RestaurantOperations.GetRestaurantDetail;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.RestaurantOperations.GetRestaurantDetail;

public class GetRestaurantDetailQueryTests : IClassFixture<CommonTestFixture>
{
    private readonly IFoodOrderDBContext _context;
    private readonly IMapper _mapper;

    public GetRestaurantDetailQueryTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenRestaurantIsNotExist_InvalidOperation_ExceptionShouldBeReturned()
    {
        GetRestaurantDetailQuery query = new GetRestaurantDetailQuery(_context, _mapper);
        query.RestaurantId = 999;

        FluentActions.Invoking(() => query.Handle())
                                            .Should().Throw<InvalidOperationException>()
                                            .And.Message.Should().Be("Restoran bulunamadı.");
    }

    [Fact]
    public void WhenRestaurantIsExist_Restaurant_ShouldBeReturned()
    {
        var restaurant = _context.Restaurants.Include(o => o.Foods).FirstOrDefault(c => c.Id == 1);
        GetRestaurantDetailQuery query = new GetRestaurantDetailQuery(_context, _mapper);
        query.RestaurantId = 1;

        RestaurantDetailViewModel vm = query.Handle();

        vm.Should().NotBeNull();
        vm.City.Should().Be(restaurant.City.ToString());
        vm.State.Should().Be(restaurant.State.ToString());
        vm.Adress.Should().Be(restaurant.Adress);
        vm.Title.Should().Be(restaurant.Title);
        vm.Type.Should().Be(restaurant.Type.ToString());

    }
}