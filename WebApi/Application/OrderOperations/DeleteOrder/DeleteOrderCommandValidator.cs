using FluentValidation;

namespace WebApi.Application.OrderOperations.DeleteOrder;

public class DeleteOrderCommandValidator : AbstractValidator<DeleteOrderCommand>
{
    public DeleteOrderCommandValidator()
    {
        RuleFor(c => c.OrderId).GreaterThan(0);
    }
}