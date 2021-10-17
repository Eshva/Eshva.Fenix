#region Usings

using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Eshva.OpenApiAndMongoDb.EndToEndTests.Tools;
using Eshva.OpenApiAndMongoDb.Models.ProductLimitPage;
using FluentAssertions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;
using Random = Eshva.OpenApiAndMongoDb.EndToEndTests.Tools.Random;

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
      var document = JsonConvert.DeserializeObject<ProductLimitRevisionPageDto>(jsonDocumentSample);
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
}
