using System.Linq.Expressions;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.FoodOperations.DeleteFood;

public class DeleteFoodCommand
{
    private readonly IFoodOrderDBContext _context;
    public int FoodId { get; set; }
    public int UserId { get; set; }
    public string UserRole { get; set; }
    public DeleteFoodCommand(IFoodOrderDBContext context)
    {
        _context = context;
    }

    public void Handle()
    {
        Expression<Func<Food, bool>> queryByRole;
        if (UserRole == "Admin")
            queryByRole = (x => x.IsActive && x.Id == FoodId);
        else
            queryByRole = (x => x.IsActive && x.Restaurant.UserId == UserId && x.Id == FoodId);

        var food = _context.Foods.FirstOrDefault(queryByRole);
        if (food is null)
            throw new InvalidOperationException("Yiyecek bulunamadı.");

        _context.Foods.Remove(food);
        _context.SaveChanges();
    }



}