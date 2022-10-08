namespace Webeditor.Domain.Entities.System;

public sealed class SystemModule : EntityBase
{
  private SystemModule()
  { }

  public SystemModule(long id, Guid guid, string name)
  {
    Id = id;
    Guid = guid;
    Name = name;
  }

  public SystemModule(string name)
  {
    Name = name;
  }
  public string? Name { get; private set; }

  public ICollection<SystemCompany>? SystemCompanies { get; private set; }
}
