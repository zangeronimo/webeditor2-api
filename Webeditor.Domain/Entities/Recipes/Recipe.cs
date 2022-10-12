using System.Collections.ObjectModel;
using Webeditor.Domain.Enuns;

namespace Webeditor.Domain.Entities.Recipes;

public class Recipe : EntityBase
{
  private Recipe()
  {
    RecipeImages = new List<RecipeImage?>() { };
    RecipeRates = new List<RecipeRate?>() { };
  }

  public Recipe(string slug, string name, string ingredients, string preparation, ActiveEnum active, long systemCompanyId)
  {
    Slug = slug;
    Name = name;
    Ingredients = ingredients;
    Preparation = preparation;
    Active = active;
    SystemCompanyId = systemCompanyId;
    RecipeImages = new List<RecipeImage?>() { };
    RecipeRates = new List<RecipeRate?>() { };
  }

  public string? Slug { get; private set; }

  public string? Name { get; private set; }

  public string? Ingredients { get; private set; }

  public string? Preparation { get; private set; }

  public long RecipeCategoryId { get; private set; }

  public long SystemCompanyId { get; private set; }

  public virtual RecipeCategory? RecipeCategory { get; private set; }

  public ICollection<RecipeImage?> RecipeImages { get; private set; }

  public ICollection<RecipeRate?> RecipeRates { get; private set; }

  public ICollection<RecipeTag> RecipeTags { get; private set; }

  public void AddCategory(RecipeCategory recipeCategory)
  {
    RecipeCategory = recipeCategory;
  }

  public void AddTag(RecipeTag tag)
  {
    if (RecipeTags == null)
    {
      RecipeTags = new Collection<RecipeTag>();
    }

    if (!RecipeTags.Contains(tag))
    {
      RecipeTags.Add(tag);
    }
  }

  public void AddImages(ICollection<RecipeImage> recipeImages)
  {
    RecipeImages = recipeImages;
  }

  public void Delete()
  {
    RemovedAt = DateTime.Now;
  }

  public void Update(string name, string ingredientes, string preparation, ActiveEnum active)
  {
    Name = name;
    Ingredients = ingredientes;
    Preparation = preparation;
    Active = active;
    UpdatedAt = DateTime.Now;
  }
}
