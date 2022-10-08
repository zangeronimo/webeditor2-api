using Webeditor.Application.DTOs.System;
using Webeditor.Application.Interfaces.Application.System;
using Webeditor.Domain.Entities.System;
using Webeditor.Domain.Interfaces.Infra;
using Webeditor.Domain.Interfaces.System;
using Webeditor.Domain.Models;
using Webeditor.Domain.Models.System;

namespace Webeditor.Application.Services.System;

public class SystemUserService : ISystemUserService
{

  private readonly ISystemUserRepository _systemUserRepository;
  private readonly ISystemRoleRepository _systemRoleRepository;
  private readonly IHashProvider _hashProvider;

  public SystemUserService(ISystemUserRepository systemUserRepository, ISystemRoleRepository systemRoleRepository, IHashProvider hashProvider)
  {
    _systemUserRepository = systemUserRepository;
    _systemRoleRepository = systemRoleRepository;
    _hashProvider = hashProvider;
  }

  public async Task<PaginationResultModel<SystemUser>> GetAllAsync(long systemCompanyId, SystemUserFilterModel filter, BasePaginationModel pagination)
  {
    try
    {
      return await _systemUserRepository.GetAllAsync(systemCompanyId, filter, pagination);
    }
    catch (Exception err)
    {
      throw new Exception(err.Message);
    }
  }

  public async Task<SystemUser> GetByGuidAsync(Guid guid, long systemCompanyId)
  {
    try
    {
      var systemUser = await _systemUserRepository.GetByGuidAsync(guid, systemCompanyId);
      if (systemUser == null)
      {
        throw new ArgumentException("SystemUser not found!");
      }
      return systemUser;
    }
    catch (Exception err)
    {
      throw new Exception(err.Message);
    }
  }


  public async Task<SystemUser> CreateAsync(CreateSystemUserDto payload, long systemCompanyId)
  {
    try
    {
      var emailExists = await _systemUserRepository.GetByEmailAsync(payload.Email);

      if (emailExists != null)
      {
        throw new ArgumentException("Invalid email, may you can try with another one.");
      }

      var systemUser = new SystemUser(payload.Name, payload.Email, payload.Password, systemCompanyId);
      systemUser.EncryptPassword(_hashProvider);

      var roles = new List<SystemRole> { };
      foreach (Guid roleGuid in payload.SystemRolesGuid)
      {
        var getRole = await _systemRoleRepository.GetByGuidAsync(roleGuid);
        roles.Add(getRole);
      }
      systemUser.AddRoles(roles);

      return await _systemUserRepository.CreateAsync(systemUser);
    }
    catch
    {
      throw;
    }
  }

  public async Task<SystemUser> UpdateAsync(UpdateSystemUserDto payload, long systemCompanyId)
  {
    try
    {
      var emailExists = await _systemUserRepository.GetByEmailAsync(payload.Email);

      if (emailExists != null && emailExists.Guid != payload.Guid)
      {
        throw new ArgumentException("Invalid email, may you can try with another one.");
      }

      var systemUser = await _systemUserRepository.GetByGuidAsync(payload.Guid, systemCompanyId);
      if (systemUser == null)
      {
        throw new ArgumentException("SystemUser not found!");
      }

      if (!string.IsNullOrEmpty(payload.Password))
      {
        systemUser.SetPassword(payload.Password);
        systemUser.EncryptPassword(_hashProvider);
      }

      systemUser.Update(payload.Name, payload.Email);

      var roles = new List<SystemRole> { };
      foreach (var roleGuid in payload.SystemRolesGuid)
      {
        var getRole = await _systemRoleRepository.GetByGuidAsync(roleGuid);
        roles.Add(getRole);
      }
      systemUser.AddRoles(roles);

      return await _systemUserRepository.UpdateAsync(systemUser);
    }
    catch
    {
      throw;
    }
  }

  public async Task<SystemUser> DeleteAsync(Guid guid, long systemCompanyId)
  {
    try
    {
      var systemUser = await _systemUserRepository.GetByGuidAsync(guid, systemCompanyId);
      if (systemUser == null)
      {
        throw new ArgumentException("SystemUser not found!");
      }

      systemUser.Delete();

      return await _systemUserRepository.UpdateAsync(systemUser);
    }
    catch
    {
      throw;
    }
  }
}
