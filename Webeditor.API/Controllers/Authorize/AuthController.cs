using Microsoft.AspNetCore.Mvc;
using Webeditor.Application.DTOs.Authorize;
using Webeditor.Application.Interfaces.Application.Authorize;

namespace Webeditor.API.Controllers.Authorize;

[Route("api/v1/[controller]")]
[ApiController]
public class AuthController : Controller
{
  private readonly IAuthorizeService _authorizeService;

  public AuthController(IAuthorizeService authorizeService)
  {
    _authorizeService = authorizeService;
  }

  [HttpPost]
  public async Task<ActionResult<AuthorizeDTO>> Authorize([FromBody] AuthorizeCredentialDTO credential)
  {
    try
    {
      var token = await _authorizeService.AuthenticateAsync(credential);
      return Ok(token);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}
