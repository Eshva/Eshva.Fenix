#region Usings

using System;
using System.Collections.Generic;

#endregion

namespace Eshva.OpenApiAndMongoDb.Models.ProductLimitPage
{
  public class ProductLimitRevisionPageDto : ProductLimitRevision
  {
    public Guid Id;
    public LimitType LimitType;
    public Participant[] Participants;
    public Dictionary<string, object> Metadata;
  }
}
