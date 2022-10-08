using Microsoft.AspNetCore.Mvc;
using Webeditor.Infra.Authorization;
using Webeditor.Infra.Extensions;
using AutoMapper;
using Webeditor.Domain.Models;
using Webeditor.Application.DTOs;
using Webeditor.Domain.Models.Recipes;
using Webeditor.Application.DTOs.Recipes.Tags;
using Webeditor.Application.Interfaces.Application.Recipes;

namespace Webeditor.API.Controllers.Recipes;

[Route("api/v1/Recipes/Tags")]
[ApiController]
public class RecipeTagController : Controller
{
  private readonly IRecipeTagService _recipeTagService;
  private readonly IMapper _mapper;

  public RecipeTagController(IRecipeTagService recipeTagService, IMapper mapper)
  {
    _recipeTagService = recipeTagService;
    _mapper = mapper;
  }

  [Authorize("ROLE_GET_RECIPETAG")]
  [HttpGet]
  public async Task<ActionResult<PaginationResultDto<RecipeTagDto>>> GetAll([FromQuery] RecipeTagFilterModel filter, [FromQuery] BasePaginationModel pagination)
  {
    try
    {
      var companyId = User.GetCompanyId();
      var result = await _recipeTagService.GetAllAsync(companyId, filter, pagination);
      var resultDto = new PaginationResultDto<RecipeTagDto>()
      {
        Result = _mapper.Map<ICollection<RecipeTagDto>>(result.Result),
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

  [Authorize("ROLE_GET_RECIPETAG")]
  [HttpGet("{guid}")]
  public async Task<ActionResult<RecipeTagDto>> Get(Guid guid)
  {
    try
    {
      var companyId = User.GetCompanyId();
      var tag = await _recipeTagService.GetByGuidAsync(guid, companyId);
      return Ok(_mapper.Map<RecipeTagDto>(tag));
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [Authorize("ROLE_PUT_RECIPETAG")]
  [HttpPost]
  public async Task<ActionResult<RecipeTagDto>> Create(CreateRecipeTagDto payload)
  {
    try
    {
      var companyId = User.GetCompanyId();
      var recipeTag = await _recipeTagService.CreateAsync(payload, companyId);
      return Created("", _mapper.Map<RecipeTagDto>(recipeTag));
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [Authorize("ROLE_PUT_RECIPETAG")]
  [HttpPut("{guid}")]
  public async Task<ActionResult<RecipeTagDto>> Update(Guid guid, UpdateRecipeTagDto payload)
  {
    try
    {
      if (!long.Equals(guid, payload.Guid))
      {
        throw new ArgumentException("The Guid param is invalid!");
      }

      var companyId = User.GetCompanyId();
      var recipeTag = await _recipeTagService.UpdateAsync(payload, companyId);
      return Ok(_mapper.Map<RecipeTagDto>(recipeTag));
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [Authorize("ROLE_DEL_RECIPETAG")]
  [HttpDelete("{guid}")]
  public async Task<ActionResult<RecipeTagDto>> Delete(Guid guid)
  {
    try
    {
      var companyId = User.GetCompanyId();
      var recipeTag = await _recipeTagService.DeleteAsync(guid, companyId);
      return Ok(_mapper.Map<RecipeTagDto>(recipeTag));
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}
