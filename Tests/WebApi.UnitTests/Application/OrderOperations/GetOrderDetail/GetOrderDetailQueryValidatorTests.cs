
using FluentAssertions;
using WebApi.Application.OrderOperations.GetOrderDetail;

namespace WebApi.UnitTests.Application.OrderOperations.GetOrderDetail;

public class GetOrderDetailQueryValidatorTests
{
    [Fact]
    public void WhenInvalidInputIsGiven_Validator_ShouldBeReturnError()
    {
        GetOrderDetailQuery query = new GetOrderDetailQuery(null, null);
        query.OrderId = 0;

        GetOrderDetailQueryValidator validator = new GetOrderDetailQueryValidator();
        var result = validator.Validate(query);

        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidInputIsGiven_Validator_ShouldNotBeReturnError()
    {
        GetOrderDetailQuery query = new GetOrderDetailQuery(null, null);
        query.OrderId = 1;

        GetOrderDetailQueryValidator validator = new GetOrderDetailQueryValidator();
        var result = validator.Validate(query);

        result.Errors.Count.Should().Be(0);
    }
}