using System.ComponentModel.DataAnnotations;

namespace Webeditor.Application.DTOs.System;

public class ProfileDto
{
  [Required(ErrorMessage = "{0} is required")]
  [MinLength(3)]
  [MaxLength(200)]
  public string? Name { get; set; }

  [Required(ErrorMessage = "{0} is required")]
  [MinLength(3)]
  [MaxLength(200)]
  public string? Email { get; set; }

  public string? Avatar { get; set; }
}
