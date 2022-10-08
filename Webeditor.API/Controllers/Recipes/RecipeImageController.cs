using Microsoft.AspNetCore.Mvc;
using Webeditor.Infra.Authorization;
using Webeditor.Infra.Extensions;
using AutoMapper;
using Webeditor.Application.Interfaces.Application.Recipes;
using Webeditor.Domain.Enuns;
using Webeditor.Application.DTOs.Recipes.Images;
using Webeditor.Domain.Models;
using Webeditor.Application.DTOs;
using Webeditor.Domain.Models.Recipes;

namespace Webeditor.API.Controllers.Recipes;

[Route("api/v1/Recipes/Images")]
[ApiController]
public class RecipeImageController : Controller
{
  private readonly IRecipeImageService _recipeImageService;
  private readonly IMapper _mapper;
  public RecipeImageController(IRecipeImageService recipeImageService, IMapper mapper)
  {
    _recipeImageService = recipeImageService;
    _mapper = mapper;
  }

  [Authorize("ROLE_GET_RECIPEIMAGE")]
  [HttpGet]
  public async Task<ActionResult<PaginationResultDto<RecipeImageDto>>> GetAll([FromQuery] RecipeImageFilterModel filter, [FromQuery] BasePaginationModel pagination)
  {
    try
    {
      var companyId = User.GetCompanyId();
      var result = await _recipeImageService.GetAllAsync(companyId, filter, pagination);
      var resultDto = new PaginationResultDto<RecipeImageDto>()
      {
        Result = _mapper.Map<ICollection<RecipeImageDto>>(result.Result),
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

  [Authorize("ROLE_PUT_RECIPEIMAGE")]
  [HttpPatch("{guid}/{status}")]
  public async Task<ActionResult<RecipeImageDto>> SetStatus(Guid guid, ActiveEnum status)
  {
    try
    {
      var companyId = User.GetCompanyId();
      var recipeImage = await _recipeImageService.SetStatusAsync(guid, status, companyId);
      return Ok(_mapper.Map<RecipeImageDto>(recipeImage));
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [Authorize("ROLE_DEL_RECIPEIMAGE")]
  [HttpDelete("{guid}")]
  public async Task<ActionResult<RecipeImageDto>> Delete(Guid guid)
  {
    try
    {
      var companyId = User.GetCompanyId();
      var recipeImage = await _recipeImageService.DeleteAsync(guid, companyId);
      return Ok(_mapper.Map<RecipeImageDto>(recipeImage));
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}
