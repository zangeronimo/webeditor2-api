using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Webeditor.Domain.Entities.Authorize;
using Webeditor.Domain.Enuns;
using Webeditor.Domain.Interfaces.System;
using Webeditor.Domain.Models;
using Webeditor.Domain.Models.System;
using Webeditor.Infra.Context;

namespace Webeditor.Infra.Repositories.System;

public class SystemUserRepository : BaseRepository<SystemUser>, ISystemUserRepository
{

  public SystemUserRepository(AppDbContext context) : base(context)
  { }

  public async Task<Authorize?> GetByEmailAsync(string email)
  {
    try
    {
      var user = await DbSet.Where(user => user.Email == email)
        .FirstOrDefaultAsync(user => user.RemovedAt == null && user.SystemCompany.RemovedAt == null && user.Active == ActiveEnum.Active);

      if (user != null)
        return new Authorize(user.Id, user.Guid, user.SystemCompanyId, user.Email, user.Password);

      return null;

    }
    catch
    {
      throw;
    }
  }

  public async Task<SystemUser?> GetByIdAsync(long id, long systemCompanyId)
  {

    try
    {
      return await DbSet
        .Include(user => user.SystemRoles.Where(r => r.RemovedAt == null))
        .Where(user => user.SystemCompanyId == systemCompanyId &&
          user.RemovedAt == null &&
          user.SystemCompany.RemovedAt == null)
        .FirstOrDefaultAsync(user => user.Id == id);
    }
    catch
    {
      throw;
    }
  }

  public override async Task<SystemUser?> GetByGuidAsync(Guid guid, long systemCompanyId)
  {

    try
    {
      return await DbSet
        .Include(user => user.SystemRoles.Where(r => r.RemovedAt == null))
        .Where(user => user.SystemCompanyId == systemCompanyId &&
          user.RemovedAt == null &&
          user.SystemCompany.RemovedAt == null)
        .FirstOrDefaultAsync(user => user.Guid == guid);
    }
    catch
    {
      throw;
    }
  }

  public async Task<PaginationResultModel<SystemUser>> GetAllAsync(long systemCompanyId, SystemUserFilterModel filter, BasePaginationModel pagination)
  {
    try
    {
      var query = DbSet.AsQueryable();

      if (!string.IsNullOrEmpty(filter?.Word))
        query = query.Where(user => user.Email.Contains(filter.Word) || user.Name.Contains(filter.Word));

      if (filter?.Guid != null)
        query = query.Where(user => user.Guid == filter.Guid);

      if (filter?.Active != null)
        query = query.Where(user => user.Active == filter.Active);

      if (filter?.InitialDate != null)
        query = query.Where(user => user.CreatedAt >= filter.InitialDate);

      if (filter?.FinalDate != null)
        query = query.Where(user => user.CreatedAt <= filter.FinalDate);

      if (filter?.Asc == true)
        query = query.OrderBy(Ordenation(filter?.OrderBy));
      else
        query = query.OrderByDescending(Ordenation(filter?.OrderBy));

      query = query.Where(user => user.SystemCompanyId == systemCompanyId && user.RemovedAt == null)
        .Include(user => user.SystemRoles.Where(r => r.RemovedAt == null));

      var Page = pagination?.Page ?? 0;
      var Items = pagination?.ItemsPerPage ?? 20;

      var Total = query.Count();
      query = query.Skip(Page * Items);
      query = query.Take(Items);

      var result = await query.ToListAsync();
      return new PaginationResultModel<SystemUser>()
      {
        Result = await query.ToListAsync(),
        Page = Page,
        ItemsPerPage = Items,
        Total = Total
      };
    }
    catch
    {
      throw;
    }
  }

  private Expression<Func<SystemUser, Object>> Ordenation(string? order)
  {
    if (!string.IsNullOrEmpty(order))
    {
      switch (order.ToLowerInvariant())
      {
        case "name":
          return x => x.Name;
        case "email":
          return x => x.Email;
        case "active":
          return x => x.Active;
      }
    }

    return x => x.CreatedAt;
  }
}
