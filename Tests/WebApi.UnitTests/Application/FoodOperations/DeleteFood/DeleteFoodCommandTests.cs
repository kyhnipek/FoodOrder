using FluentAssertions;
using WebApi.Application.FoodOperations.DeleteFood;
using WebApi.DBOperations;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.FoodOperations.DeleteFood;

public class DeleteFoodCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly IFoodOrderDBContext _context;

    public DeleteFoodCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
    }

    [Fact]
    public void WhenFoodNotExist_InvalidOperationException_ShouldBeReturn()
    {
        DeleteFoodCommand command = new DeleteFoodCommand(_context);
        command.FoodId = 999;

        FluentActions.Invoking(() => command.Handle())
                                .Should().Throw<InvalidOperationException>()
                                .And.Message.Should().Be("Yiyecek bulunamadı.");
    }

    [Fact]
    public void WhenValidInputIsGiven_Food_ShouldBeDeleted()
    {
        DeleteFoodCommand command = new DeleteFoodCommand(_context);
        command.FoodId = 1;
        command.UserRole = "Admin";

        FluentActions.Invoking(() => command.Handle()).Invoke();
        var food = _context.Foods.Where(x => x.IsActive).FirstOrDefault(x => x.Id == command.FoodId);

        food.Should().BeNull();
    }
}