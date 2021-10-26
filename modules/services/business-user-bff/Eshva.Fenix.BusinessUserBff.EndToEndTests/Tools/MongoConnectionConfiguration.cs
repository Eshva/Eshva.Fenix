namespace Eshva.Fenix.BusinessUserBff.EndToEndTests.Tools
{
  public class MongoConnectionConfiguration
  {
    public string ConnectionString { get; set; }

    public string DatabaseName { get; set; }

    public const string Section = "MongoConnection";
  }
}
