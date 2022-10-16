namespace Webeditor.Domain.Models.Recipes;

public class RecipeTagFilterModel : BaseFilterModel
{
  public string? Word { get; set; }

  public Guid? RecipeCategoryGuid { get; set; }
}
