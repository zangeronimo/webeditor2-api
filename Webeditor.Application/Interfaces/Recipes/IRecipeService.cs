using Webeditor.Application.DTOs.Recipes;
using Webeditor.Domain.Entities.Recipes;
using Webeditor.Domain.Models;
using Webeditor.Domain.Models.Recipes;

namespace Webeditor.Application.Interfaces.Application.Recipes;

public interface IRecipeService
{
  Task<PaginationResultModel<Recipe>> GetAllAsync(long systemCompanyId, RecipeFilterModel filter, BasePaginationModel pagination);

  Task<Recipe> GetByGuidAsync(Guid guid, long systemCompanyId);

  Task<Recipe> CreateAsync(CreateRecipeDto payload, long systemCompanyId);

  Task<Recipe> UpdateAsync(UpdateRecipeDto payload, long systemCompanyId);

  Task<Recipe> DeleteAsync(Guid guid, long systemCompanyId);
}
