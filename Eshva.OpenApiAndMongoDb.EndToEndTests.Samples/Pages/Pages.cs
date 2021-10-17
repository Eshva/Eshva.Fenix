#region Usings

using System.IO;
using System.Text;

#endregion

namespace Eshva.OpenApiAndMongoDb.EndToEndTests
{
  public static class Samples
  {
    static Samples()
    {
      var rootFolder = new ResourceFolder(typeof(Samples).Namespace);
      Pages = new PagesFolder(rootFolder);
    }

    public static PagesFolder Pages { get; }
  }

  public class PagesFolder : ResourceFolder
  {
    public PagesFolder(ResourceFolder parentFolder) : base(parentFolder, "Pages")
    {
      ProductLimitRevision = new ProductLimitRevisionFolder(this);
    }

    public ProductLimitRevisionFolder ProductLimitRevision;
  }

  public class ProductLimitRevisionFolder : ResourceFolder
  {
    public ProductLimitRevisionFolder(ResourceFolder parent) : base(parent, "ProductLimitRevision") { }

    public string GetByIdResponse => ReadFileContent("get-by-id-response.json");
  }

  public class ResourceFolder
  {
    public ResourceFolder(string basePath)
    {
      _basePath = Normalize(basePath);
    }
    //Eshva.OpenApiAndMongoDb.EndToEndTests.pages.product_limit_revision.get-by-id-response.json
    protected ResourceFolder(ResourceFolder parentFolder, string folderName)
    {
      _basePath = Normalize($"{parentFolder._basePath}.{folderName}");
    }

    private string Normalize(string path)
    {
      // TODO: Use C#-type name rules for each part of the path to replace illegal symbols with underscores.
      // TODO: Path part can't start with a digit. Add underscore before digit.
      return path.Replace('-', '_');
    }

    protected string ReadFileContent(string resourceFileName)
    {
      var resourceFilePath = $"{_basePath}.{resourceFileName}";
      using var resourceStream = GetType().Assembly.GetManifestResourceStream(resourceFilePath);
      using var reader = new StreamReader(resourceStream!, Encoding.UTF8);
      return reader.ReadToEnd();
    }

    private readonly string _basePath;
  }
}
