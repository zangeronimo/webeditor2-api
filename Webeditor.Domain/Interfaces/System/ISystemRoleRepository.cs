using Webeditor.Domain.Entities.System;

namespace Webeditor.Domain.Interfaces.System;

public interface ISystemRoleRepository : IBaseRepository<SystemRole>
{
  Task<SystemRole?> GetByGuidAsync(Guid guid);
}
