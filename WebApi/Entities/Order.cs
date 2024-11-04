using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities;

public class Order
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int UserId { get; set; }
    public User Users { get; set; }
    public ICollection<Food> Foods { get; set; }
    public int RestaurantId { get; set; }
    public Restaurant Restaurant { get; set; }
    public DateTime OrderDate { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal OrderTotal { get; set; }
    public Status OrderStatus { get; set; } = Status.NewOrder;
    public ICollection<Quantity> Quantities { get; set; } = new List<Quantity>();
    public bool IsActive { get; set; } = true;
    public int CourierId { get; set; }
    public Courier Couriers { get; set; }
}