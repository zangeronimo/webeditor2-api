using Webeditor.Domain.Enuns;

namespace Webeditor.Domain.Entities;

public abstract class EntityBase
{
  public long Id { get; protected set; }
  public Guid Guid { get; protected set; }
  public ActiveEnum Active { get; protected set; }
  public DateTime CreatedAt { get; protected set; }
  public DateTime UpdatedAt { get; protected set; }
  public DateTime? RemovedAt { get; protected set; }
}
