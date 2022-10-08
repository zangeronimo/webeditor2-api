using Webeditor.Domain.Enuns;

namespace Webeditor.Domain.Entities.Recipes;

public class RecipeImage : EntityBase
{
  private RecipeImage()
  { }

  public RecipeImage(string imagePath, long recipeId, ActiveEnum active, long systemCompanyId)
  {
    Path = imagePath;
    RecipeId = recipeId;
    Active = active;
    SystemCompanyId = systemCompanyId;
  }

  public string? Path { get; private set; }

  public long RecipeId { get; private set; }

  public long SystemCompanyId { get; private set; }

  public void SetStatus(ActiveEnum status)
  {
    UpdatedAt = DateTime.Now;
    Active = status;
  }

  public void Delete()
  {
    RemovedAt = DateTime.Now;
  }
}
