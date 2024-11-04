using FluentAssertions;
using WebApi.Application.RestaurantOperations.GetRestaurantDetail;

namespace WebApi.UnitTests.Application.RestaurantOperations.GetRestaurantDetail;
public class GetRestaurantDetailQueryValidatorTests
{
    [Fact]
    public void WhenInvalidInputIsGiven_Validator_ShouldBeReturnError()
    {
        GetRestaurantDetailQuery query = new GetRestaurantDetailQuery(null, null);
        query.RestaurantId = 0;

        GetRestaurantDetailQueryValidator validator = new GetRestaurantDetailQueryValidator();
        var result = validator.Validate(query);

        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidInputIsGiven_Validator_ShouldNotBeReturnError()
    {
        GetRestaurantDetailQuery query = new GetRestaurantDetailQuery(null, null);
        query.RestaurantId = 1;

        GetRestaurantDetailQueryValidator validator = new GetRestaurantDetailQueryValidator();
        var result = validator.Validate(query);

        result.Errors.Count.Should().Be(0);
    }
}