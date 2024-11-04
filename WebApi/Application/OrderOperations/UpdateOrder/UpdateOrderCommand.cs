using System.Linq.Expressions;
using System.Security.Authentication;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.OrderOperations.UpdateOrder;

public class UpdateOrderCommand
{
    private readonly IFoodOrderDBContext _context;
    public int OrderId { get; set; }
    public int UserId { get; set; }
    public string UserRole { get; set; }
    public UpdateOrderModel Model { get; set; }

    public UpdateOrderCommand(IFoodOrderDBContext context)
    {
        _context = context;
    }

    public void Handle()
    {
        Expression<Func<Order, bool>> queryByRole;
        if (UserRole == "Admin")
            queryByRole = (x => x.IsActive && x.Id == OrderId);
        else if (UserRole == "Restaurant")
            queryByRole = (x => x.IsActive && x.Restaurant.UserId == UserId && x.Id == OrderId);
        else
            queryByRole = (x => x.Couriers.UserId == UserId && x.Id == OrderId);

        var order = _context.Orders.Include(o => o.Quantities).FirstOrDefault(queryByRole);
        if (order is null)
            throw new InvalidOperationException("Sipariş bulunamadı.");

        List<Food> foods = new List<Food>();
        List<Quantity> quantities = new List<Quantity>();
        decimal totalPrice = 0;
        if (Model.FoodIds != null && Model.Quantities != null)
        {
            foreach (var item in Model.FoodIds)
            {
                var food = _context.Foods.FirstOrDefault(f => f.Id == item);
                if (food is null)
                    throw new InvalidOperationException("Yiyecek bulunamadı.");
                foods.Add(food);

                int currentFoodIndex = foods.FindIndex(f => f.Id == item);
                var q = new Quantity() { FoodId = item, Quantities = Model.Quantities[currentFoodIndex] };
                quantities.Add(q);

                totalPrice = totalPrice + (Model.Quantities[currentFoodIndex] * food.Price);
            }
        }

        order.Foods = Model.FoodIds != default ? foods : order.Foods;
        order.OrderTotal = Model.FoodIds != default ? totalPrice : order.OrderTotal;
        order.Quantities = Model.Quantities != default ? quantities : order.Quantities;
        order.OrderStatus = Model.OrderStatus != default ? Model.OrderStatus : order.OrderStatus;
        _context.Orders.Update(order);
        _context.SaveChanges();

    }
}

public class UpdateOrderModel
{
#nullable enable
    public List<int>? FoodIds { get; set; }
#nullable enable
    public List<int>? Quantities { get; set; }
    public Status OrderStatus { get; set; }
}