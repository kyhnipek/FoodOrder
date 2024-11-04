using FluentValidation;

namespace WebApi.Application.CourierOperations.GetCourierDetail;

public class GetCourierDetailQueryValidator : AbstractValidator<GetCourierDetailQuery>
{
    public GetCourierDetailQueryValidator()
    {
        RuleFor(q => q.CourierId).GreaterThan(0);
    }
}