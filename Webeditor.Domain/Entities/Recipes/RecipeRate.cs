using Webeditor.Domain.Enuns;

namespace Webeditor.Domain.Entities.Recipes;

public class RecipeRate : EntityBase
{
  private RecipeRate()
  { }

  public int Rate { get; private set; }

  public string? Comment { get; private set; }

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
