#region Usings

using Xunit;

#endregion

namespace Eshva.Fenix.BusinessUserBff.EndToEndTests.Tools
{
  [CollectionDefinition(Name)]
  public class PageQueriesCollection
    : ICollectionFixture<MongoFixture>,
      ICollectionFixture<HttpFixture>,
      ICollectionFixture<DeploymentFixture>
  {
    public const string Name = "Page queries collection";
  }
}
