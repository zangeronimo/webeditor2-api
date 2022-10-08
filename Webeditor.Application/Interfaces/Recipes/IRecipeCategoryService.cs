using Webeditor.Application.DTOs.Recipes.Categories;
using Webeditor.Domain.Entities.Recipes;
using Webeditor.Domain.Models;
using Webeditor.Domain.Models.Recipes;

namespace Webeditor.Application.Interfaces.Application.Recipes;

public interface IRecipeCategoryService
{
  Task<PaginationResultModel<RecipeCategory>> GetAllAsync(long systemCompanyId, RecipeCategoryFilterModel filter, BasePaginationModel pagination);

  Task<RecipeCategory> GetByGuidAsync(Guid guid, long systemCompanyId);

  Task<RecipeCategory> CreateAsync(CreateRecipeCategoryDto payload, long systemCompanyId);

  Task<RecipeCategory> UpdateAsync(UpdateRecipeCategoryDto payload, long systemCompanyId);

  Task<RecipeCategory> DeleteAsync(Guid guid, long systemCompanyId);
}
