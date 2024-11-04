using FluentValidation;

namespace WebApi.Application.FoodOperations.UpdateFood;

public class UpdateFoodCommandValidator : AbstractValidator<UpdateFoodCommand>
{
    public UpdateFoodCommandValidator()
    {
        RuleFor(c => c.FoodId).GreaterThan(0);
        RuleFor(c => c.Model.Title).MinimumLength(2).When(c => c.Model.Title != string.Empty);
        RuleFor(c => c.Model.Description).MinimumLength(5).When(c => c.Model.Description != string.Empty);
        RuleFor(c => c.Model.ImgUrl).MinimumLength(5).When(c => c.Model.ImgUrl != string.Empty);

    }
}