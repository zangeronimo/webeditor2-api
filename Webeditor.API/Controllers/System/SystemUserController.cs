using Microsoft.AspNetCore.Mvc;
using Webeditor.Infra.Authorization;
using Webeditor.Infra.Extensions;
using Webeditor.Application.DTOs.System;
using Webeditor.Application.Interfaces.Application.System;
using AutoMapper;
using Webeditor.Domain.Models;
using Webeditor.Domain.Models.System;
using Webeditor.Application.DTOs;

namespace Webeditor.API.Controllers.System;

[Route("api/v1/[Controller]")]
[ApiController]
public class SystemUserController : Controller
{
  private readonly ISystemUserService _systemUserService;
  private readonly IMapper _mapper;

  public SystemUserController(ISystemUserService systemUserService, IMapper mapper)
  {
    _systemUserService = systemUserService;
    _mapper = mapper;
  }

  [Authorize("ROLE_GET_SYSTEMUSER")]
  [HttpGet]
  public async Task<ActionResult<PaginationResultModel<SystemUserDto>>> Get([FromQuery] SystemUserFilterModel filter, [FromQuery] BasePaginationModel pagination)
  {
    try
    {
      var companyId = User.GetCompanyId();
      var result = await _systemUserService.GetAllAsync(companyId, filter, pagination);
      var resultDto = new PaginationResultDto<SystemUserDto>()
      {
        Result = _mapper.Map<ICollection<SystemUserDto>>(result.Result),
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

  [Authorize("ROLE_GET_SYSTEMUSER")]
  [HttpGet("{guid}")]
  public async Task<ActionResult<SystemUserDto>> Get(Guid guid)
  {
    try
    {
      var companyId = User.GetCompanyId();
      var systemUser = await _systemUserService.GetByGuidAsync(guid, companyId);
      return Ok(_mapper.Map<SystemUserDto>(systemUser));
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [Authorize("ROLE_PUT_SYSTEMUSER")]
  [HttpPost]
  public async Task<ActionResult<SystemUserDto>> Create(CreateSystemUserDto payload)
  {
    try
    {
      if (!string.Equals(payload.Password, payload.PasswordConfirmation))
      {
        throw new ArgumentException("Invalid password confirmation");
      }

      var companyId = User.GetCompanyId();
      var systemUser = await _systemUserService.CreateAsync(payload, companyId);
      return Created("", _mapper.Map<SystemUserDto>(systemUser));
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [Authorize("ROLE_PUT_SYSTEMUSER")]
  [HttpPut("{guid}")]
  public async Task<ActionResult<SystemUserDto>> Update(Guid guid, UpdateSystemUserDto payload)
  {
    try
    {
      if (!long.Equals(guid, payload.Guid))
      {
        throw new ArgumentException("The Guid param is invalid!");
      }

      var userGuid = User.GetUserGuid();
      if (Guid.Equals(guid, userGuid) && !string.IsNullOrEmpty(payload.Password))
      {
        throw new ArgumentException("You can't change your password here!");
      }

      if (!string.Equals(payload.Password, payload.PasswordConfirmation))
      {
        throw new ArgumentException("Invalid password confirmation");
      }

      var companyId = User.GetCompanyId();
      var systemUser = await _systemUserService.UpdateAsync(payload, companyId);
      return Ok(_mapper.Map<SystemUserDto>(systemUser));
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [Authorize("ROLE_DEL_SYSTEMUSER")]
  [HttpDelete("{guid}")]
  public async Task<ActionResult<SystemUserDto>> Delete(Guid guid)
  {
    try
    {
      var userGuid = User.GetUserGuid();
      if (Guid.Equals(guid, userGuid))
      {
        throw new ArgumentException("You can't remove yourself!");
      }

      var companyId = User.GetCompanyId();
      var systemUser = await _systemUserService.DeleteAsync(guid, companyId);
      return Ok(_mapper.Map<SystemUserDto>(systemUser));
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}
