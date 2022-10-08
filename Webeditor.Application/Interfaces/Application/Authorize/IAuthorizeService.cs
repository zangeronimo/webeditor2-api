using Webeditor.Application.DTOs.Authorize;

namespace Webeditor.Application.Interfaces.Application.Authorize;

public interface IAuthorizeService
{
  Task<AuthorizeDTO> AuthenticateAsync(AuthorizeCredentialDTO credential);
}
