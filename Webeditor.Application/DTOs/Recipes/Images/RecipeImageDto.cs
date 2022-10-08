using System.ComponentModel.DataAnnotations;
using Webeditor.Domain.Enuns;

namespace Webeditor.Application.DTOs.Recipes.Images;

public class RecipeImageDto
{
  public Guid Guid { get; set; }

  public string? Path { get; set; }

  public ActiveEnum Active { get; set; }
}
