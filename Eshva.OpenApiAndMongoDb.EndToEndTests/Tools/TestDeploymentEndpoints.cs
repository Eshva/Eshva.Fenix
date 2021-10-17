#region Usings

using System;

#endregion

namespace Eshva.OpenApiAndMongoDb.EndToEndTests.Tools
{
  public class TestDeploymentEndpoints
  {
    public Uri BffUrl { get; } = new("http://localhost:40101");
  }
}
