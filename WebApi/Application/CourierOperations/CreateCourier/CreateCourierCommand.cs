using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.Services;

namespace WebApi.Application.CourierOperations.CreateCourier;

public class CreateCourierCommand
{
    private readonly IFoodOrderDBContext _context;

    public CreateCourierCommand(IFoodOrderDBContext context)
    {
        _context = context;
    }

    public CreateCourierModel Model { get; set; }

    public void Handle()
    {
        List<Restaurant> restaurants = new List<Restaurant>();
        var courier = _context.Couriers.Include(c => c.Users).FirstOrDefault(x => x.Users.Name == Model.Name && x.Users.Surname == Model.Surname);
        if (courier is not null)
            throw new InvalidOperationException("Kurye zaten mevcut.");

        foreach (var item in Model.RestoranIds)
        {
            var restaurant = _context.Restaurants.FirstOrDefault(r => r.Id == item);
            if (restaurant is null)
                throw new InvalidOperationException("Restorant bulunamadı.");
            restaurants.Add(restaurant);
        }
        string passwordSalt;
        string passwordHash = PasswordService.CreatePasswordHash(Model.Password, out passwordSalt);
        courier = new Courier()
        {
            Users = new User
            {
                Name = Model.Name,
                Surname = Model.Surname,
                Role = Role.Courier,
                Email = Model.Email,
                PasswordSalt = passwordSalt,
                PasswordHash = passwordHash,
                State = Model.State,
                City = Model.City,
            },
            Restaurants = restaurants,
        };


        _context.Couriers.Add(courier);
        _context.SaveChanges();

    }
}
public class CreateCourierModel
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public States State { get; set; }
    public Cities City { get; set; }
    public List<int> RestoranIds { get; set; }

}