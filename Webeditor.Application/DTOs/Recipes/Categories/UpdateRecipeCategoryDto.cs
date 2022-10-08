using System.ComponentModel.DataAnnotations;
using Webeditor.Domain.Enuns;

namespace Webeditor.Application.DTOs.Recipes.Categories;

public class UpdateRecipeCategoryDto
{
  [Required(ErrorMessage = "{0} is required")]
  public Guid Guid { get; set; }

  [Required(ErrorMessage = "{0} is required")]
  [MinLength(3)]
  [MaxLength(45)]
  public string? Name { get; set; }

  [Required(ErrorMessage = "{0} is required")]
  public ActiveEnum Active { get; set; }
}
