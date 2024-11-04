using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Application.RestaurantOperations.GetRestaurantDetail;
using WebApi.DBOperations;

namespace WebApi.Application.CourierOperations.GetCourierDetail;

public class GetCourierDetailQuery
{
    private readonly IFoodOrderDBContext _context;
    private readonly IMapper _mapper;
    public int CourierId { get; set; }
    public GetCourierDetailQuery(IFoodOrderDBContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public CourierDetailViewModel Handle()
    {
        var courier = _context.Couriers
                                .Include(c => c.Restaurants)
                                .Include(c => c.Users)
                                .Where(c => c.IsActive)
                                .FirstOrDefault(c => c.Id == CourierId);
        if (courier is null)
            throw new InvalidOperationException("Kurye bulunamadı.");

        CourierDetailViewModel vm = _mapper.Map<CourierDetailViewModel>(courier);
        return vm;
    }
}

public class CourierDetailViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public List<RestaurantDetailViewModelShort> Restaurants { get; set; } = new List<RestaurantDetailViewModelShort>();
}
public class CourierDetailViewModelShort
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
}