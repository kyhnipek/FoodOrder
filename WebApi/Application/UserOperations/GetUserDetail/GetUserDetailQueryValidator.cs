using System.Data;
using FluentValidation;

namespace WebApi.Application.UserOperations.GetUserDetail;

public class GetUserDetailQueryValidator : AbstractValidator<GetUserDetailQuery>
{
    public GetUserDetailQueryValidator()
    {
        RuleFor(q => q.UserId).GreaterThan(0);
    }
}