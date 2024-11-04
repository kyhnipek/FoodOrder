using FluentValidation;

namespace WebApi.Application.CourierOperations.UpdateCourier;

public class UpdateCourierCommandValidator : AbstractValidator<UpdateCourierCommand>
{
    public UpdateCourierCommandValidator()
    {
        RuleFor(c => c.CourierId).GreaterThan(0);
    }
}