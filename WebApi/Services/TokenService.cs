using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using WebApi.Entities;

namespace WebApi.Services;

public class TokenService
{
    public IConfiguration Configuration { get; set; }
    public TokenService(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    public Token CreateAccessToken(User user)
    {
        var claims = new Claim[]
         {
             new Claim(ClaimTypes.Name, user.Id.ToString()),
             new Claim(ClaimTypes.Role, user.Role.ToString()),
         };
        Token tokenModel = new Token();
        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:SecurityKey"]));
        SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        tokenModel.Expiration = DateTime.Now.AddMinutes(15);
        JwtSecurityToken securityToken = new JwtSecurityToken(
            issuer: Configuration["Token:Issuer"],
            audience: Configuration["Token:Audience"],
            expires: tokenModel.Expiration,
            notBefore: DateTime.Now,
            signingCredentials: credentials,
            claims: claims
        );
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

        tokenModel.AccessToken = tokenHandler.WriteToken(securityToken);
        tokenModel.RefreshToken = CreateRefreshToken();
        return tokenModel;
    }
    public string CreateRefreshToken()
    {
        return Guid.NewGuid().ToString();
    }
}