using Webeditor.Domain.Entities.Recipes;
using Webeditor.Domain.Models;
using Webeditor.Domain.Models.Recipes;

namespace Webeditor.Domain.Interfaces.Recipes;

public interface IRecipeTagRepository : IBaseRepository<RecipeTag>
{
  Task<PaginationResultModel<RecipeTag>> GetAllAsync(long systemCompanyId, RecipeTagFilterModel filter, BasePaginationModel pagination);

  Task<RecipeTag?> GetByNameAsync(string name, Guid recipeCategoryGuid, long systemCompanyId);
}
