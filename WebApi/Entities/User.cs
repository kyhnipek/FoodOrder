using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities;

public class User
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string PasswordSalt { get; set; }
    public string PasswordHash { get; set; }
#nullable enable
    public string? RefreshToken { get; set; }
#nullable enable
    public DateTime? RefreshTokenExpireDate { get; set; }
#nullable enable
    public string? Adress { get; set; }
#nullable enable
    public string? Phone { get; set; }
    public States State { get; set; }
    public Cities City { get; set; }
    public ICollection<Order>? Orders { get; set; }
    public Role Role { get; set; } = Role.Customer;
    public bool IsActive { get; set; } = true;
}

