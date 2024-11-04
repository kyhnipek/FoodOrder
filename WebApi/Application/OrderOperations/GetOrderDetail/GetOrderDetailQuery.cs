using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Application.CourierOperations.GetCourierDetail;
using WebApi.Application.FoodOperations.GetFoodDetail;
using WebApi.Application.RestaurantOperations.GetRestaurantDetail;
using WebApi.Application.UserOperations.GetUserDetail;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.OrderOperations.GetOrderDetail;

public class GetOrderDetailQuery
{
    private readonly IFoodOrderDBContext _context;
    private readonly IMapper _mapper;
    public int OrderId { get; set; }
    public int UserId { get; set; }
    public string UserRole { get; set; }

    public GetOrderDetailQuery(IFoodOrderDBContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public OrderDetailViewModel Handle()
    {
        Expression<Func<Order, bool>> queryByRole;
        if (UserRole == "Admin")
            queryByRole = (x => x.IsActive && x.Id == OrderId);
        else if (UserRole == "Restaurant")
            queryByRole = (x => x.IsActive && x.Restaurant.UserId == UserId && x.Id == OrderId);
        else if (UserRole == "Courier")
            queryByRole = (x => x.Couriers.UserId == UserId && x.Id == OrderId);
        else
            queryByRole = (x => x.IsActive && x.UserId == UserId && x.Id == OrderId);

        var order = _context.Orders
                                .Include(o => o.Foods)
                                .Include(o => o.Restaurant)
                                .Include(o => o.Quantities)
                                .Include(o => o.Users)
                                .Include(o => o.Couriers).ThenInclude(c => c.Users)
                                .Where(o => o.IsActive)
                                .FirstOrDefault(queryByRole);

        if (order is null)
            throw new InvalidOperationException("Sipariş bulunamadı.");

        OrderDetailViewModel vm = _mapper.Map<OrderDetailViewModel>(order);
        var orderQuantities = _context.Quantities.Include(x => x.Order)
            .Where(q => q.Id == OrderId)
            .ToDictionary(q => q.FoodId, q => q.Quantities);

        foreach (var food in vm.Foods)
        {
            if (orderQuantities.TryGetValue(food.Id, out int quantity))
            {
                food.Quantity = quantity;
            }

        }

        return vm;
    }
}

public class OrderDetailViewModel
{
    public int Id { get; set; }
    public UserDetailViewModel Users { get; set; }
    public List<FoodDetailViewModelShort> Foods { get; set; } = new List<FoodDetailViewModelShort>();
    public RestaurantDetailViewModelShort Restaurant { get; set; }
    public CourierDetailViewModelShort Couriers { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal OrderTotal { get; set; }
    public string OrderStatus { get; set; }
}