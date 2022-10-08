using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Webeditor.Domain.Interfaces.Infra;
using Webeditor.Domain.Entities.Authorize;

namespace Webeditor.Infra.Providers.TokenProvider;


public class TokenProvider : ITokenProvider
{
  public IConfiguration _configuration;

  public TokenProvider(IConfiguration config)
  {
    _configuration = config;
  }

  public string Generate(ClaimUser user)
  {
    var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("Guid", user.Guid.ToString()),
                        new Claim("Name", user.Name ?? ""),
                        new Claim("Avatar", user.Avatar ?? ""),
                        new Claim("CompanyId", user.CompanyId.ToString()),
                        new Claim("Email", user.Email ?? ""),
                        new Claim("Roles", JsonSerializer.Serialize(user.Roles))
                    };

    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
    var token = new JwtSecurityToken(
        _configuration["Jwt:Issuer"],
        _configuration["Jwt:Audience"],
        claims,
        expires: DateTime.UtcNow.AddDays(1),
        signingCredentials: signIn);

    return new JwtSecurityTokenHandler().WriteToken(token);
  }

  public ValidateResultModel Validate(string token)
  {
    if (token == null)
      return new ValidateResultModel();

    var tokenHandler = new JwtSecurityTokenHandler();
    var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
    try
    {
      var securityKey = new SymmetricSecurityKey(key);
      tokenHandler.ValidateToken(token, new TokenValidationParameters
      {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = securityKey,
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero
      }, out SecurityToken validatedToken);

      var jwtToken = (JwtSecurityToken)validatedToken;
      var userGuid = Guid.Parse(jwtToken.Claims.First(x => x.Type == "Guid").Value);
      var companyId = long.Parse(jwtToken.Claims.First(x => x.Type == "CompanyId").Value);

      return new ValidateResultModel(userGuid, companyId);
    }
    catch
    {
      return new ValidateResultModel();
    }
  }
}
