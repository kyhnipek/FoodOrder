using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.Services;

namespace WebApi.Application.UserOperations.Login;

public class LoginCommand
{
    private readonly IFoodOrderDBContext _context;
    private readonly IConfiguration _configuration;
    public UserLoginModel Model { get; set; }
    public LoginCommand(IFoodOrderDBContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public Token Handle()
    {
        var user = _context.Users.Where(u => u.IsActive).FirstOrDefault(u => u.Email == Model.Email);
        if (user is null)
            throw new InvalidOperationException("Kullanıcı bulunamadı.");

        bool verifyPassword = PasswordService.VerifyPassword(Model.Password, user.PasswordHash, user.PasswordSalt);
        if (!verifyPassword)
            throw new InvalidOperationException("Email/Şifre hatalı.");

        TokenService tk = new TokenService(_configuration);
        Token token = tk.CreateAccessToken(user);

        user.RefreshToken = token.RefreshToken;
        user.RefreshTokenExpireDate = token.Expiration.AddDays(1);
        _context.SaveChanges();
        return token;
    }


}
public class UserLoginModel
{
    public string Email { get; set; }
    public string Password { get; set; }
}