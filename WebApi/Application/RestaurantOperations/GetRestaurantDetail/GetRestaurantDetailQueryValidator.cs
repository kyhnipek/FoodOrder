using FluentValidation;

namespace WebApi.Application.RestaurantOperations.GetRestaurantDetail;

public class GetRestaurantDetailQueryValidator : AbstractValidator<GetRestaurantDetailQuery>
{
    public GetRestaurantDetailQueryValidator()
    {
        RuleFor(q => q.RestaurantId).GreaterThan(0);
    }
}