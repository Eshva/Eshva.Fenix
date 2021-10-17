#region Usings

using Newtonsoft.Json;

#endregion

namespace Eshva.OpenApiAndMongoDb.Models.ProductLimitPage
{
  public class LimitType
  {
    [JsonRequired]
    public ProductLimitType ProductLimitType;

    public Revolving Revolving;
  }
}
