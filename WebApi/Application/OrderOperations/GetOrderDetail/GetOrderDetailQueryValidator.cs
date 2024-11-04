using FluentValidation;

namespace WebApi.Application.OrderOperations.GetOrderDetail;

public class GetOrderDetailQueryValidator : AbstractValidator<GetOrderDetailQuery>
{
    public GetOrderDetailQueryValidator()
    {
        RuleFor(query => query.OrderId).GreaterThan(0);
    }
}