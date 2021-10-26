#region Usings

using System;

#endregion

namespace Eshva.Fenix.BusinessUserBff.EndToEndTests.Tools
{
  public class TestDeploymentEndpoints
  {
    public Uri BffUrl { get; } = new("http://localhost:40101");
  }
}
