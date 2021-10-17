#region Usings

using System.Threading.Tasks;
using MongoDB.Driver;

#endregion

namespace Eshva.OpenApiAndMongoDb.EndToEndTests.Tools
{
  public class MongoTestContext
  {
    // TODO: Ensure MongoDB authentication is disabled.
    public MongoTestContext(MongoConnectionConfiguration configuration)
    {
      var connectionSettings = MongoClientSettings.FromConnectionString(configuration.ConnectionString);
      Client = new MongoClient(connectionSettings);
      Database = Client.GetDatabase(configuration.DatabaseName);
    }

    public IMongoDatabase Database { get; }

    public MongoClient Client { get; }

    // public MongoConnectionConfiguration Configuration { get; }

    public async Task Store<TDocument>(string collectionName, TDocument document)
    {
      await EnsureCollectionCreated(collectionName);
    }

    private Task EnsureCollectionCreated(string collectionName) => Database.CreateCollectionAsync(collectionName);
  }
}
