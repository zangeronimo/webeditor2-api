namespace Webeditor.Domain.Entities.System;

public sealed class SystemCompany : EntityBase
{
  private SystemCompany()
  { }

  public SystemCompany(long id, Guid guid, string name)
  {
    Id = id;
    Guid = guid;
    Name = name;
  }
  public string? Name { get; private set; }

  public ICollection<SystemUser>? SystemUsers { get; private set; }

  public ICollection<SystemModule>? SystemModules { get; private set; }
}
