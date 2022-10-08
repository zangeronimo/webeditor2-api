using Webeditor.Application.DTOs.System;

namespace Webeditor.Application.Interfaces.Application.System;

public interface IProfileService
{
  Task<SystemUser> UpdateAsync(ProfileDto payload, Guid userGuid, long systemCompanyId);
  Task<string> ChangePasswordAsync(PasswordProfileDto payload, Guid userGuid, long systemCompanyId);
}
