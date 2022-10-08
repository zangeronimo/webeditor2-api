using Microsoft.AspNetCore.Mvc;
using Webeditor.Infra.Authorization;
using Webeditor.Infra.Extensions;
using AutoMapper;
using Webeditor.Application.Interfaces.Application.Recipes;
using Webeditor.Domain.Enuns;
using Webeditor.Application.DTOs.Recipes.Rates;
using Webeditor.Domain.Models;
using Webeditor.Domain.Models.Recipes;
using Webeditor.Application.DTOs;

namespace Webeditor.API.Controllers.Recipes;

[Route("api/v1/Recipes/Rates")]
[ApiController]
public class RecipeRateController : Controller
{
  private readonly IRecipeRateService _recipeRateService;
  private readonly IMapper _mapper;
  public RecipeRateController(IRecipeRateService recipeRateService, IMapper mapper)
  {
    _recipeRateService = recipeRateService;
    _mapper = mapper;
  }

  [Authorize("ROLE_GET_RECIPERATE")]
  [HttpGet]
  public async Task<ActionResult<PaginationResultModel<RecipeRateDto>>> GetAll([FromQuery] RecipeRateFilterModel filter, [FromQuery] BasePaginationModel pagination)
  {
    try
    {
      var companyId = User.GetCompanyId();
      var result = await _recipeRateService.GetAllAsync(companyId, filter, pagination);
      var resultDto = new PaginationResultDto<RecipeRateDto>()
      {
        Result = _mapper.Map<ICollection<RecipeRateDto>>(result.Result),
        Page = result.Page,
        ItemsPerPage = result.ItemsPerPage,
        Total = result.Total
      };
      return Ok(resultDto);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [Authorize("ROLE_PUT_RECIPERATE")]
  [HttpPatch("{guid}/{status}")]
  public async Task<ActionResult<RecipeRateDto>> SetStatus(Guid guid, ActiveEnum status)
  {
    try
    {
      var companyId = User.GetCompanyId();
      var recipeRate = await _recipeRateService.SetStatusAsync(guid, status, companyId);
      return Ok(_mapper.Map<RecipeRateDto>(recipeRate));
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [Authorize("ROLE_DEL_RECIPERATE")]
  [HttpDelete("{guid}")]
  public async Task<ActionResult<RecipeRateDto>> Delete(Guid guid)
  {
    try
    {
      var companyId = User.GetCompanyId();
      var recipeRate = await _recipeRateService.DeleteAsync(guid, companyId);
      return Ok(_mapper.Map<RecipeRateDto>(recipeRate));
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}
