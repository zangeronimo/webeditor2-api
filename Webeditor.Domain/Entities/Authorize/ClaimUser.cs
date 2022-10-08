namespace Webeditor.Domain.Entities.Authorize;

public class ClaimUser
{
  private ClaimUser()
  {
    Roles = new List<string?> { };
  }

  public ClaimUser(Guid guid, long companyId, string? name, string? email, string? avatar, List<string?> roles)
  {
    Guid = guid;
    Name = name;
    Email = email;
    Avatar = avatar;
    CompanyId = companyId;
    Roles = roles;
  }

  public Guid Guid { get; private set; }
  public string? Name { get; private set; }
  public string? Email { get; private set; }
  public string? Avatar { get; set; }
  public long CompanyId { get; private set; }
  public List<string?> Roles { get; private set; }
}
