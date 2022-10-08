using Webeditor.Application.DTOs.System;
using Webeditor.Application.Interfaces.Application.System;
using Webeditor.Domain.Interfaces.Infra;
using Webeditor.Domain.Interfaces.System;

namespace Webeditor.Application.Services.System;

public class ProfileService : IProfileService
{

  private readonly ISystemUserRepository _systemUserRepository;
  private readonly IFileUploadProvider _fileUpload;

  private readonly IHashProvider _hashProvider;

  public ProfileService(ISystemUserRepository systemUserRepository, IHashProvider hashProvider, IFileUploadProvider fileUpload)
  {
    _systemUserRepository = systemUserRepository;
    _hashProvider = hashProvider;
    _fileUpload = fileUpload;
  }

  public async Task<SystemUser> UpdateAsync(ProfileDto payload, Guid userGuid, long systemCompanyId)
  {
    try
    {
      var emailExists = await _systemUserRepository.GetByEmailAsync(payload.Email);

      if (emailExists != null && emailExists.Guid != userGuid)
      {
        throw new ArgumentException("Invalid email, may you can try with another one.");
      }

      var systemUser = await _systemUserRepository.GetByGuidAsync(userGuid, systemCompanyId);
      if (systemUser == null)
      {
        throw new ArgumentException("SystemUser not found!");
      }

      if (!string.IsNullOrEmpty(payload.Avatar))
      {
        _fileUpload.DeleteFile(systemUser.Avatar);
        var upload = await _fileUpload.UploadFileAsync(payload.Avatar, $"{systemCompanyId}/profile");
        systemUser.SetAvatar(upload);
      }

      systemUser.Update(payload.Name, payload.Email);

      return await _systemUserRepository.UpdateAsync(systemUser);
    }
    catch
    {
      throw;
    }
  }

  public async Task<string> ChangePasswordAsync(PasswordProfileDto payload, Guid userGuid, long systemCompanyId)
  {
    try
    {
      var systemUser = await _systemUserRepository.GetByGuidAsync(userGuid, systemCompanyId);
      if (systemUser == null)
      {
        throw new ArgumentException("SystemUser not found!");
      }

      if (!_hashProvider.Verify(systemUser.Password, payload.Current))
      {
        throw new Exception("Invalid current password!");
      }
      systemUser.SetPassword(payload.New);
      systemUser.EncryptPassword(_hashProvider);

      await _systemUserRepository.UpdateAsync(systemUser);
      return "Password changed with successful!";
    }
    catch
    {
      throw;
    }
  }
}
