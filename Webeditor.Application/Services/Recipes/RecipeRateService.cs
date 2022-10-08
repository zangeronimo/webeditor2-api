using Webeditor.Application.Interfaces.Application.Recipes;
using Webeditor.Domain.Entities.Recipes;
using Webeditor.Domain.Enuns;
using Webeditor.Domain.Interfaces.Recipes;
using Webeditor.Domain.Models;
using Webeditor.Domain.Models.Recipes;

namespace Webeditor.Application.Services.Recipes;

public class RecipeRateService : IRecipeRateService
{

  private readonly IRecipeRateRepository _recipeRateRepository;

  public RecipeRateService(IRecipeRateRepository recipeRateRepository)
  {
    _recipeRateRepository = recipeRateRepository;
  }

  public async Task<PaginationResultModel<RecipeRate>> GetAllAsync(long systemCompanyId, RecipeRateFilterModel filter, BasePaginationModel pagination)
  {
    try
    {
      return await _recipeRateRepository.GetAllAsync(systemCompanyId, filter, pagination);
    }
    catch (Exception err)
    {
      throw new Exception(err.Message);
    }
  }

  public async Task<RecipeRate> SetStatusAsync(Guid guid, ActiveEnum status, long systemCompanyId)
  {
    try
    {
      var recipeRate = await _recipeRateRepository.GetByGuidAsync(guid, systemCompanyId);
      if (recipeRate == null)
      {
        throw new ArgumentException("RecipeRate not found!");
      }

      recipeRate.SetStatus(status);

      await _recipeRateRepository.UpdateAsync(recipeRate);
      return recipeRate;
    }
    catch
    {
      throw;
    }
  }

  public async Task<RecipeRate> DeleteAsync(Guid guid, long systemCompanyId)
  {
    try
    {
      var recipeRate = await _recipeRateRepository.GetByGuidAsync(guid, systemCompanyId);
      if (recipeRate == null)
      {
        throw new ArgumentException("RecipeRate not found!");
      }

      recipeRate.Delete();

      await _recipeRateRepository.UpdateAsync(recipeRate);
      return recipeRate;
    }
    catch
    {
      throw;
    }
  }
}
