using Webeditor.Domain.Enuns;

namespace Webeditor.Application.DTOs.System;

public class SystemUserDto
{
  private SystemUserDto()
  {
    SystemRoles = new List<SystemRoleDto?> { };
  }

  public Guid Guid { get; set; }

  public string? Name { get; set; }

  public string? Email { get; set; }

  public string? Avatar { get; set; }

  public ActiveEnum Active { get; set; }

  public List<SystemRoleDto?> SystemRoles { get; set; }
}
