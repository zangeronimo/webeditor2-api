
public class ValidateResultModel
{
  public ValidateResultModel() { }

  public ValidateResultModel(Guid userGuid, long companyId)
  {
    UserGuid = userGuid;
    CompanyId = companyId;
  }

  public Guid UserGuid { get; set; }
  public long CompanyId { get; set; }
}
