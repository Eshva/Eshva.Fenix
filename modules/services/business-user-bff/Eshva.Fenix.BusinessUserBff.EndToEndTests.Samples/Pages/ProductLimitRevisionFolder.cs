namespace Eshva.Fenix.BusinessUserBff.EndToEndTests
{
  public class ProductLimitRevisionFolder : ResourceFolder
  {
    public ProductLimitRevisionFolder(ResourceFolder parent) : base(parent, "ProductLimitRevision") { }

    public string GetByIdResponse => ReadFileContent("get-by-id-response.json");
  }
}
