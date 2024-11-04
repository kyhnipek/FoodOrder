using FluentValidation;

namespace WebApi.Application.CourierOperations.DeleteCourier;

public class DeleteCourierCommandValidator : AbstractValidator<DeleteCourierCommand>
{
    public DeleteCourierCommandValidator()
    {
        RuleFor(c => c.CourierId).GreaterThan(0);
    }
}