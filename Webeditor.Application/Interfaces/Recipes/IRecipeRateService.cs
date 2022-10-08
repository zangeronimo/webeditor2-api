using Webeditor.Domain.Entities.Recipes;
using Webeditor.Domain.Enuns;
using Webeditor.Domain.Models;
using Webeditor.Domain.Models.Recipes;

namespace Webeditor.Application.Interfaces.Application.Recipes;

public interface IRecipeRateService
{
  Task<PaginationResultModel<RecipeRate>> GetAllAsync(long systemCompanyId, RecipeRateFilterModel filter, BasePaginationModel pagination);

  Task<RecipeRate> SetStatusAsync(Guid Guid, ActiveEnum status, long systemCompanyId);

  Task<RecipeRate> DeleteAsync(Guid guid, long systemCompanyId);
}
