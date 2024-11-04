using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities;

public class Quantity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int Quantities { get; set; }
    public int? OrderId { get; set; }
    public Order Order { get; set; }
    public int? FoodId { get; set; }
    public Food Food { get; set; }
}