namespace Webeditor.Domain.Models.Recipes;

public class RecipeFilterModel : BaseFilterModel
{
  public string? Word { get; set; }

  public long? RecipeCategoryId { get; set; }
}
