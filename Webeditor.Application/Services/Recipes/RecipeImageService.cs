using Webeditor.Application.Interfaces.Application.Recipes;
using Webeditor.Domain.Entities.Recipes;
using Webeditor.Domain.Enuns;
using Webeditor.Domain.Interfaces.Infra;
using Webeditor.Domain.Interfaces.Recipes;
using Webeditor.Domain.Models;
using Webeditor.Domain.Models.Recipes;

namespace Webeditor.Application.Services.Recipes;

public class RecipeImageService : IRecipeImageService
{

  private readonly IRecipeImageRepository _recipeImageRepository;
  private readonly IFileUploadProvider _fileUpload;

  public RecipeImageService(IRecipeImageRepository recipeImageRepository, IFileUploadProvider fileUpload)
  {
    _recipeImageRepository = recipeImageRepository;
    _fileUpload = fileUpload;
  }

  public async Task<PaginationResultModel<RecipeImage>> GetAllAsync(long systemCompanyId, RecipeImageFilterModel filter, BasePaginationModel pagination)
  {
    try
    {
      return await _recipeImageRepository.GetAllAsync(systemCompanyId, filter, pagination);
    }
    catch (Exception err)
    {
      throw new Exception(err.Message);
    }
  }

  public async Task<RecipeImage> SetStatusAsync(Guid guid, ActiveEnum status, long systemCompanyId)
  {
    try
    {
      var recipeImage = await _recipeImageRepository.GetByGuidAsync(guid, systemCompanyId);
      if (recipeImage == null)
      {
        throw new ArgumentException("RecipeImage not found!");
      }

      recipeImage.SetStatus(status);

      await _recipeImageRepository.UpdateAsync(recipeImage);
      return recipeImage;
    }
    catch
    {
      throw;
    }
  }

  public async Task<RecipeImage> DeleteAsync(Guid guid, long systemCompanyId)
  {
    try
    {
      var recipeImage = await _recipeImageRepository.GetByGuidAsync(guid, systemCompanyId);
      if (recipeImage == null)
      {
        throw new ArgumentException("RecipeImage not found!");
      }

      _fileUpload.DeleteFile(recipeImage.Path);
      recipeImage.Delete();

      await _recipeImageRepository.UpdateAsync(recipeImage);
      return recipeImage;
    }
    catch
    {
      throw;
    }
  }
}
