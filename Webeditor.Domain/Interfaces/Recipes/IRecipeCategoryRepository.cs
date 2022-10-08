using Webeditor.Domain.Entities.Recipes;
using Webeditor.Domain.Models;
using Webeditor.Domain.Models.Recipes;

namespace Webeditor.Domain.Interfaces.Recipes;

public interface IRecipeCategoryRepository : IBaseRepository<RecipeCategory>
{
  Task<PaginationResultModel<RecipeCategory>> GetAllAsync(long systemCompanyId, RecipeCategoryFilterModel filter, BasePaginationModel pagination);

  Task<RecipeCategory?> GetByNameAsync(string name, long systemCompanyId);

  Task<RecipeCategory?> GetBySlugAsync(string slug, long systemCompanyId);
}
