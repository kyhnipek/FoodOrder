using FluentValidation;

namespace WebApi.Application.RestaurantOperations.CreateRestaurant;

public class CreateRestaurantCommandValidator : AbstractValidator<CreateRestaurantCommand>

{
    public CreateRestaurantCommandValidator()
    {
        RuleFor(c => c.Model.Title).NotEmpty().MinimumLength(2);
        RuleFor(c => c.Model.UserId).GreaterThan(0);
        RuleFor(c => c.Model.Type).NotNull();
        RuleFor(c => c.Model.City).NotNull();
        RuleFor(c => c.Model.State).NotNull();
    }
}