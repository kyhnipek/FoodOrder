using FluentAssertions;
using WebApi.Application.FoodOperations.GetFoodDetail;

namespace WebApi.UnitTests.Application.FoodOperations.GetFoodDetail;

public class GetFoodDetailQueryValidatorTests
{
    [Fact]
    public void WhenInvalidInputIsGiven_Validator_ShouldBeReturnError()
    {
        GetFoodDetailQuery query = new GetFoodDetailQuery(null, null);
        query.FoodId = 0;

        GetFoodDetailQueryValidator validator = new GetFoodDetailQueryValidator();
        var result = validator.Validate(query);

        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidInputIsGiven_Validator_ShouldNotBeReturnError()
    {
        GetFoodDetailQuery query = new GetFoodDetailQuery(null, null);
        query.FoodId = 1;

        GetFoodDetailQueryValidator validator = new GetFoodDetailQueryValidator();
        var result = validator.Validate(query);

        result.Errors.Count.Should().Be(0);
    }
}