using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Webeditor.Domain.Entities.Recipes;
using Webeditor.Domain.Interfaces.Recipes;
using Webeditor.Domain.Models;
using Webeditor.Domain.Models.Recipes;
using Webeditor.Infra.Context;

namespace Webeditor.Infra.Repositories.Recipes;

public class RecipeImageRepository : BaseRepository<RecipeImage>, IRecipeImageRepository
{
  public RecipeImageRepository(AppDbContext context) : base(context)
  { }

  public async Task<PaginationResultModel<RecipeImage>> GetAllAsync(long systemCompanyId, RecipeImageFilterModel filter, BasePaginationModel pagination)
  {
    try
    {
      var query = DbSet.AsQueryable();

      var Page = pagination?.Page ?? 0;
      var Items = pagination?.ItemsPerPage ?? 20;

      if (filter?.Guid != null)
        query = query.Where(image => image.Guid == filter.Guid);

      if (filter?.Active != null)
        query = query.Where(image => image.Active == filter.Active);

      if (filter?.InitialDate != null)
        query = query.Where(image => image.CreatedAt >= filter.InitialDate);

      if (filter?.FinalDate != null)
        query = query.Where(image => image.CreatedAt <= filter.FinalDate);

      if (filter?.Asc == true)
        query = query.OrderBy(Ordenation(filter?.OrderBy));
      else
        query = query.OrderByDescending(Ordenation(filter?.OrderBy));

      query = query.Where(image => image.RemovedAt == null &&
        image.SystemCompanyId == systemCompanyId);

      var Total = query.Count();
      query = query.Skip(Page * Items);
      query = query.Take(Items);

      var result = await query.ToListAsync();
      return new PaginationResultModel<RecipeImage>()
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

  private Expression<Func<RecipeImage, Object>> Ordenation(string? order)
  {
    if (!string.IsNullOrEmpty(order))
    {
      switch (order.ToLowerInvariant())
      {
        case "active":
          return x => x.Active;
      }
    }

    return x => x.CreatedAt;
  }

  public async Task<ICollection<RecipeImage>> GetAllByRecipeAsync(long recipeId, long systemCompanyId)
  {
    try
    {
      return await DbSet.Where(image => image.RemovedAt == null &&
        image.RecipeId == recipeId &&
        image.SystemCompanyId == systemCompanyId)
        .ToListAsync();
    }
    catch
    {
      throw;
    }
  }

  public override async Task<RecipeImage?> GetByGuidAsync(Guid guid, long systemCompanyId)
  {
    try
    {
      return await DbSet.Where(image => image.RemovedAt == null &&
        image.SystemCompanyId == systemCompanyId && image.Guid == guid)
        .FirstOrDefaultAsync();
    }
    catch
    {
      throw;
    }
  }
}
