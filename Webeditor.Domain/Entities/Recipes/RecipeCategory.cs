using Webeditor.Domain.Enuns;

namespace Webeditor.Domain.Entities.Recipes;

public class RecipeCategory : EntityBase
{
  private RecipeCategory()
  { }

  public RecipeCategory(string slug, string name, ActiveEnum active, long systemCompanyId)
  {
    Slug = slug;
    Name = name;
    Active = active;
    SystemCompanyId = systemCompanyId;
  }

  public string? Slug { get; private set; }

  public string? Name { get; private set; }

  public virtual ICollection<RecipeTag>? RecipeTags { get; private set; }

  public long SystemCompanyId { get; private set; }

  public void AddTags(List<RecipeTag> recipeTags)
  {
    RecipeTags = recipeTags;
  }

  public void Delete()
  {
    RemovedAt = DateTime.Now;
  }

  public void Update(string name, ActiveEnum active)
  {
    Name = name;
    Active = active;
    UpdatedAt = DateTime.Now;
  }
}
