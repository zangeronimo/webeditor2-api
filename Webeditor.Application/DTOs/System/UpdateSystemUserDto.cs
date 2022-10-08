using System.ComponentModel.DataAnnotations;
using Webeditor.Domain.Enuns;

namespace Webeditor.Application.DTOs.System;

public class UpdateSystemUserDto
{
  public Guid Guid { get; set; }

  [Required(ErrorMessage = "{0} is required")]
  [MinLength(3)]
  [MaxLength(200)]
  public string? Name { get; set; }

  [Required(ErrorMessage = "{0} is required")]
  [MinLength(3)]
  [MaxLength(200)]
  public string? Email { get; set; }

  public string? Password { get; set; }

  [Required(ErrorMessage = "{0} is required")]
  public ActiveEnum Active { get; set; }

  public string? PasswordConfirmation { get; set; }

  public List<Guid>? SystemRolesGuid { get; set; }
}
