using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities;

public class Courier
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public ICollection<Restaurant> Restaurants { get; set; } = new List<Restaurant>();
    public int UserId { get; set; }
    public User Users { get; set; }
    public bool IsActive { get; set; } = true;
    // public ICollection<Order>? Orders { get; set; } = new List<Order>();

}