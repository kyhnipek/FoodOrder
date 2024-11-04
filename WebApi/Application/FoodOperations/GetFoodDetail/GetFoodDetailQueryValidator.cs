using FluentValidation;

namespace WebApi.Application.FoodOperations.GetFoodDetail;

public class GetFoodDetailQueryValidator : AbstractValidator<GetFoodDetailQuery>
{
    public GetFoodDetailQueryValidator()
    {
        RuleFor(q => q.FoodId).GreaterThan(0);
    }
}