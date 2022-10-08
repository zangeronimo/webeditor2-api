using Webeditor.Domain.Enuns;

namespace Webeditor.Application.DTOs.System;

public class SystemRoleDto
{
  public Guid Guid { get; set; }

  public string? Name { get; set; }

  public string? Label { get; set; }

  public ActiveEnum Active { get; set; }
}
