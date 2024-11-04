using FluentValidation;

namespace WebApi.Application.FoodOperations.CreateFood;

public class CreateFoodCommandValidator : AbstractValidator<CreateFoodCommand>
{
    public CreateFoodCommandValidator()
    {
        RuleFor(c => c.Model.Title).NotEmpty().MinimumLength(2);
        RuleFor(c => c.Model.Description).NotEmpty().MinimumLength(2);
        RuleFor(c => c.Model.Price).NotEmpty();
        RuleFor(c => c.Model.RestaurantId).GreaterThan(0);
    }
}