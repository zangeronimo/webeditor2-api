using System.ComponentModel.DataAnnotations;

namespace Webeditor.Application.DTOs.Authorize
{
  public class AuthorizeCredentialDTO
  {
    [Required(ErrorMessage = "Email is required")]
    [MinLength(3)]
    [MaxLength(200)]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [MinLength(3)]
    [MaxLength(10)]
    public string? Password { get; set; }
  }
}
