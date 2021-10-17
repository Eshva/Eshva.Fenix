namespace Eshva.OpenApiAndMongoDb.EndToEndTests.Tools
{
  public class MongoConnectionConfiguration
  {
    public MongoConnectionConfiguration(
      string connectionString,
      string databaseName,
      string userName,
      string password)
    {
      ConnectionString = connectionString;
      DatabaseName = databaseName;
      UserName = userName;
      Password = password;
    }

    public string ConnectionString { get; }

    public string DatabaseName { get; }

    public string UserName { get; }

    public string Password { get; }
  }
}
