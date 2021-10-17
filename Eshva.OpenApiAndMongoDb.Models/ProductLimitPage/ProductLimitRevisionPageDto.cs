#region Usings

using System.Collections.Generic;

#endregion

namespace Eshva.OpenApiAndMongoDb.Models.ProductLimitPage
{
  public class ProductLimitRevisionPageDto : ProductLimitRevision
  {
    public Dictionary<string, object> Metadata;
  }
}
