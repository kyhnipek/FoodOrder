using FluentValidation;

namespace WebApi.Application.CourierOperations.CreateCourier;

public class CreateCourierCommandValidator : AbstractValidator<CreateCourierCommand>
{
    public CreateCourierCommandValidator()
    {
        RuleFor(c => c.Model.Name).NotEmpty().MinimumLength(2);
        RuleFor(c => c.Model.Surname).NotEmpty().MinimumLength(2);
        RuleFor(c => c.Model.Email).NotEmpty().MinimumLength(5).EmailAddress();
        RuleFor(c => c.Model.Password).NotEmpty().MinimumLength(6);
    }
}