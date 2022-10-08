using System.ComponentModel.DataAnnotations;
using Webeditor.Domain.Enuns;

namespace Webeditor.Application.DTOs.System;

public class CreateSystemUserDto
{
  private CreateSystemUserDto()
  {
    SystemRolesGuid = new List<Guid?>() { };
  }

  [Required(ErrorMessage = "{0} is required")]
  [MinLength(3)]
  [MaxLength(200)]
  public string? Name { get; set; }

  [Required(ErrorMessage = "{0} is required")]
  [MinLength(3)]
  [MaxLength(200)]
  public string? Email { get; set; }

  [Required(ErrorMessage = "{0} is required")]
  [MinLength(3)]
  [MaxLength(20)]
  public string? Password { get; set; }

  [Required(ErrorMessage = "{0} is required")]
  [MinLength(3)]
  [MaxLength(20)]
  public string? PasswordConfirmation { get; set; }

  [Required(ErrorMessage = "{0} is required")]
  public ActiveEnum Active { get; set; }

  public List<Guid?> SystemRolesGuid { get; set; }
}
