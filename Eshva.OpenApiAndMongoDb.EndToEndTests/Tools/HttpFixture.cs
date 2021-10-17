#region Usings

using System.Net.Http;

#endregion

namespace Eshva.OpenApiAndMongoDb.EndToEndTests.Tools
{
  public class HttpFixture
  {
    public HttpClient Client { get; set; } = new();
  }
}
