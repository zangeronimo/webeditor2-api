namespace Webeditor.Domain.Interfaces.Infra;

public interface IHashProvider
{
  string Encript(string value);

  Boolean Verify(string hash, string compare);
}

