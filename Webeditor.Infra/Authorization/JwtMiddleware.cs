using Microsoft.AspNetCore.Http;
using Webeditor.Application.Interfaces.Application.System;
using Webeditor.Domain.Interfaces.Infra;

namespace Webeditor.Infra;

public class JwtMiddleware
{
  private readonly RequestDelegate _next;

  public JwtMiddleware(RequestDelegate next)
  {
    _next = next;
  }

  public async Task Invoke(HttpContext context, ISystemUserService userService, ITokenProvider tokenProvider)
  {
    var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
    var claim = tokenProvider.Validate(token);
    if (claim != null && claim.UserGuid != Guid.Empty)
    {
      // attach user to context on successful jwt validation
      context.Items["User"] = await userService.GetByGuidAsync(claim.UserGuid, claim.CompanyId);
    }

    await _next(context);
  }
}

