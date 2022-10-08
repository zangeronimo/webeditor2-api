using Webeditor.Domain.Entities.Recipes;
using Webeditor.Domain.Enuns;
using Webeditor.Domain.Models;
using Webeditor.Domain.Models.Recipes;

namespace Webeditor.Application.Interfaces.Application.Recipes;

public interface IRecipeImageService
{
  Task<PaginationResultModel<RecipeImage>> GetAllAsync(long systemCompanyId, RecipeImageFilterModel filter, BasePaginationModel pagination);

  Task<RecipeImage> SetStatusAsync(Guid Guid, ActiveEnum status, long systemCompanyId);

  Task<RecipeImage> DeleteAsync(Guid guid, long systemCompanyId);
}
