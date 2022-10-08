using Webeditor.Domain.Interfaces.Infra;

namespace Webeditor.Infra.Providers.HashProvider;

public class HashProvider : IHashProvider
{
  public string Encript(string value)
  {
    try
    {
      return BCrypt.Net.BCrypt.HashPassword(value);
    }
    catch
    {
      throw new ArgumentException("[CRYPT] Anything wrong happened!");
    }
  }

  public Boolean Verify(string hash, string compare)
  {
    try
    {
      return BCrypt.Net.BCrypt.Verify(compare, hash);
    }
    catch
    {
      throw new ArgumentException("[CRYPT] Anything wrong happened!");
    }
  }
}
