using FluentValidation;

namespace WebApi.Application.RestaurantOperations.UpdateRestaurant;

public class UpdateRestaurantCommandValidator : AbstractValidator<UpdateRestaurantCommand>
{
    public UpdateRestaurantCommandValidator()
    {
        RuleFor(c => c.RestaurantId).GreaterThan(0);
        RuleFor(c => c.Model.Title).MinimumLength(2).When(c => c.Model.Title.Trim() != string.Empty);
        RuleFor(c => c.Model.Adress).MinimumLength(10).When(c => c.Model.Adress.Trim() != string.Empty);
    }
}