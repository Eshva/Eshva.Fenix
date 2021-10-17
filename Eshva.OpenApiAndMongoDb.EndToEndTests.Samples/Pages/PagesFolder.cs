namespace Eshva.OpenApiAndMongoDb.EndToEndTests
{
  public class PagesFolder : ResourceFolder
  {
    public PagesFolder(ResourceFolder parentFolder) : base(parentFolder, "Pages")
    {
      ProductLimitRevision = new ProductLimitRevisionFolder(this);
    }

    public readonly ProductLimitRevisionFolder ProductLimitRevision;
  }
}
