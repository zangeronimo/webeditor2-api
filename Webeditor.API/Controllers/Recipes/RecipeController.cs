using Microsoft.AspNetCore.Mvc;
using Webeditor.Infra.Authorization;
using Webeditor.Infra.Extensions;
using AutoMapper;
using Webeditor.Application.DTOs.Recipes;
using Webeditor.Application.Interfaces.Application.Recipes;
using Webeditor.Domain.Models;
using Webeditor.Domain.Models.Recipes;
using Webeditor.Application.DTOs;

namespace Webeditor.API.Controllers.Recipes;

[Route("api/v1/Recipes")]
[ApiController]
public class RecipeController : Controller
{
  private readonly IRecipeService _recipeService;
  private readonly IMapper _mapper;

  public RecipeController(IRecipeService recipeService, IMapper mapper)
  {
    _recipeService = recipeService;
    _mapper = mapper;
  }

  [Authorize("ROLE_GET_RECIPE")]
  [HttpGet]
  public async Task<ActionResult<PaginationResultModel<RecipeDto>>> GetAll([FromQuery] RecipeFilterModel filter, [FromQuery] BasePaginationModel pagination)
  {
    try
    {
      var companyId = User.GetCompanyId();
      var result = await _recipeService.GetAllAsync(companyId, filter, pagination);
      var resultDto = new PaginationResultDto<RecipeDto>()
      {
        Result = _mapper.Map<ICollection<RecipeDto>>(result.Result),
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

  [Authorize("ROLE_GET_RECIPE")]
  [HttpGet("{guid}")]
  public async Task<ActionResult<RecipeDto>> Get(Guid guid)
  {
    try
    {
      var companyId = User.GetCompanyId();
      var recipe = await _recipeService.GetByGuidAsync(guid, companyId);
      return Ok(_mapper.Map<RecipeDto>(recipe));
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [Authorize("ROLE_PUT_RECIPE")]
  [HttpPost]
  public async Task<ActionResult<RecipeDto>> Create(CreateRecipeDto payload)
  {
    try
    {
      var companyId = User.GetCompanyId();
      var recipe = await _recipeService.CreateAsync(payload, companyId);
      return Created("", _mapper.Map<RecipeDto>(recipe));
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [Authorize("ROLE_PUT_RECIPE")]
  [HttpPut("{guid}")]
  public async Task<ActionResult<RecipeDto>> Update(Guid guid, UpdateRecipeDto payload)
  {
    try
    {
      if (!long.Equals(guid, payload.Guid))
      {
        throw new ArgumentException("The Guid param is invalid!");
      }

      var companyId = User.GetCompanyId();
      var recipe = await _recipeService.UpdateAsync(payload, companyId);
      return Ok(_mapper.Map<RecipeDto>(recipe));
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [Authorize("ROLE_DEL_RECIPE")]
  [HttpDelete("{guid}")]
  public async Task<ActionResult<RecipeDto>> Delete(Guid guid)
  {
    try
    {
      var companyId = User.GetCompanyId();
      var recipe = await _recipeService.DeleteAsync(guid, companyId);
      return Ok(_mapper.Map<RecipeDto>(recipe));
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}
