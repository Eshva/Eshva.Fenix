#region Usings

using System;
using Newtonsoft.Json;

#endregion

namespace Eshva.OpenApiAndMongoDb.Models.ProductLimitPage
{
  public class ProductLimitRevision
  {
    [JsonRequired]
    public Guid Id;

    [JsonRequired]
    public LimitType LimitType;

    public Participant[] Participants;
  }
}
