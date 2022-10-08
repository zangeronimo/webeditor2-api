using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Webeditor.Infra.Extensions;
using Webeditor.Application.DTOs.System;
using Webeditor.Application.Interfaces.Application.System;
using Webeditor.Infra.Authorization;

namespace Webeditor.API.Controllers.System;

[ApiController]
[Route("api/v1/[controller]")]
public class ProfileController : ControllerBase
{
  private readonly IProfileService _profileService;
  private readonly IMapper _mapper;

  public ProfileController(IProfileService profileService, IMapper mapper)
  {
    _profileService = profileService;
    _mapper = mapper;
  }

  [Authorize]
  [HttpPost]
  public async Task<ActionResult<IEnumerable<ProfileDto>>> Update(ProfileDto payload)
  {
    try
    {
      var userGuid = User.GetUserGuid();
      var companyId = User.GetCompanyId();
      var systemUser = await _profileService.UpdateAsync(payload, userGuid, companyId);
      return Ok(_mapper.Map<ProfileDto>(systemUser));
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [Authorize]
  [HttpPost("Password")]
  public async Task<ActionResult<IEnumerable<string>>> ChangePassword(PasswordProfileDto payload)
  {
    try
    {
      if (!string.Equals(payload.New, payload.Confirmation))
      {
        throw new ArgumentException("Invalid password confirmation");
      }

      var userGuid = User.GetUserGuid();
      var companyId = User.GetCompanyId();
      var systemUser = await _profileService.ChangePasswordAsync(payload, userGuid, companyId);
      return Ok("Password changed with success!");
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}
