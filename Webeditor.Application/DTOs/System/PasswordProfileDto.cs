using System.ComponentModel.DataAnnotations;

namespace Webeditor.Application.DTOs.System;

public class PasswordProfileDto
{
  [Required(ErrorMessage = "{0} is required")]
  [MinLength(3)]
  [MaxLength(20)]
  public string? Current { get; set; }

  [Required(ErrorMessage = "{0} is required")]
  [MinLength(3)]
  [MaxLength(20)]
  public string? New { get; set; }

  [Required(ErrorMessage = "{0} is required")]
  [MinLength(3)]
  [MaxLength(20)]
  public string? Confirmation { get; set; }
}
