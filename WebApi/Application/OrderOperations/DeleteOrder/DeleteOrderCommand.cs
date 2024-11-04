using System.Linq.Expressions;
using System.Security.Authentication;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.OrderOperations.DeleteOrder;

public class DeleteOrderCommand
{
    private readonly IFoodOrderDBContext _context;
    public DeleteOrderCommand(IFoodOrderDBContext context)
    {
        _context = context;
    }

    public void Handle()
    {
        var order = _context.Orders.Include(o => o.Restaurant).FirstOrDefault(x => x.IsActive && x.Id == OrderId);
        if (order is null)
            throw new InvalidOperationException("Sipariş bulunamadı.");

        order.IsActive = false;

        _context.Orders.Update(order);
        _context.SaveChanges();
    }

    public int OrderId { get; set; }


}