#region Usings

using System.IO;
using System.Text;

#endregion

namespace Eshva.OpenApiAndMongoDb.EndToEndTests
{
  public class ResourceFolder
  {
    public ResourceFolder(string basePath)
    {
      _basePath = Normalize(basePath);
    }

    protected ResourceFolder(ResourceFolder parentFolder, string folderName)
    {
      _basePath = Normalize($"{parentFolder._basePath}.{folderName}");
    }

    protected string ReadFileContent(string resourceFileName)
    {
      var resourceFilePath = $"{_basePath}.{resourceFileName}";
      using var resourceStream = GetType().Assembly.GetManifestResourceStream(resourceFilePath);
      using var reader = new StreamReader(resourceStream!, Encoding.UTF8);
      return reader.ReadToEnd();
    }

    private string Normalize(string path) =>
      // TODO: Use C#-type name rules for each part of the path to replace illegal symbols with underscores.
      // TODO: Path part can't start with a digit. Add underscore before digit.
      path.Replace(oldChar: '-', newChar: '_');

    private readonly string _basePath;
  }
}
