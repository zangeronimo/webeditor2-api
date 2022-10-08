using Webeditor.Application.DTOs.Recipes.Categories;
using Webeditor.Application.Interfaces.Application.Recipes;
using Webeditor.Application.Utils;
using Webeditor.Domain.Entities.Recipes;
using Webeditor.Domain.Interfaces.Recipes;
using Webeditor.Domain.Models;
using Webeditor.Domain.Models.Recipes;

namespace Webeditor.Application.Services.Recipes;

public class RecipeCategoryService : IRecipeCategoryService
{

  private readonly IRecipeCategoryRepository _recipeCategoryRepository;

  private readonly IRecipeTagRepository _recipeTagRepository;

  public RecipeCategoryService(IRecipeCategoryRepository recipeCategoryRepository, IRecipeTagRepository recipeTagRepository)
  {
    _recipeCategoryRepository = recipeCategoryRepository;

    _recipeTagRepository = recipeTagRepository;
  }

  public async Task<PaginationResultModel<RecipeCategory>> GetAllAsync(long systemCompanyId, RecipeCategoryFilterModel filter, BasePaginationModel pagination)
  {
    try
    {
      return await _recipeCategoryRepository.GetAllAsync(systemCompanyId, filter, pagination);
    }
    catch (Exception err)
    {
      throw new Exception(err.Message);
    }
  }

  public async Task<RecipeCategory> GetByGuidAsync(Guid guid, long systemCompanyId)
  {
    try
    {
      var recipeCategory = await _recipeCategoryRepository.GetByGuidAsync(guid, systemCompanyId);
      if (recipeCategory == null)
      {
        throw new ArgumentException("RecipeCategory not found!");
      }
      return recipeCategory;
    }
    catch (Exception err)
    {
      throw new Exception(err.Message);
    }
  }

  public async Task<RecipeCategory> CreateAsync(CreateRecipeCategoryDto payload, long systemCompanyId)
  {
    try
    {
      var nameExists = await _recipeCategoryRepository.GetByNameAsync(payload.Name, systemCompanyId);
      if (nameExists != null)
      {
        throw new ArgumentException("Invalid name, may you can try with another one.");
      }

      var slug = payload.Name.SlugGenerate();
      var slugExists = await _recipeCategoryRepository.GetBySlugAsync(slug, systemCompanyId);
      if (slugExists != null)
      {
        throw new ArgumentException($"Invalid slug, the {slug} already exists!");
      }

      var recipeCategory = new RecipeCategory(slug, payload.Name, payload.Active, systemCompanyId);

      await _recipeCategoryRepository.CreateAsync(recipeCategory);
      return recipeCategory;
    }
    catch
    {
      throw;
    }
  }

  public async Task<RecipeCategory> UpdateAsync(UpdateRecipeCategoryDto payload, long systemCompanyId)
  {
    try
    {
      var CategoryExists = await _recipeCategoryRepository.GetByNameAsync(payload.Name, systemCompanyId);

      if (CategoryExists != null && CategoryExists.Guid != payload.Guid)
      {
        throw new ArgumentException("Invalid name, may you can try with another one.");
      }

      var recipeCategory = await _recipeCategoryRepository.GetByGuidAsync(payload.Guid, systemCompanyId);
      if (recipeCategory == null)
      {
        throw new ArgumentException("RecipeCategory not found!");
      }

      recipeCategory.Update(payload.Name, payload.Active);

      await _recipeCategoryRepository.UpdateAsync(recipeCategory);
      return recipeCategory;
    }
    catch
    {
      throw;
    }
  }

  public async Task<RecipeCategory> DeleteAsync(Guid guid, long systemCompanyId)
  {
    try
    {
      var recipeCategory = await _recipeCategoryRepository.GetByGuidAsync(guid, systemCompanyId);
      if (recipeCategory == null)
      {
        throw new ArgumentException("RecipeCategory not found!");
      }

      recipeCategory.Delete();

      await _recipeCategoryRepository.UpdateAsync(recipeCategory);
      return recipeCategory;
    }
    catch
    {
      throw;
    }
  }
}
