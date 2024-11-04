
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.CourierOperations.UpdateCourier;

public class UpdateCourierCommand
{
    private readonly IFoodOrderDBContext _context;
    // private readonly IMapper _mapper;
    public int CourierId { get; set; }

    public UpdateCourierCommand(IFoodOrderDBContext context)
    {
        _context = context;
        // _mapper = mapper;
    }

    public UpdateCourierModel Model { get; set; }

    public void Handle()
    {
        List<Restaurant> restaurants = new List<Restaurant>();
        var courier = _context.Couriers.FirstOrDefault(c => c.Id == CourierId);
        if (courier is null)
            throw new InvalidOperationException("Kurye bulunamadı.");

        if (Model.RestoranIds != null)
        {
            foreach (var item in Model.RestoranIds)
            {
                var restaurant = _context.Restaurants.FirstOrDefault(r => r.Id == item);
                if (restaurant is null)
                    throw new InvalidOperationException("Restorant bulunamadı.");
                restaurants.Add(restaurant);
            }
        }
        // courier = _mapper.Map<UpdateCourierModel, Courier>(Model, courier);
        courier.IsActive = Model.IsActive != default ? Model.IsActive : courier.IsActive;
        courier.Restaurants = Model.RestoranIds != default ? restaurants : courier.Restaurants;
        _context.Update(courier);
        _context.SaveChanges();
    }
}
public class UpdateCourierModel
{
#nullable enable
    public List<int>? RestoranIds { get; set; }
    public bool IsActive { get; set; } = true;

}