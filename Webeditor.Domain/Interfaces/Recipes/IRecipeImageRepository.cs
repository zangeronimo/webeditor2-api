using Webeditor.Domain.Entities.Recipes;
using Webeditor.Domain.Models;
using Webeditor.Domain.Models.Recipes;

namespace Webeditor.Domain.Interfaces.Recipes;

public interface IRecipeImageRepository : IBaseRepository<RecipeImage>
{
  Task<PaginationResultModel<RecipeImage>> GetAllAsync(long systemCompanyId, RecipeImageFilterModel filter, BasePaginationModel pagination);

  Task<ICollection<RecipeImage>> GetAllByRecipeAsync(long recipeId, long systemCompanyId);
}
