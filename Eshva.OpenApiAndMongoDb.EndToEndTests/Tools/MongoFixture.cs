#region Usings

using Eshva.OpenApiAndMongoDb.Models.ProductLimitPage;
using MongoDB.Driver;

#endregion

namespace Eshva.OpenApiAndMongoDb.EndToEndTests.Tools
{
  public class MongoFixture
  {
    public MongoFixture(
      MongoConnectionConfiguration mongoConnectionConfiguration,
      DocumentCollectionsConfiguration documentCollectionsConfiguration)
    {
      _client = new MongoClient(mongoConnectionConfiguration.ConnectionString);
      _database = _client.GetDatabase(mongoConnectionConfiguration.DatabaseName);
      ProductLimitRevisionPagesCollection =
        _database.GetCollection<ProductLimitRevisionPageDto>(documentCollectionsConfiguration.ProductLimitRevisionPages);
    }

    public IMongoCollection<ProductLimitRevisionPageDto> ProductLimitRevisionPagesCollection { get; }

    private readonly MongoClient _client;
    private readonly IMongoDatabase _database;
  }
}
