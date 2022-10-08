using Webeditor.Domain.Enuns;

namespace Webeditor.Application.DTOs.Recipes.Categories;

public class RecipeCategoryDto
{
  public Guid Guid { get; set; }

  public string? Slug { get; set; }

  public string? Name { get; set; }

  public ActiveEnum Active { get; set; }
}
