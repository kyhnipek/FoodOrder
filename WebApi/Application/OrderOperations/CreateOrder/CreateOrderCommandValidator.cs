using FluentValidation;

namespace WebApi.Application.OrderOperations.CreateOrder;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(c => c.UserId).GreaterThan(0);
        RuleFor(c => c.Model.RestaurantId).GreaterThan(0);
        RuleFor(c => c.Model.FoodIds).NotEmpty();
        RuleFor(c => c.Model.Quantities).NotEmpty();
        RuleFor(c => c.Model.FoodIds.Count).Equal(c => c.Model.Quantities.Count);
        RuleFor(c => c.Model.CourierId).GreaterThan(0);
    }
}