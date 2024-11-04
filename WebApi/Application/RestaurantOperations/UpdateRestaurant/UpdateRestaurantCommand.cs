
using System.Linq.Expressions;
using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.RestaurantOperations.UpdateRestaurant;

public class UpdateRestaurantCommand
{
    private readonly IFoodOrderDBContext _context;
    private readonly IMapper _mapper;
    public UpdateRestaurantModel Model { get; set; }
    public int RestaurantId { get; set; }
    public string UserRole { get; set; }
    public UpdateRestaurantCommand(IFoodOrderDBContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public void Handle()
    {
        Expression<Func<Restaurant, bool>> queryByRole;
        if (UserRole == "Admin")
            queryByRole = (r => r.Id == RestaurantId);
        else
            queryByRole = (r => r.IsActive && r.Id == RestaurantId);

        var restaurant = _context.Restaurants.FirstOrDefault(queryByRole);
        if (restaurant is null)
            throw new InvalidOperationException("Restoran bulunamadı.");

        restaurant = _mapper.Map<UpdateRestaurantModel, Restaurant>(Model, restaurant);
        _context.Restaurants.Update(restaurant);
        _context.SaveChanges();
    }
}

public class UpdateRestaurantModel
{
#nullable enable
    public string? Title { get; set; }
#nullable enable
    public Type? Type { get; set; }
#nullable enable
    public string? Adress { get; set; }
#nullable enable
    public States? State { get; set; }
#nullable enable
    public Cities? City { get; set; }
#nullable enable
    public int? UserId { get; set; }
#nullable enable
    public bool? IsActive { get; set; }

}