using FluentAssertions;
using WebApi.Application.CourierOperations.GetCourierDetail;

namespace WebApi.UnitTests.Application.CourierOperations.GetCourierDetail;

public class GetCourierDetailQueryValidatorTests
{
    [Fact]
    public void WhenInvalidInputIsGiven_Validator_ShouldBeReturnError()
    {
        GetCourierDetailQuery query = new GetCourierDetailQuery(null, null);
        query.CourierId = 0;

        GetCourierDetailQueryValidator validator = new GetCourierDetailQueryValidator();
        var result = validator.Validate(query);

        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidInputIsGiven_Validator_ShouldNotBeReturnError()
    {
        GetCourierDetailQuery query = new GetCourierDetailQuery(null, null);
        query.CourierId = 1;

        GetCourierDetailQueryValidator validator = new GetCourierDetailQueryValidator();
        var result = validator.Validate(query);

        result.Errors.Count.Should().Be(0);
    }
}