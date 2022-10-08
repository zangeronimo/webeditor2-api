using System.Collections.ObjectModel;

namespace Webeditor.Domain.Models;

public class PaginationResultModel<T>
{
  public PaginationResultModel()
  {
    Result = new Collection<T> { };
  }

  public ICollection<T> Result { get; set; }

  public int Page { get; set; }

  public int ItemsPerPage { get; set; }

  public int Total { get; set; }
}
