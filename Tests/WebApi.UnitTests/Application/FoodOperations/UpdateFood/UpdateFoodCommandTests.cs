using AutoMapper;
using FluentAssertions;
using WebApi.Application.FoodOperations.UpdateFood;
using WebApi.DBOperations;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.FoodOperations.UpdateFood;

public class UpdateFoodCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly IFoodOrderDBContext _context;
    private readonly IMapper _mapper;

    public UpdateFoodCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenFoodIsNotExist_InvalidOperationException_ShouldBeReturned()
    {
        UpdateFoodCommand command = new UpdateFoodCommand(_context, _mapper);
        command.FoodId = 555;
        command.Model = new UpdateFoodModel() { Title = "test", Description = "test", ImgUrl = "test", Price = 100 };

        FluentActions.Invoking(() => command.Handle())
                                        .Should().Throw<InvalidOperationException>()
                                        .And.Message.Should().Be("Yiyecek bulunamadı.");
    }

    [Fact]
    public void WhenValidInputsAreGiven_Food_ShouldBeUpdated()
    {
        UpdateFoodModel model = new UpdateFoodModel() { Title = "test", Description = "test", ImgUrl = "test", Price = 100 };
        UpdateFoodCommand command = new UpdateFoodCommand(_context, _mapper);
        command.FoodId = 1;
        command.Model = model;
        command.UserRole = "Admin";

        FluentActions.Invoking(() => command.Handle()).Invoke();

        var food = _context.Foods.FirstOrDefault(x => x.Id == command.FoodId);

        food.Should().NotBeNull();
        food.Title.Should().Be(model.Title);
        food.Description.Should().Be(model.Description);
        food.ImgUrl.Should().Be(model.ImgUrl);
        food.Price.Should().Be(model.Price);
    }
}