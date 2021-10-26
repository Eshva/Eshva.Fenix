#region Usings

using System.Net.Http;

#endregion

namespace Eshva.Fenix.BusinessUserBff.EndToEndTests.Tools
{
  public class HttpFixture
  {
    public HttpClient Client { get; set; } = new();
  }
}
