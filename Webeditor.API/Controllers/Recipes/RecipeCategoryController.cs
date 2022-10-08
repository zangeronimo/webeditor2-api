using Microsoft.AspNetCore.Mvc;
using Webeditor.Infra.Authorization;
using Webeditor.Infra.Extensions;
using AutoMapper;
using Webeditor.Application.Interfaces.Application.Recipes;
using Webeditor.Application.DTOs.Recipes.Categories;
using Webeditor.Domain.Models;
using Webeditor.Application.DTOs;
using Webeditor.Domain.Models.Recipes;

namespace Webeditor.API.Controllers.Recipes;

[Route("api/v1/Recipes/Categories")]
[ApiController]
public class RecipeCategoryController : Controller
{
  private readonly IRecipeCategoryService _recipeCategoryService;
  private readonly IMapper _mapper;

  public RecipeCategoryController(IRecipeCategoryService recipeCategoryService, IMapper mapper)
  {
    _recipeCategoryService = recipeCategoryService;
    _mapper = mapper;
  }

  [Authorize("ROLE_GET_RECIPECATEGORY")]
  [HttpGet]
  public async Task<ActionResult<PaginationResultModel<RecipeCategoryDto>>> GetAll([FromQuery] RecipeCategoryFilterModel filter, [FromQuery] BasePaginationModel pagination)
  {
    try
    {
      var companyId = User.GetCompanyId();
      var result = await _recipeCategoryService.GetAllAsync(companyId, filter, pagination);
      var resultDto = new PaginationResultDto<RecipeCategoryDto>()
      {
        Result = _mapper.Map<ICollection<RecipeCategoryDto>>(result.Result),
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

  [Authorize("ROLE_GET_RECIPECATEGORY")]
  [HttpGet("{guid}")]
  public async Task<ActionResult<RecipeCategoryDto>> Get(Guid guid)
  {
    try
    {
      var companyId = User.GetCompanyId();
      var cateogry = await _recipeCategoryService.GetByGuidAsync(guid, companyId);
      return Ok(_mapper.Map<RecipeCategoryDto>(cateogry));
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [Authorize("ROLE_PUT_RECIPECATEGORY")]
  [HttpPost]
  public async Task<ActionResult<RecipeCategoryDto>> Create(CreateRecipeCategoryDto payload)
  {
    try
    {
      var companyId = User.GetCompanyId();
      var recipeCategory = await _recipeCategoryService.CreateAsync(payload, companyId);
      return Created("", _mapper.Map<RecipeCategoryDto>(recipeCategory));
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [Authorize("ROLE_PUT_RECIPECATEGORY")]
  [HttpPut("{guid}")]
  public async Task<ActionResult<RecipeCategoryDto>> Update(Guid guid, UpdateRecipeCategoryDto payload)
  {
    try
    {
      if (!long.Equals(guid, payload.Guid))
      {
        throw new ArgumentException("The Guid param is invalid!");
      }

      var companyId = User.GetCompanyId();
      var recipeCategory = await _recipeCategoryService.UpdateAsync(payload, companyId);
      return Ok(_mapper.Map<RecipeCategoryDto>(recipeCategory));
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [Authorize("ROLE_DEL_RECIPECATEGORY")]
  [HttpDelete("{guid}")]
  public async Task<ActionResult<RecipeCategoryDto>> Delete(Guid guid)
  {
    try
    {
      var companyId = User.GetCompanyId();
      var recipeCategory = await _recipeCategoryService.DeleteAsync(guid, companyId);
      return Ok(_mapper.Map<RecipeCategoryDto>(recipeCategory));
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}
