using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Webeditor.Domain.Entities.Recipes;
using Webeditor.Domain.Interfaces.Recipes;
using Webeditor.Domain.Models;
using Webeditor.Domain.Models.Recipes;
using Webeditor.Infra.Context;

namespace Webeditor.Infra.Repositories.Recipes;

public class RecipeTagRepository : BaseRepository<RecipeTag>, IRecipeTagRepository
{
  public RecipeTagRepository(AppDbContext context) : base(context)
  { }

  public async Task<PaginationResultModel<RecipeTag>> GetAllAsync(long systemCompanyId, RecipeTagFilterModel filter, BasePaginationModel pagination)
  {
    try
    {
      var query = DbSet.AsQueryable();

      query = query.Include(tag => tag.RecipeCategory);

      if (!string.IsNullOrEmpty(filter?.Word))
        query = query.Where(tag => tag.Name.Contains(filter.Word));

      if (filter?.RecipeCategoryGuid != null)
        query = query.Where(tag => tag.RecipeCategory.Guid == filter.RecipeCategoryGuid);

      if (filter?.Guid != null)
        query = query.Where(tag => tag.Guid == filter.Guid);

      if (filter?.Active != null)
        query = query.Where(tag => tag.Active == filter.Active);

      if (filter?.InitialDate != null)
        query = query.Where(tag => tag.CreatedAt >= filter.InitialDate);

      if (filter?.FinalDate != null)
        query = query.Where(tag => tag.CreatedAt <= filter.FinalDate);

      if (filter?.Asc == true)
        query = query.OrderBy(Ordenation(filter?.OrderBy));
      else
        query = query.OrderByDescending(Ordenation(filter?.OrderBy));

      query = query.Where(tag => tag.RemovedAt == null &&
        tag.SystemCompanyId == systemCompanyId);

      var Page = pagination?.Page ?? 0;
      var Items = pagination?.ItemsPerPage ?? 20;

      var Total = query.Count();
      query = query.Skip(Page * Items);
      query = query.Take(Items);

      var result = await query.ToListAsync();
      return new PaginationResultModel<RecipeTag>()
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

  private Expression<Func<RecipeTag, Object>> Ordenation(string? order)
  {
    if (!string.IsNullOrEmpty(order))
    {
      switch (order.ToLowerInvariant())
      {
        case "name":
          return x => x.Name;
        case "active":
          return x => x.Active;
      }
    }

    return x => x.CreatedAt;
  }

  public async Task<RecipeTag?> GetByNameAsync(string name, Guid recipeCategoryGuid, long systemCompanyId)
  {
    try
    {
      return await DbSet.Where(tag => tag.Name == name && tag.RecipeCategory.Guid == recipeCategoryGuid)
        .FirstOrDefaultAsync(tag => tag.RemovedAt == null && tag.SystemCompanyId == systemCompanyId);
    }
    catch
    {
      throw;
    }
  }

  public override async Task<RecipeTag?> GetByGuidAsync(Guid guid, long systemCompanyId)
  {
    try
    {
      return await DbSet.Where(tag => tag.RemovedAt == null &&
          tag.SystemCompanyId == systemCompanyId && tag.Guid == guid)
        .FirstOrDefaultAsync();
    }
    catch
    {
      throw;
    }
  }
}
