using FluentValidation;

namespace WebApi.Application.UserOperations.CreateUser;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(c => c.Model.Name).NotEmpty().MinimumLength(2);
        RuleFor(c => c.Model.Surname).NotEmpty().MinimumLength(2);
        RuleFor(c => c.Model.Email).NotEmpty().EmailAddress().MinimumLength(5);
        RuleFor(c => c.Model.Password).NotEmpty().MinimumLength(6);
    }
}