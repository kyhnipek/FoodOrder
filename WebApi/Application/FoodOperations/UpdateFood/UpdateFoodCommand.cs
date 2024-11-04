using System.Linq.Expressions;
using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.FoodOperations.UpdateFood;

public class UpdateFoodCommand
{
    private readonly IFoodOrderDBContext _context;
    private readonly IMapper _mapper;
    public int FoodId { get; set; }
    public UpdateFoodModel Model { get; set; }
    public int UserId { get; set; }
    public string UserRole { get; set; }

    public UpdateFoodCommand(IFoodOrderDBContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public void Handle()
    {
        Expression<Func<Food, bool>> queryByRole;
        if (UserRole == "Admin")
            queryByRole = (x => x.IsActive && x.Id == FoodId);
        else
            queryByRole = (x => x.IsActive && x.Restaurant.UserId == UserId && x.Id == FoodId);

        var food = _context.Foods.SingleOrDefault(queryByRole);
        if (food is null)
            throw new InvalidOperationException("Yiyecek bulunamadı.");

        food = _mapper.Map<UpdateFoodModel, Food>(Model, food);
        _context.Foods.Update(food);
        _context.SaveChanges();
    }
}
public class UpdateFoodModel
{
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
#nullable enable
    public string? ImgUrl { get; set; }
}