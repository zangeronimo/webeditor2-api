namespace Webeditor.Domain.Entities.Authorize;

public class Authorize
{
  private Authorize()
  { }

  public Authorize(long id, Guid guid, long companyId, string email, string password)
  {
    Id = id;
    Guid = guid;
    Email = email;
    Password = password;
    SystemCompanyId = companyId;
  }

  public long Id { get; private set; }

  public Guid Guid { get; set; }

  public string? Email { get; private set; }

  public long SystemCompanyId { get; private set; }

  public string? Password { get; private set; }
}
