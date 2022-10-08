using Webeditor.Application.DTOs.System;
using Webeditor.Domain.Models;
using Webeditor.Domain.Models.System;

namespace Webeditor.Application.Interfaces.Application.System;

public interface ISystemUserService
{
  Task<PaginationResultModel<SystemUser>> GetAllAsync(long systemCompanyId, SystemUserFilterModel filter, BasePaginationModel pagination);

  Task<SystemUser> GetByGuidAsync(Guid guid, long systemCompanyId);

  Task<SystemUser> CreateAsync(CreateSystemUserDto payload, long systemCompanyId);

  Task<SystemUser> UpdateAsync(UpdateSystemUserDto payload, long systemCompanyId);

  Task<SystemUser> DeleteAsync(Guid guid, long systemCompanyId);
}
