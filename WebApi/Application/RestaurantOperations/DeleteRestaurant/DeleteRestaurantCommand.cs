using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Application.RestaurantOperations.DeleteRestaurant;

public class DeleteRestaurantCommand
{
    private readonly IFoodOrderDBContext _context;
    public int RestaurantId { get; set; }

    public DeleteRestaurantCommand(IFoodOrderDBContext context)
    {
        _context = context;
    }
    public void Handle()
    {
        var restaurant = _context.Restaurants.Where(r => r.IsActive).FirstOrDefault(r => r.Id == RestaurantId);
        if (restaurant is null)
            throw new InvalidOperationException("Restoran bulunamadı.");

        restaurant.IsActive = false;
        _context.Restaurants.Update(restaurant);
        _context.SaveChanges();
    }



}