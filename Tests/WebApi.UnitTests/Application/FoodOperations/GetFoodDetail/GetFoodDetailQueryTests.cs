using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WebApi.Application.FoodOperations.GetFoodDetail;
using WebApi.DBOperations;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.FoodOperations.GetFoodDetail;

public class GetFoodDetailQueryTests : IClassFixture<CommonTestFixture>
{
    private readonly IFoodOrderDBContext _context;
    private readonly IMapper _mapper;

    public GetFoodDetailQueryTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenFoodIsNotExist_InvalidOperation_ExceptionShouldBeReturned()
    {
        GetFoodDetailQuery query = new GetFoodDetailQuery(_context, _mapper);
        query.FoodId = 999;

        FluentActions.Invoking(() => query.Handle())
                                            .Should().Throw<InvalidOperationException>()
                                            .And.Message.Should().Be("Yiyecek bulunamadı.");
    }

    [Fact]
    public void WhenFoodIsExist_Food_ShouldBeReturned()
    {
        var food = _context.Foods.FirstOrDefault(c => c.Id == 1);
        GetFoodDetailQuery query = new GetFoodDetailQuery(_context, _mapper);
        query.FoodId = 1;

        FoodDetailViewModel vm = query.Handle();

        vm.Should().NotBeNull();
        vm.Title.Should().Be(food.Title);
        vm.Description.Should().Be(food.Description);
        vm.ImgUrl.Should().Be(food.ImgUrl);
        vm.Price.Should().Be(food.Price);
        vm.RestaurantId.Should().Be(food.RestaurantId);
    }
}