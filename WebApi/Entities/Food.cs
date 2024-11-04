using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities;

public class Food
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Title { get; set; }
#nullable enable
    public string? Description { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }
#nullable enable
    public string? ImgUrl { get; set; }
#nullable enable
    public int? RestaurantId { get; set; }
    public Restaurant? Restaurant { get; set; }
    public List<Quantity> Quantities { get; set; } = new List<Quantity>();
    public bool IsActive { get; set; } = true;

}