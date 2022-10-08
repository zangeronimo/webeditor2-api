using Webeditor.Domain.Entities.Recipes;
using Webeditor.Domain.Models;
using Webeditor.Domain.Models.Recipes;

namespace Webeditor.Domain.Interfaces.Recipes;

public interface IRecipeRateRepository : IBaseRepository<RecipeRate>
{
  Task<PaginationResultModel<RecipeRate>> GetAllAsync(long systemCompanyId, RecipeRateFilterModel filter, BasePaginationModel pagination);
}
