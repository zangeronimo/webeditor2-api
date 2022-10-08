using Webeditor.Domain.Enuns;

namespace Webeditor.Domain.Models;

public abstract class BaseFilterModel
{
  public Guid? Guid { get; set; }

  public DateTime? InitialDate { get; set; }

  public DateTime? FinalDate { get; set; }

  public ActiveEnum? Active { get; set; }

  public string? OrderBy { get; set; }

  public bool? Asc { get; set; }
}
