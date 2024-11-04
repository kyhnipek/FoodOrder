using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.OrderOperations.CreateOrder;

public class CreateOrderCommand
{
    private readonly IFoodOrderDBContext _context;
    public CreateOrderModel Model { get; set; }
    public int UserId { get; set; }

    public CreateOrderCommand(IFoodOrderDBContext context)
    {
        _context = context;
    }

    public void Handle()
    {
        List<Food> foods = new List<Food>();
        List<Quantity> quantities = new List<Quantity>();
        decimal totalPrice = 0;
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

        Order o = new Order();
        o.UserId = UserId;
        o.Foods = foods;
        o.OrderDate = DateTime.Now;
        o.OrderTotal = totalPrice;
        o.RestaurantId = Model.RestaurantId;
        o.Quantities = quantities;
        o.CourierId = Model.CourierId;
        _context.Orders.Add(o);
        _context.SaveChanges();
    }
}

public class CreateOrderModel
{
    public int RestaurantId { get; set; }
    public List<int> FoodIds { get; set; } = new List<int>();
    public List<int> Quantities { get; set; } = new List<int>();
    public int CourierId { get; set; }

}