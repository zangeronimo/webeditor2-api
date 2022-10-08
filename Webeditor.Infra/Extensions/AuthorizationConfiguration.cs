using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Webeditor.Infra.Extensions
{
  public static class AuthorizationConfiguration
  {
    public static WebApplicationBuilder AddAuthentication(this WebApplicationBuilder builder)
    {
      builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
      {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
          ValidateIssuer = true,
          ValidateAudience = true,
          ValidAudience = builder.Configuration["Jwt:Audience"],
          ValidIssuer = builder.Configuration["Jwt:Issuer"],
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
      });
      return builder;
    }
  }
}
