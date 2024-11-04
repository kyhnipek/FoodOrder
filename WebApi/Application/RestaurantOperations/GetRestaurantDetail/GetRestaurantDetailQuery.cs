using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Application.FoodOperations.GetFoodDetail;
using WebApi.DBOperations;

namespace WebApi.Application.RestaurantOperations.GetRestaurantDetail;

public class GetRestaurantDetailQuery
{
    private readonly IFoodOrderDBContext _context;
    private readonly IMapper _mapper;
    public int RestaurantId { get; set; }
    public GetRestaurantDetailQuery(IFoodOrderDBContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public RestaurantDetailViewModel Handle()
    {
        var restaurant = _context.Restaurants.Include(r => r.Foods).Where(r => r.IsActive).FirstOrDefault(r => r.Id == RestaurantId);
        if (restaurant is null)
            throw new InvalidOperationException("Restoran bulunamadı.");

        RestaurantDetailViewModel vm = _mapper.Map<RestaurantDetailViewModel>(restaurant);
        RestaurantDetailViewModelShort vms = _mapper.Map<RestaurantDetailViewModelShort>(restaurant);
        return vm;
    }
}
public class RestaurantDetailViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Type { get; set; }
    public string Adress { get; set; }
    public string State { get; set; }
    public string City { get; set; }
    public List<FoodDetailViewModel> Foods { get; set; } = new List<FoodDetailViewModel>();
}
public class RestaurantDetailViewModelShort
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Type { get; set; }
    public string Adress { get; set; }
    public string State { get; set; }
    public string City { get; set; }
}