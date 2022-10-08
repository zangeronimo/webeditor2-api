using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Webeditor.Infra.Authorization;

public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
  private readonly IList<string> _roles;

  public AuthorizeAttribute(params string[] roles)
  {
    _roles = roles ?? new string[] { };
  }

  public void OnAuthorization(AuthorizationFilterContext context)
  {
    // skip authorization if action is decorated with [AllowAnonymous] attribute
    var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
    if (allowAnonymous)
      return;

    // authorization
    var user = (SystemUser)context.HttpContext.Items["User"];
    var unauthorized = _roles.Any();
    if (user != null)
    {
      foreach (var role in user.SystemRoles)
      {
        if ((!string.IsNullOrEmpty(role.Name) && _roles.Contains(role.Name)))
          unauthorized = false;
      }

    }

    if (user == null || unauthorized)
    {
      context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
    }
  }
}
