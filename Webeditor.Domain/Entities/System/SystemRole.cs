namespace Webeditor.Domain.Entities.System;

public sealed class SystemRole : EntityBase
{
  private SystemRole()
  { }

  public SystemRole(long id, Guid guid, string name, string label, long systemModuleId)
  {
    Id = id;
    Guid = guid;
    Name = name;
    Label = label;
    SystemModuleId = systemModuleId;
  }

  public string? Name { get; private set; }

  public string? Label { get; private set; }

  public long SystemModuleId { get; private set; }

  public SystemModule? SystemModule { get; private set; }

  public ICollection<SystemUser>? SystemUsers { get; private set; }
}
