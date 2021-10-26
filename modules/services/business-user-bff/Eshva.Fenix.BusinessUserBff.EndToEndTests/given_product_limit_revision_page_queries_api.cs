#region Usings

using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Eshva.Fenix.BusinessUserBff.EndToEndTests.Tools;
using Eshva.Fenix.BusinessUserBff.Models.ProductLimitPage;
using FluentAssertions.Json;
using MongoDB.Driver;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Xunit;
using Random = Eshva.Fenix.BusinessUserBff.EndToEndTests.Tools.Random;

#endregion

namespace Eshva.Fenix.BusinessUserBff.EndToEndTests
{
  [Collection(PageQueriesCollection.Name)]
  public class given_product_limit_revision_page_queries_api
  {
    public given_product_limit_revision_page_queries_api(
      MongoFixture mongo,
      HttpFixture http,
      DeploymentFixture deployment)
    {
      _mongo = mongo;
      _http = http.Client;
      _deploymentEndpoints = deployment.Endpoints;

      _productLimitRevisionPageId = Guid.NewGuid();
      _metadata = Random.String();
      _testTimeout = new CancellationTokenSource(TimeSpan.FromSeconds(value: 10)).Token;
    }

    [Fact]
    public async Task when_request_page_data_by_id_it_should_return_data_with_expected_structure_and_content()
    {
      var sample = PrepareSample(Samples.Pages.ProductLimitRevision.GetByIdResponse);
      await GivenKnownPageDataPreparedInStorage(sample);
      var response = await WhenRequestKnownPageDataById();
      await ItShouldRespondWithDocumentWithExpectedStructureAndContent(response, sample);
    }

    private Task GivenKnownPageDataPreparedInStorage(string sample)
    {
      var document = JsonConvert.DeserializeObject<ProductLimitRevisionPageDto>(sample);
      return _mongo.ProductLimitRevisionPagesCollection.InsertOneAsync(
        document,
        new InsertOneOptions(),
        _testTimeout);
    }

    private string PrepareSample(string sample)
    {
      var document = JsonConvert.DeserializeObject<ProductLimitRevisionPageDto>(sample);
      document.Id = _productLimitRevisionPageId;
      document.Metadata.Add("SomeMetadata", _metadata);
      return JsonConvert.SerializeObject(
        document,
        new JsonSerializerSettings
        {
          Converters = new JsonConverter[] { new StringEnumConverter() },
          ContractResolver = new DefaultContractResolver { NamingStrategy = new CamelCaseNamingStrategy() }
        });
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

    private readonly TestDeploymentEndpoints _deploymentEndpoints;
    private readonly HttpClient _http;
    private readonly string _metadata;
    private readonly MongoFixture _mongo;
    private readonly Guid _productLimitRevisionPageId;
    private readonly CancellationToken _testTimeout;
  }
}
