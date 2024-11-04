using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Application.RestaurantOperations.GetRestaurantDetail;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.CourierOperations.GetCouriers;

public class GetCouriersQuery
{
    private readonly IFoodOrderDBContext _context;
    private readonly IMapper _mapper;

    public GetCouriersQuery(IFoodOrderDBContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public List<CouriersViewModel> Handle()
    {
        var couriers = _context.Couriers
                                    .Include(c => c.Users)
                                    .Include(c => c.Restaurants)
                                    .Where(c => c.IsActive)
                                    .OrderBy(c => c.Id)
                                    .ToList();
        List<CouriersViewModel> vm = _mapper.Map<List<CouriersViewModel>>(couriers);
        return vm;
    }
}

public class CouriersViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public List<RestaurantDetailViewModelShort> Restaurants { get; set; } = new List<RestaurantDetailViewModelShort>();
}