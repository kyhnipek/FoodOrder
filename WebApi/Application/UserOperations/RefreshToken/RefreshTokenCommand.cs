using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.Services;

namespace WebApi.Application.UserOperations.RefreshToken;

public class RefreshTokenCommand
{
    private readonly IFoodOrderDBContext _context;
    private readonly IConfiguration _configuration;
    public string RefreshToken { get; set; }

    public RefreshTokenCommand(IFoodOrderDBContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public Token Handle()
    {
        var user = _context.Users.FirstOrDefault(x => x.RefreshToken == RefreshToken && x.RefreshTokenExpireDate > DateTime.Now);
        if (user is null)

            throw new InvalidOperationException("Geçerli bir refresh token bulunamadı.");

        TokenService handler = new TokenService(_configuration);
        Token token = handler.CreateAccessToken(user);

        user.RefreshToken = token.RefreshToken;
        user.RefreshTokenExpireDate = token.Expiration.AddDays(1);
        _context.SaveChanges();
        return token;
    }
}