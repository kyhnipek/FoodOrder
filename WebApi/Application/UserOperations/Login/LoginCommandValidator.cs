using System.Data;
using FluentValidation;

namespace WebApi.Application.UserOperations.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(c => c.Model.Email).NotEmpty().EmailAddress();
        RuleFor(c => c.Model.Password).NotEmpty().MinimumLength(6);
    }
}