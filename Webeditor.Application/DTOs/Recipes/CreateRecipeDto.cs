using System.ComponentModel.DataAnnotations;
using Webeditor.Domain.Enuns;

namespace Webeditor.Application.DTOs.Recipes;

public class CreateRecipeDto
{
  [Required(ErrorMessage = "{0} is required")]
  [MinLength(3)]
  [MaxLength(80)]
  public string? Name { get; set; }

  [Required(ErrorMessage = "{0} is required")]
  public string? Ingredients { get; set; }

  [Required(ErrorMessage = "{0} is required")]
  public string? Preparation { get; set; }

  [Required(ErrorMessage = "{0} is required")]
  public ActiveEnum Active { get; set; }

  [Required(ErrorMessage = "{0} is required")]
  public Guid RecipeCategoryGuid { get; set; }

  public List<Guid>? Tags { get; set; }

  public string? Image { get; set; }
}
