using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Application.RestaurantOperations.GetRestaurants;

public class GetRestaurantsQuery
{
    private readonly IFoodOrderDBContext _context;
    private readonly IMapper _mapper;
    public GetRestaurantsQuery(IFoodOrderDBContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }


    public List<RestaurantsViewModel> Handle()
    {
        var restaurant = _context.Restaurants.Where(r => r.IsActive).OrderBy(r => r.Id);

        List<RestaurantsViewModel> vm = _mapper.Map<List<RestaurantsViewModel>>(restaurant).ToList();
        // foreach (var r in vm)
        // {
        //     r.State = Enum.GetName(typeof(States), Convert.ToInt32(r.State));
        //     r.City = Enum.GetName(typeof(Cities), Convert.ToInt32(r.City));
        //     r.Type = Enum.GetName(typeof(Type), Convert.ToInt32(r.City));
        // }
        return vm;
    }
}
public class RestaurantsViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Type { get; set; }
    public string Adress { get; set; }
    public string State { get; set; }
    public string City { get; set; }
}