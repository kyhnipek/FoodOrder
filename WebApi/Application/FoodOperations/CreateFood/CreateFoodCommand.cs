using System.Linq.Expressions;
using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.FoodOperations.CreateFood;

public class CreateFoodCommand
{
    private readonly IFoodOrderDBContext _context;
    private readonly IMapper _mapper;
    public CreateFoodModel Model { get; set; }
    public int UserId { get; set; }
    public string UserRole { get; set; }
    public CreateFoodCommand(IFoodOrderDBContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public void Handle()
    {
        Expression<Func<Food, bool>> queryByRole;
        if (UserRole == "Admin")
            queryByRole = (f => f.RestaurantId == Model.RestaurantId);
        else
            queryByRole = (f => f.Restaurant.UserId == UserId);

        var food = _context.Foods.Where(queryByRole).FirstOrDefault(f => f.Title == Model.Title);
        if (food is not null)
            throw new InvalidOperationException("Yiyecek restoranda zaten mevcut.");

        food = _mapper.Map<Food>(Model);
        if (UserRole == "Admin")
            food.RestaurantId = Model.RestaurantId;
        else
            food.RestaurantId = _context.Restaurants.FirstOrDefault(r => r.UserId == UserId).Id;

        _context.Foods.Add(food);
        _context.SaveChanges();
    }

}

public class CreateFoodModel
{
#nullable enable
    public int? RestaurantId { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public decimal Price { get; set; }
#nullable enable
    public string? ImgUrl { get; set; }
}