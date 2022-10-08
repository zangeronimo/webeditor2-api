using System.Security.Claims;

namespace Webeditor.Infra.Extensions;

public static class JwtClaim
{
  public static long GetCompanyId(this ClaimsPrincipal user)
  {
    try
    {
      if (user == null)
      {
        throw new UnauthorizedAccessException();
      }

      return long.Parse(user.FindFirst("CompanyId")?.Value);
    }
    catch
    {
      throw new UnauthorizedAccessException("Unauthorized");
    }
  }
  public static Guid GetUserGuid(this ClaimsPrincipal user)
  {
    try
    {
      if (user == null)
      {
        throw new UnauthorizedAccessException();
      }

      return Guid.Parse(user.FindFirst("Guid")?.Value);
    }
    catch
    {
      throw new UnauthorizedAccessException("Unauthorized");
    }
  }
}

