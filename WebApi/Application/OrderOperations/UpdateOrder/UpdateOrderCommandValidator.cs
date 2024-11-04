using FluentValidation;

namespace WebApi.Application.OrderOperations.UpdateOrder;

public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(c => c.OrderId).GreaterThan(0);
        RuleFor(c => c.UserId).GreaterThan(0);
    }
}