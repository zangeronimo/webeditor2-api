using Webeditor.Application.DTOs.Recipes.Tags;
using Webeditor.Domain.Entities.Recipes;
using Webeditor.Domain.Models;
using Webeditor.Domain.Models.Recipes;

namespace Webeditor.Application.Interfaces.Application.Recipes;

public interface IRecipeTagService
{
  Task<PaginationResultModel<RecipeTag>> GetAllAsync(long systemCompanyId, RecipeTagFilterModel filter, BasePaginationModel pagination);

  Task<RecipeTag> GetByGuidAsync(Guid guid, long systemCompanyId);

  Task<RecipeTag> CreateAsync(CreateRecipeTagDto payload, long systemCompanyId);

  Task<RecipeTag> UpdateAsync(UpdateRecipeTagDto payload, long systemCompanyId);

  Task<RecipeTag> DeleteAsync(Guid guid, long systemCompanyId);
}
