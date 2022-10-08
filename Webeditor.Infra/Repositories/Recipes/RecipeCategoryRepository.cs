using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Webeditor.Domain.Entities.Recipes;
using Webeditor.Domain.Interfaces.Recipes;
using Webeditor.Domain.Models;
using Webeditor.Domain.Models.Recipes;
using Webeditor.Infra.Context;

namespace Webeditor.Infra.Repositories.Recipes;

public class RecipeCategoryRepository : BaseRepository<RecipeCategory>, IRecipeCategoryRepository
{
  public RecipeCategoryRepository(AppDbContext context) : base(context)
  { }

  public async Task<PaginationResultModel<RecipeCategory>> GetAllAsync(long systemCompanyId, RecipeCategoryFilterModel filter, BasePaginationModel pagination)
  {
    try
    {
      var query = DbSet.AsQueryable();

      if (!string.IsNullOrEmpty(filter?.Word))
        query = query.Where(category => category.Slug.Contains(filter.Word) || category.Name.Contains(filter.Word));

      if (filter?.Guid != null)
        query = query.Where(category => category.Guid == filter.Guid);

      if (filter?.Active != null)
        query = query.Where(category => category.Active == filter.Active);

      if (filter?.InitialDate != null)
        query = query.Where(category => category.CreatedAt >= filter.InitialDate);

      if (filter?.FinalDate != null)
        query = query.Where(category => category.CreatedAt <= filter.FinalDate);

      if (filter?.Asc == true)
        query = query.OrderBy(Ordenation(filter?.OrderBy));
      else
        query = query.OrderByDescending(Ordenation(filter?.OrderBy));

      query = query
        .Where(category => category.RemovedAt == null &&
          category.SystemCompanyId == systemCompanyId);

      var Page = pagination?.Page ?? 0;
      var Items = pagination?.ItemsPerPage ?? 20;

      var Total = query.Count();
      query = query.Skip(Page * Items);
      query = query.Take(Items);

      var result = await query.ToListAsync();
      return new PaginationResultModel<RecipeCategory>()
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

  private Expression<Func<RecipeCategory, Object>> Ordenation(string? order)
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

  public override async Task<RecipeCategory?> GetByGuidAsync(Guid guid, long systemCompanyId)
  {
    try
    {
      return await DbSet
        .Where(category => category.RemovedAt == null &&
          category.SystemCompanyId == systemCompanyId && category.Guid == guid)
        .FirstOrDefaultAsync();
    }
    catch
    {
      throw;
    }
  }
  public async Task<RecipeCategory?> GetByNameAsync(string name, long systemCompanyId)
  {
    try
    {
      return await DbSet.Where(category => category.RemovedAt == null &&
        category.SystemCompanyId == systemCompanyId &&
        category.Name == name)
        .FirstOrDefaultAsync();
    }
    catch
    {
      throw;
    }
  }

  public async Task<RecipeCategory?> GetBySlugAsync(string slug, long systemCompanyId)
  {
    try
    {
      return await DbSet.Where(category => category.RemovedAt == null &&
        category.SystemCompanyId == systemCompanyId && category.Slug == slug)
        .FirstOrDefaultAsync();
    }
    catch
    {
      throw;
    }
  }
}
