using WebApi.DBOperations;

namespace WebApi.Application.CourierOperations.DeleteCourier;

public class DeleteCourierCommand
{
    private readonly IFoodOrderDBContext _context;
    public int CourierId { get; set; }
    public DeleteCourierCommand(IFoodOrderDBContext context)
    {
        _context = context;
    }

    public void Handle()
    {
        var courier = _context.Couriers.Where(c => c.IsActive).FirstOrDefault(c => c.Id == CourierId);
        if (courier is null)
            throw new InvalidOperationException("Kurye bulunamadı.");

        courier.IsActive = false;
        courier.Users.IsActive = false;
        _context.Couriers.Update(courier);
        _context.SaveChanges();
    }
}