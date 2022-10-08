using Webeditor.Application.DTOs.Recipes.Tags;
using Webeditor.Application.Interfaces.Application.Recipes;
using Webeditor.Application.Utils;
using Webeditor.Domain.Entities.Recipes;
using Webeditor.Domain.Interfaces.Recipes;
using Webeditor.Domain.Models;
using Webeditor.Domain.Models.Recipes;

namespace Webeditor.Application.Services.Recipes;

public class RecipeTagService : IRecipeTagService
{

  private readonly IRecipeTagRepository _recipeTagRepository;
  private readonly IRecipeCategoryRepository _recipeCategoryRepository;

  public RecipeTagService(IRecipeTagRepository recipeTagRepository, IRecipeCategoryRepository recipeCategoryRepository)
  {
    _recipeTagRepository = recipeTagRepository;
    _recipeCategoryRepository = recipeCategoryRepository;
  }

  public async Task<PaginationResultModel<RecipeTag>> GetAllAsync(long systemCompanyId, RecipeTagFilterModel filter, BasePaginationModel pagination)
  {
    try
    {
      return await _recipeTagRepository.GetAllAsync(systemCompanyId, filter, pagination);
    }
    catch (Exception err)
    {
      throw new Exception(err.Message);
    }
  }

  public async Task<RecipeTag> GetByGuidAsync(Guid guid, long systemCompanyId)
  {
    try
    {
      var recipeTag = await _recipeTagRepository.GetByGuidAsync(guid, systemCompanyId);
      if (recipeTag == null)
      {
        throw new ArgumentException("RecipeTag not found!");
      }
      return recipeTag;
    }
    catch (Exception err)
    {
      throw new Exception(err.Message);
    }
  }

  public async Task<RecipeTag> CreateAsync(CreateRecipeTagDto payload, long systemCompanyId)
  {
    try
    {
      var nameExists = await _recipeTagRepository.GetByNameAsync(payload.Name, payload.RecipeCategoryGuid, systemCompanyId);
      if (nameExists != null)
      {
        throw new ArgumentException("Invalid name, may you can try with another one.");
      }

      var recipeCategory = await _recipeCategoryRepository.GetByGuidAsync(payload.RecipeCategoryGuid, systemCompanyId);
      if (recipeCategory == null)
      {
        throw new ArgumentException("Invalid RecipeCategory, may you can try with another one.");
      }

      var recipeTag = new RecipeTag(payload.Name, payload.Active, recipeCategory.Id, systemCompanyId);

      await _recipeTagRepository.CreateAsync(recipeTag);
      return recipeTag;
    }
    catch
    {
      throw;
    }
  }

  public async Task<RecipeTag> UpdateAsync(UpdateRecipeTagDto payload, long systemCompanyId)
  {
    try
    {
      var tagExists = await _recipeTagRepository.GetByNameAsync(payload.Name, payload.RecipeCategoryGuid, systemCompanyId);

      if (tagExists != null && tagExists.Guid != payload.Guid)
      {
        throw new ArgumentException("Invalid name, may you can try with another one.");
      }

      var recipeTag = await _recipeTagRepository.GetByGuidAsync(payload.Guid, systemCompanyId);
      if (recipeTag == null)
      {
        throw new ArgumentException("RecipeTag not found!");
      }

      var recipeCategory = await _recipeCategoryRepository.GetByGuidAsync(payload.RecipeCategoryGuid, systemCompanyId);
      if (recipeCategory == null)
      {
        throw new ArgumentException("Invalid RecipeCategory, may you can try with another one.");
      }

      recipeTag.Update(payload.Name, recipeCategory.Id, payload.Active);

      await _recipeTagRepository.UpdateAsync(recipeTag);
      return recipeTag;
    }
    catch
    {
      throw;
    }
  }

  public async Task<RecipeTag> DeleteAsync(Guid guid, long systemCompanyId)
  {
    try
    {
      var recipeTag = await _recipeTagRepository.GetByGuidAsync(guid, systemCompanyId);
      if (recipeTag == null)
      {
        throw new ArgumentException("RecipeTag not found!");
      }

      recipeTag.Delete();

      await _recipeTagRepository.UpdateAsync(recipeTag);
      return recipeTag;
    }
    catch
    {
      throw;
    }
  }
}
