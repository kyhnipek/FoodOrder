using FluentValidation;

namespace WebApi.Application.FoodOperations.DeleteFood;

public class DeleteFoodCommandValidator : AbstractValidator<DeleteFoodCommand>
{
    public DeleteFoodCommandValidator()
    {
        RuleFor(c => c.FoodId).GreaterThan(0);
    }
}