using Webeditor.Domain.Enuns;

namespace Webeditor.Domain.Entities.Recipes;

public class RecipeTag : EntityBase
{
  private RecipeTag()
  { }

  public RecipeTag(string name, ActiveEnum active, long recipeCategoryId, long systemCompanyId)
  {
    Name = name;
    Active = active;
    RecipeCategoryId = recipeCategoryId;
    SystemCompanyId = systemCompanyId;
  }

  public string? Name { get; private set; }

  public long SystemCompanyId { get; private set; }

  public long RecipeCategoryId { get; private set; }

  public virtual RecipeCategory RecipeCategory { get; private set; }

  public virtual ICollection<Recipe?> Recipes { get; private set; }

  public void Delete()
  {
    RemovedAt = DateTime.Now;
  }

  public void Update(string name, long recipeCategoryId, ActiveEnum active)
  {
    Name = name;
    RecipeCategoryId = recipeCategoryId;
    Active = active;
    UpdatedAt = DateTime.Now;
  }
}
