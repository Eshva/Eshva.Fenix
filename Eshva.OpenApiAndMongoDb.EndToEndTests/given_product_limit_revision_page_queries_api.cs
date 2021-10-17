#region Usings

using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Eshva.OpenApiAndMongoDb.Models.ProductLimitPage;
using FluentAssertions;
using MongoDB.Driver;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RandomStringCreator;
using Xunit;

#endregion

namespace Eshva.OpenApiAndMongoDb.EndToEndTests
{
  [Collection(PageQueriesCollection.Name)]
  public class given_product_limit_revision_page_queries_api
  {
    public given_product_limit_revision_page_queries_api(
      MongoFixture mongo,
      HttpFixture http,
      DeploymentFixture deployment)
    {
      _mongo = mongo.Context;
      _http = http.Client;
      _deploymentEndpoints = deployment.Endpoints;

      _productLimitRevisionPageId = Guid.NewGuid();
      _collectionName = Random.String();
      _testTimeout = new CancellationTokenSource(TimeSpan.FromSeconds(value: 10)).Token;

      // MongoDbMapper.MapClasses(); // TODO: Do I need it?
    }

    [Fact]
    public async Task when_request_page_data_by_id_it_should_return_data_with_expected_structure_and_content()
    {
      var jsonDocumentSample = Samples.Pages.ProductLimitRevision.GetByIdResponse;
      await GivenKnownPageDataPreparedInStorage(jsonDocumentSample);
      var response = await WhenRequestKnownPageDataById();
      await ItShouldRespondWithDocumentWithExpectedStructureAndContent(response, jsonDocumentSample);
    }

    private Task GivenKnownPageDataPreparedInStorage(string jsonDocumentSample)
    {
      var document = JsonConvert.DeserializeObject<ProductLimitRevisionPageDto>(
        jsonDocumentSample,
        new JsonSerializerSettings
        {
          TypeNameHandling = TypeNameHandling.All
        });
      document.Id = _productLimitRevisionPageId;
      return _mongo.Store(_collectionName, document);
    }

    private Task<HttpResponseMessage> WhenRequestKnownPageDataById()
    {
      var requestUri = new Uri(_deploymentEndpoints.BffUrl, $"/api/pages/product-limit/{_productLimitRevisionPageId:N}");
      return _http.GetAsync(requestUri, _testTimeout);
    }

    private async Task ItShouldRespondWithDocumentWithExpectedStructureAndContent(HttpResponseMessage response, string jsonSampleDocument)
    {
      var content = await response.Content.ReadAsStringAsync(_testTimeout);
      var responseDocument = JToken.Parse(content);
      var sampleDocument = JToken.Parse(jsonSampleDocument);
      responseDocument.Should().BeEquivalentTo(sampleDocument);

      // TODO: Check other properties of response.
    }

    private readonly string _collectionName;
    private readonly TestDeploymentEndpoints _deploymentEndpoints;
    private readonly HttpClient _http;
    private readonly MongoTestContext _mongo;
    private readonly Guid _productLimitRevisionPageId;
    private readonly CancellationToken _testTimeout;
  }

  public class DeploymentFixture
  {
    public TestDeploymentEndpoints Endpoints { get; set; } = new TestDeploymentEndpoints();
  }

  [CollectionDefinition(Name)]
  public class PageQueriesCollection
    : ICollectionFixture<MongoFixture>,
      ICollectionFixture<HttpFixture>,
      ICollectionFixture<DeploymentFixture>
  {
    public const string Name = "Page queries collection";
  }

  public class TestDeploymentEndpoints
  {
    public Uri BffUrl { get; } = new("http://localhost:40101");
  }

  public class MongoTestContext
  {
    public MongoTestContext(MongoConnectionConfiguration configuration)
    {
      var connectionSettings = MongoClientSettings.FromConnectionString(configuration.ConnectionString);
      // connectionSettings.Credential = MongoCredential.CreateGssapiCredential(
      //     configuration.UserName,
      //     configuration.Password);
      // connectionSettings.Credential = MongoCredential.CreateCredential(
      //     "admin",
      //     configuration.UserName,
      //     configuration.Password);
      // Configuration = configuration;
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

  public class MongoConnectionConfiguration
  {
    public MongoConnectionConfiguration(
      string connectionString,
      string databaseName,
      string userName,
      string password)
    {
      ConnectionString = connectionString;
      DatabaseName = databaseName;
      UserName = userName;
      Password = password;
    }

    public string ConnectionString { get; }

    public string DatabaseName { get; }

    public string UserName { get; }

    public string Password { get; }
  }

  public class HttpFixture
  {
    public HttpClient Client { get; set; } = new HttpClient();
  }

  public class HttpContext { }

  public class MongoFixture
  {
    public MongoFixture(MongoTestContext context)
    {
      Context = context;
    }

    public MongoTestContext Context { get; set; }
  }

  public static class Random
  {
    public static string String(int length = 10) => StringCreator.Get(length);

    private static readonly StringCreator StringCreator = new(@"abcdefghijklmnopqrstuvwxyz");
  }
}
