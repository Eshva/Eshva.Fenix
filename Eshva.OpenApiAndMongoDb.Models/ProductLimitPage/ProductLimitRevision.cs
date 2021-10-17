#region Usings

using System;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using Newtonsoft.Json;

#endregion

namespace Eshva.OpenApiAndMongoDb.Models.ProductLimitPage
{
  public class ProductLimitRevision
  {
    [JsonRequired]
    [BsonId(IdGenerator = typeof(GuidGenerator))]
    public Guid Id;

    [JsonRequired]
    public LimitType LimitType;

    public Participant[] Participants;
  }
}
