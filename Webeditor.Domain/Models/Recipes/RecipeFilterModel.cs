namespace Webeditor.Domain.Models.Recipes;

public class RecipeFilterModel : BaseFilterModel
{
  public string? Word { get; set; }

  public Guid? RecipeCategoryGuid { get; set; }
}
