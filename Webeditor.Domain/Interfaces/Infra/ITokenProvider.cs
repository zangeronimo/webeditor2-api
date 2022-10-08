using Webeditor.Domain.Entities.Authorize;

namespace Webeditor.Domain.Interfaces.Infra;

public interface ITokenProvider
{
  string Generate(ClaimUser user);

  ValidateResultModel Validate(string token);
}

