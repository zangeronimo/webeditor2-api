using System.Collections.ObjectModel;

namespace Webeditor.Application.DTOs;

public class PaginationResultDto<T>
{
  public PaginationResultDto()
  {
    Result = new Collection<T>() { };
  }

  public ICollection<T> Result { get; set; }

  public int Page { get; set; }

  public int ItemsPerPage { get; set; }

  public int Total { get; set; }
}
