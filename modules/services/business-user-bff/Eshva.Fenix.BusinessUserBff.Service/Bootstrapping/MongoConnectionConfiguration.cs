namespace Eshva.Fenix.BusinessUserBff.Service.Bootstrapping
{
  public class MongoConnectionConfiguration
  {
    public string ConnectionString { get; set; }

    public string DatabaseName { get; set; }

    public string UserName { get; set; }

    public string Password { get; set; }

    public const string Section = "DocumentStorage";
  }
}
