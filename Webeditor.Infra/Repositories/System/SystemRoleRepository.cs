using Microsoft.EntityFrameworkCore;
using Webeditor.Domain.Entities.System;
using Webeditor.Domain.Interfaces.System;
using Webeditor.Infra.Context;

namespace Webeditor.Infra.Repositories.System;

public class SystemRoleRepository : BaseRepository<SystemRole>, ISystemRoleRepository
{
  public SystemRoleRepository(AppDbContext context) : base(context)
  { }

  public async Task<SystemRole?> GetByGuidAsync(Guid guid)
  {
    try
    {
      return await DbSet.Where(role => role.RemovedAt == null)
      .FirstOrDefaultAsync(role => role.Guid == guid);
    }
    catch
    {
      throw;
    }

  }

  public override async Task<IEnumerable<SystemRole>> GetAllAsync(long systemCompanyId)
  {
    try
    {
      return await DbSet.Where(role => role.RemovedAt == null).ToListAsync();
    }
    catch
    {
      throw;
    }
  }
}
