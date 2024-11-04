using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities;

public class Restaurant
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Title { get; set; }
    public Type Type { get; set; } = Type.Restaurant;
#nullable enable
    public string? Adress { get; set; }
    public States State { get; set; }
    public Cities City { get; set; }
#nullable enable
    public ICollection<Food>? Foods { get; set; }
#nullable enable
    public ICollection<Order>? Orders { get; set; }
    public int UserId { get; set; }
    public User? Users { get; set; }
    public ICollection<Courier> Couriers { get; set; } = new List<Courier>();
    public bool IsActive { get; set; } = true;

}

