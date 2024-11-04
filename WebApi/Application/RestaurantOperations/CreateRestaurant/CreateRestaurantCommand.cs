using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.RestaurantOperations.CreateRestaurant;

public class CreateRestaurantCommand
{
    private readonly IFoodOrderDBContext _context;
    private readonly IMapper _mapper;
    public CreateRestaurantModel Model { get; set; }
    public CreateRestaurantCommand(IFoodOrderDBContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public void Handle()
    {
        var restaurant = _context.Restaurants.FirstOrDefault(r => r.City == Model.City && r.Title == Model.Title);
        if (restaurant is not null)
            throw new InvalidOperationException("Restoran zaten mevcut.");

        restaurant = _mapper.Map<Restaurant>(Model);
        _context.Restaurants.Add(restaurant);
        _context.SaveChanges();
    }



}

public class CreateRestaurantModel
{
    public string Title { get; set; }
    public Type Type { get; set; } = Type.Restaurant;
#nullable enable
    public string? Adress { get; set; }
    public States State { get; set; }
    public Cities City { get; set; }
    public int UserId { get; set; }

}