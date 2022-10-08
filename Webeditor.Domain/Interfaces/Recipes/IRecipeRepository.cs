using Webeditor.Domain.Entities.Recipes;
using Webeditor.Domain.Models;
using Webeditor.Domain.Models.Recipes;

namespace Webeditor.Domain.Interfaces.Recipes;

public interface IRecipeRepository : IBaseRepository<Recipe>
{
  Task<PaginationResultModel<Recipe>> GetAllAsync(long systemCompanyId, RecipeFilterModel filter, BasePaginationModel pagination);

  Task<Recipe?> GetBySlugAsync(string slug, long systemCompanyId);
}
