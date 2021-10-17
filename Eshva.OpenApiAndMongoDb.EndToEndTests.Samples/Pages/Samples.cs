namespace Eshva.OpenApiAndMongoDb.EndToEndTests
{
  public static class Samples
  {
    static Samples()
    {
      var rootFolder = new ResourceFolder(typeof(Samples).Namespace);
      Pages = new PagesFolder(rootFolder);
    }

    public static readonly PagesFolder Pages;
  }
}
