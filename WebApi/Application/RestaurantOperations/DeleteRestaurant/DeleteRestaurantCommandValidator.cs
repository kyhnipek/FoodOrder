using FluentValidation;

namespace WebApi.Application.RestaurantOperations.DeleteRestaurant;

public class DeleteRestaurantCommandValidator : AbstractValidator<DeleteRestaurantCommand>
{
    public DeleteRestaurantCommandValidator()
    {
        RuleFor(c => c.RestaurantId).GreaterThan(0);
    }
}