using Webeditor.Domain.Enuns;

namespace Webeditor.Application.DTOs.Recipes.Tags;

public class RecipeTagDto
{
  public Guid Guid { get; set; }

  public string? Name { get; set; }

  public ActiveEnum Active { get; set; }

  public Guid RecipeCategoryGuid { get; set; }
}
