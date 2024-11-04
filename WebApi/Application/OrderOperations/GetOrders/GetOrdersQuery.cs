using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Application.FoodOperations.GetFoodDetail;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.OrderOperations.GetOrders;

public class GetOrdersQuery
{
    private readonly IFoodOrderDBContext _context;
    private readonly IMapper _mapper;
    public int UserId { get; set; }
    public string UserRole { get; set; }

    public GetOrdersQuery(IFoodOrderDBContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public List<OrdersViewModel> Handle()
    {
        Expression<Func<Order, bool>> queryByRole;
        if (UserRole == "Admin")
            queryByRole = (x => x.IsActive);
        else if (UserRole == "Restaurant")
            queryByRole = (x => x.IsActive && x.Restaurant.UserId == UserId);
        else if (UserRole == "Courier")
            queryByRole = (x => x.Couriers.UserId == UserId);
        else
            queryByRole = (x => x.IsActive && x.UserId == UserId);

        var order = _context.Orders
                                .Include(o => o.Foods)
                                .Include(o => o.Restaurant)
                                .Include(o => o.Quantities)
                                .Include(o => o.Couriers)
                                .Where(queryByRole)
                                .OrderBy(o => o.Id)
                                .ToList();
        List<OrdersViewModel> vm = _mapper.Map<List<OrdersViewModel>>(order);

        foreach (var orderVm in vm)
        {
            foreach (var food in orderVm.Foods)
            {
                var quantity = order
                    .FirstOrDefault(o => o.Id == orderVm.Id)?
                    .Quantities
                    .FirstOrDefault(q => q.FoodId == food.Id)?
                    .Quantities ?? 0;
                food.Quantity = quantity;
            }
        }

        return vm;
    }
}

public class OrdersViewModel
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public List<FoodDetailViewModelShort> Foods { get; set; } = new List<FoodDetailViewModelShort>();
    public int RestaurantId { get; set; }
    public int CourierId { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal OrderTotal { get; set; }
    public string OrderStatus { get; set; }
}