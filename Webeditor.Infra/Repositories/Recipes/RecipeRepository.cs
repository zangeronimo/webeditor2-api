using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Webeditor.Domain.Entities.Recipes;
using Webeditor.Domain.Interfaces.Recipes;
using Webeditor.Domain.Models;
using Webeditor.Domain.Models.Recipes;
using Webeditor.Infra.Context;

namespace Webeditor.Infra.Repositories.Recipes;

public class RecipeRepository : BaseRepository<Recipe>, IRecipeRepository
{
  public RecipeRepository(AppDbContext context) : base(context)
  { }

  public async Task<PaginationResultModel<Recipe>> GetAllAsync(long systemCompanyId, RecipeFilterModel filter, BasePaginationModel pagination)
  {
    try
    {
      var query = DbSet.AsQueryable();

      if (!string.IsNullOrEmpty(filter?.Word))
        query = query.Where(recipe => recipe.Slug.Contains(filter.Word) || recipe.Name.Contains(filter.Word));

      if (filter?.RecipeCategoryGuid != null)
        query = query.Where(recipe => recipe.RecipeCategory.Guid == filter.RecipeCategoryGuid);

      if (filter?.Guid != null)
        query = query.Where(recipe => recipe.Guid == filter.Guid);

      if (filter?.Active != null)
        query = query.Where(recipe => recipe.Active == filter.Active);

      if (filter?.InitialDate != null)
        query = query.Where(recipe => recipe.CreatedAt >= filter.InitialDate);

      if (filter?.FinalDate != null)
        query = query.Where(recipe => recipe.CreatedAt <= filter.FinalDate);

      if (filter?.Asc == true)
        query = query.OrderBy(Ordenation(filter?.OrderBy));
      else
        query = query.OrderByDescending(Ordenation(filter?.OrderBy));

      query = query.Include(recipe => recipe.RecipeCategory)
        .Include(recipe => recipe.RecipeImages.Where(i => i.RemovedAt == null))
        .Include(recipe => recipe.RecipeRates.Where(r => r.RemovedAt == null))
        .Include(recipe => recipe.RecipeTags.Where(t => t.RemovedAt == null))
        .Where(recipe => recipe.RemovedAt == null &&
          recipe.SystemCompanyId == systemCompanyId &&
          recipe.RecipeCategory.RemovedAt == null);

      var Page = pagination?.Page ?? 0;
      var Items = pagination?.ItemsPerPage ?? 20;

      var Total = query.Count();
      query = query.Skip(Page * Items);
      query = query.Take(Items);

      var result = await query.ToListAsync();
      return new PaginationResultModel<Recipe>()
      {
        Result = await query.ToListAsync(),
        Page = Page,
        ItemsPerPage = Items,
        Total = Total
      };
    }
    catch
    {
      throw;
    }
  }

  private Expression<Func<Recipe, Object>> Ordenation(string? order)
  {
    if (!string.IsNullOrEmpty(order))
    {
      switch (order.ToLowerInvariant())
      {
        case "name":
          return x => x.Name;
        case "slug":
          return x => x.Slug;
        case "active":
          return x => x.Active;
      }
    }

    return x => x.CreatedAt;
  }

  public override async Task<Recipe?> GetByGuidAsync(Guid guid, long systemCompanyId)
  {
    try
    {
      return await DbSet
        .Include(recipe => recipe.RecipeCategory)
          .ThenInclude(c => c.RecipeTags)
        .Include(recipe => recipe.RecipeImages.Where(image => image.RemovedAt == null))
        .Include(recipe => recipe.RecipeRates.Where(rate => rate.RemovedAt == null))
        .Include(recipe => recipe.RecipeTags.Where(tag => tag.RemovedAt == null))
        .Where(recipe => recipe.RemovedAt == null &&
          recipe.SystemCompanyId == systemCompanyId && recipe.Guid == guid &&
          recipe.RecipeCategory.RemovedAt == null)
        .FirstOrDefaultAsync();
    }
    catch
    {
      throw;
    }
  }

  public async Task<Recipe?> GetBySlugAsync(string slug, long systemCompanyId)
  {
    try
    {
      return await DbSet.Where(recipe => recipe.RemovedAt == null &&
        recipe.SystemCompanyId == systemCompanyId && recipe.Slug == slug)
        .FirstOrDefaultAsync();
    }
    catch
    {
      throw;
    }
  }
}
