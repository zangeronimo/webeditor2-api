using Webeditor.Domain.Enuns;

namespace Webeditor.Application.DTOs.Recipes.Rates;

public class RecipeRateDto
{
  public Guid Guid { get; set; }

  public int Rate { get; set; }

  public string? Comment { get; set; }

  public ActiveEnum Active { get; set; }
}
