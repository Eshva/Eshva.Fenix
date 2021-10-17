#region Usings

using System.Collections.Generic;
using Eshva.OpenApiAndMongoDb.Models.ProductLimitPage;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Options;
using MongoDB.Bson.Serialization.Serializers;

#endregion

namespace Eshva.OpenApiAndMongoDb.Bff.Service.Bootstrapping
{
  public static class MongoDbMapper
  {
    public static void MapClasses()
    {
      BsonClassMap.RegisterClassMap<ProductLimitRevisionPageDto>(
        classMap =>
        {
          classMap.AutoMap();
          classMap.MapIdField(model => model.Id);
          classMap.MapMember(model => model.Metadata).SetSerializer(
            new DictionaryInterfaceImplementerSerializer<Dictionary<string, object>>(DictionaryRepresentation.ArrayOfDocuments));
        });
    }
  }
}
