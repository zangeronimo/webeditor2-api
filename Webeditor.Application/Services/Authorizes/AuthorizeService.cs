using Webeditor.Domain.Interfaces.Infra;
using Webeditor.Application.DTOs.Authorize;
using Webeditor.Application.Interfaces.Application.Authorize;
using Webeditor.Domain.Entities.Authorize;
using Webeditor.Domain.Entities.System;
using Webeditor.Domain.Interfaces.System;

namespace Webeditor.Application.Services.Authorizes;

public class AuthorizeService : IAuthorizeService
{
  private readonly ISystemUserRepository _systemUserRepository;
  private readonly IHashProvider _hashProvider;
  private readonly ITokenProvider _tokenProvider;

  public AuthorizeService(
    IHashProvider hashProvider,
    ITokenProvider tokenProvider,
    ISystemUserRepository systemUserRepository)
  {
    _hashProvider = hashProvider;
    _tokenProvider = tokenProvider;
    _systemUserRepository = systemUserRepository;
  }

  public async Task<AuthorizeDTO> AuthenticateAsync(AuthorizeCredentialDTO credential)
  {
    Authorize? login = await _systemUserRepository.GetByEmailAsync(credential.Email);
    if (login == null)
    {
      throw new Exception("Your login or password has invalid");
    }

    if (!_hashProvider.Verify(login.Password ?? "", credential.Password ?? ""))
    {
      throw new Exception("Your login or password has invalid");
    }

    var user = await _systemUserRepository.GetByGuidAsync(login.Guid, login.SystemCompanyId);

    if (user == null)
    {
      throw new Exception("Your login or password has invalid");
    }

    var claimUser = new ClaimUser(user.Guid, user.SystemCompanyId, user.Name, user.Email, user.Avatar, GetRolesList(user.SystemRoles));

    return new AuthorizeDTO() { Token = _tokenProvider.Generate(claimUser) };
  }

  private List<string?> GetRolesList(ICollection<SystemRole?> roles)
  {
    var result = new List<string?>();

    foreach (var role in roles)
    {
      if (role != null)
        result.Add(role.Name);
    };

    return result;
  }
}

