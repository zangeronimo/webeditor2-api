using Microsoft.EntityFrameworkCore;
using Webeditor.Domain.Interfaces;
using Webeditor.Domain.Models;
using Webeditor.Infra.Context;

namespace Webeditor.Infra.Repositories;

public abstract class BaseRepository<TEntity> : IDisposable, IBaseRepository<TEntity> where TEntity : class
{
  private AppDbContext _context;
  public DbSet<TEntity> DbSet;

  public BaseRepository(AppDbContext context)
  {
    _context = context;
    DbSet = _context.Set<TEntity>();
  }

  public async Task<TEntity?> CreateAsync(TEntity entity)
  {
    try
    {
      _context.Add(entity);
      await _context.SaveChangesAsync();
      return entity;
    }
    catch
    {
      throw;
    }
  }

  public async Task<TEntity?> UpdateAsync(TEntity entity)
  {
    try
    {
      _context.Update(entity);
      await _context.SaveChangesAsync();
      return entity;
    }
    catch
    {
      throw;
    }
  }

  public virtual Task<IEnumerable<TEntity>> GetAllAsync(long systemCompanyId)
  {
    throw new NotImplementedException();
  }

  public virtual Task<TEntity?> GetByGuidAsync(Guid guid, long systemCompanyId)
  {
    throw new NotImplementedException();
  }

  public void Dispose()
  {
    _context.Dispose();
  }
}
