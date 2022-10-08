
using Webeditor.Domain.Interfaces.Infra;
using Webeditor.Domain.Entities;
using Webeditor.Domain.Entities.System;

public sealed class SystemUser : EntityBase
{
  private SystemUser()
  {
    SystemRoles = new List<SystemRole?>() { };
  }

  public SystemUser(string? name, string? email, string? password, long systemCompanyId)
  {
    Name = name;
    Email = email;
    Password = password;
    SystemCompanyId = systemCompanyId;
    SystemRoles = new List<SystemRole?>() { };
  }
  public SystemUser(long id, Guid guid, string name, string email, string password, long systemCompanyId)
  {
    Id = id;
    Guid = guid;
    Name = name;
    Email = email;
    Password = password;
    SystemCompanyId = systemCompanyId;
    SystemRoles = new List<SystemRole?>() { };
  }

  public string? Name { get; private set; }

  public string? Email { get; private set; }

  public string? Avatar { get; private set; }

  public string? Password { get; private set; }

  public long SystemCompanyId { get; private set; }

  public SystemCompany? SystemCompany { get; private set; }

  public ICollection<SystemRole?> SystemRoles { get; private set; }

  public void AddRoles(List<SystemRole?> roles)
  {
    SystemRoles = roles;
  }

  public void Update(string name, string email)
  {
    Name = name;
    Email = email;
    UpdatedAt = DateTime.Now;
  }

  public void SetPassword(string password)
  {
    Password = password;
  }

  public void SetAvatar(string avatar)
  {
    Avatar = avatar;
  }

  public void Delete()
  {
    RemovedAt = DateTime.Now;
  }

  public void EncryptPassword(IHashProvider hashProvider)
  {
    if (string.IsNullOrEmpty(Password))
      throw new ArgumentException("Invalid operation, password can't be null.");

    Password = hashProvider.Encript(Password);
  }
}
