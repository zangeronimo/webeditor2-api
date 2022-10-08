using Webeditor.Domain.Entities.Authorize;
using Webeditor.Domain.Models;
using Webeditor.Domain.Models.System;

namespace Webeditor.Domain.Interfaces.System;

public interface ISystemUserRepository : IBaseRepository<SystemUser>
{
  Task<PaginationResultModel<SystemUser>> GetAllAsync(long systemCompanyId, SystemUserFilterModel filter, BasePaginationModel pagination);

  Task<Authorize?> GetByEmailAsync(string email);
}
