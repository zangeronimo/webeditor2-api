using Webeditor.Application.DTOs.Recipes;
using Webeditor.Application.Interfaces.Application.Recipes;
using Webeditor.Application.Utils;
using Webeditor.Domain.Entities.Recipes;
using Webeditor.Domain.Enuns;
using Webeditor.Domain.Interfaces.Infra;
using Webeditor.Domain.Interfaces.Recipes;
using Webeditor.Domain.Models;
using Webeditor.Domain.Models.Recipes;

namespace Webeditor.Application.Services.Recipes;

public class RecipeService : IRecipeService
{

  private readonly IRecipeRepository _recipeRepository;
  private readonly IRecipeCategoryRepository _recipeCategoryRepository;
  private readonly IRecipeImageRepository _recipeImageRepository;
  private readonly IRecipeTagRepository _recipeTagRepository;
  private readonly IFileUploadProvider _fileUploadProvider;

  public RecipeService(IRecipeRepository recipeRepository,
    IRecipeCategoryRepository recipeCategoryRepository,
    IRecipeImageRepository recipeImageRepository,
    IRecipeTagRepository recipeTagRepository,
    IFileUploadProvider fileUploadProvider)
  {
    _recipeRepository = recipeRepository;
    _recipeCategoryRepository = recipeCategoryRepository;
    _recipeImageRepository = recipeImageRepository;
    _recipeTagRepository = recipeTagRepository;
    _fileUploadProvider = fileUploadProvider;
  }

  public async Task<PaginationResultModel<Recipe>> GetAllAsync(long systemCompanyId, RecipeFilterModel filter, BasePaginationModel pagination)
  {
    try
    {
      return await _recipeRepository.GetAllAsync(systemCompanyId, filter, pagination);
    }
    catch (Exception err)
    {
      throw new Exception(err.Message);
    }
  }

  public async Task<Recipe> GetByGuidAsync(Guid guid, long systemCompanyId)
  {
    try
    {
      var recipe = await _recipeRepository.GetByGuidAsync(guid, systemCompanyId);
      if (recipe == null)
      {
        throw new ArgumentException("Recipe not found!");
      }
      return recipe;
    }
    catch (Exception err)
    {
      throw new Exception(err.Message);
    }
  }

  public async Task<Recipe> CreateAsync(CreateRecipeDto payload, long systemCompanyId)
  {
    try
    {
      var recipeCategory = await _recipeCategoryRepository.GetByGuidAsync(payload.RecipeCategoryGuid, systemCompanyId);
      if (recipeCategory == null)
      {
        throw new ArgumentException("Invalid RecipeCategory, may you can try with another one.");
      }

      var slug = payload.Name.SlugGenerate();
      var slugExists = await _recipeRepository.GetBySlugAsync(slug, systemCompanyId);
      if (slugExists != null)
      {
        throw new ArgumentException($"Invalid slug, the {slug} already exists!");
      }

      var recipe = new Recipe(slug, payload.Name, payload.Ingredients, payload.Preparation, payload.Active, systemCompanyId);
      recipe.AddCategory(recipeCategory);
      await _recipeRepository.CreateAsync(recipe);

      if (!string.IsNullOrEmpty(payload.Image))
      {
        await UploadImage(payload.Image, recipe.Id, systemCompanyId);
      }

      var recipeImages = await _recipeImageRepository.GetAllByRecipeAsync(recipe.Id, systemCompanyId);
      if (recipeImages.Any())
        recipe.AddImages(recipeImages);

      if (payload.Tags != null && payload.Tags.Any())
      {
        payload.Tags.ForEach(async tag =>
        {
          RecipeTag? recipeTag = await _recipeTagRepository.GetByGuidAsync(tag, systemCompanyId);
          if (recipeTag != null)
          {
            recipe.AddTag(recipeTag);
          }
        });
      }

      await _recipeRepository.UpdateAsync(recipe);
      return recipe;
    }
    catch
    {
      throw;
    }
  }

  public async Task<Recipe> UpdateAsync(UpdateRecipeDto payload, long systemCompanyId)
  {
    try
    {
      var recipe = await _recipeRepository.GetByGuidAsync(payload.Guid, systemCompanyId);
      if (recipe == null)
      {
        throw new ArgumentException("Recipe not found!");
      }

      var recipeCategory = await _recipeCategoryRepository.GetByGuidAsync(payload.RecipeCategoryGuid, systemCompanyId);
      if (recipeCategory == null)
      {
        throw new ArgumentException("Invalid RecipeCategory, may you can try with another one.");
      }

      recipe.Update(payload.Name, payload.Ingredients, payload.Preparation, payload.Active);
      recipe.AddCategory(recipeCategory);

      if (!string.IsNullOrEmpty(payload.Image))
      {
        await UploadImage(payload.Image, recipe.Id, systemCompanyId);
      }

      var recipeImages = await _recipeImageRepository.GetAllByRecipeAsync(recipe.Id, systemCompanyId);
      if (recipeImages.Any())
        recipe.AddImages(recipeImages);

      if (payload.Tags != null && payload.Tags.Any())
      {
        payload.Tags.ForEach(async tag =>
        {
          RecipeTag? recipeTag = await _recipeTagRepository.GetByGuidAsync(tag, systemCompanyId);
          if (recipeTag != null)
          {
            recipe.AddTag(recipeTag);
          }
        });
      }

      await _recipeRepository.UpdateAsync(recipe);
      return recipe;
    }
    catch
    {
      throw;
    }
  }

  public async Task<Recipe> DeleteAsync(Guid guid, long systemCompanyId)
  {
    try
    {
      var recipe = await _recipeRepository.GetByGuidAsync(guid, systemCompanyId);
      if (recipe == null)
      {
        throw new ArgumentException("Recipe not found!");
      }

      recipe.Delete();

      await _recipeRepository.UpdateAsync(recipe);
      return recipe;
    }
    catch
    {
      throw;
    }
  }

  private async Task UploadImage(string image, long recipeId, long systemCompanyId)
  {
    var imagePath = await _fileUploadProvider.UploadFileAsync(image, $"{systemCompanyId}/recipes/recipe-images");
    if (!string.IsNullOrEmpty(imagePath))
    {
      var recipeImage = new RecipeImage(imagePath, recipeId, ActiveEnum.Active, systemCompanyId);
      await _recipeImageRepository.CreateAsync(recipeImage);
    }
  }
}
