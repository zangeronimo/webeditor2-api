namespace Webeditor.Domain.Interfaces;

public interface IBaseRepository<TEntity> where TEntity : class
{
  Task<IEnumerable<TEntity>> GetAllAsync(long systemCompanyId);

  Task<TEntity?> GetByGuidAsync(Guid guid, long systemCompanyId);

  Task<TEntity?> CreateAsync(TEntity entity);

  Task<TEntity?> UpdateAsync(TEntity entity);
}
