using Webeditor.Application.DTOs.Recipes.Categories;
using Webeditor.Application.DTOs.Recipes.Images;
using Webeditor.Application.DTOs.Recipes.Rates;
using Webeditor.Application.DTOs.Recipes.Tags;
using Webeditor.Domain.Enuns;

namespace Webeditor.Application.DTOs.Recipes;

public class RecipeDto
{
  private RecipeDto()
  {
    RecipeTags = new List<RecipeTagDto?>() { };
    RecipeImages = new List<RecipeImageDto?>() { };
    RecipeRates = new List<RecipeRateDto?>() { };
  }

  public Guid Guid { get; set; }

  public string? Slug { get; set; }

  public string? Name { get; set; }

  public string? Ingredients { get; set; }

  public string? Preparation { get; set; }

  public ActiveEnum Active { get; set; }

  public RecipeCategoryDto RecipeCategory { get; set; }

  public List<RecipeTagDto?> RecipeTags { get; set; }

  public List<RecipeImageDto?> RecipeImages { get; set; }

  public List<RecipeRateDto?> RecipeRates { get; set; }
}
